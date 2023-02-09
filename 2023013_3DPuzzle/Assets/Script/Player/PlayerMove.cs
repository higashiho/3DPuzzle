using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;
using Stage;


/// <summary>
/// プレイヤーの移動制御クラス
/// </summary>
public class PlayerMove 
{
    private BasePlayer player;

    // コンストラクタ
    public PlayerMove(BasePlayer tmpPlayer)
    {
        player = tmpPlayer;
    }


    /// <summary>
    /// 移動先Y座標とプレイヤーY座標比較
    /// </summary>
    /// <param name="target">移動先座標</param>
    /// <param name="myPos">自身の座標</param>
    /// <returns>移動先のタイルと自身のY座標の差がキューブ何個分か</returns>
    private int checkHeight(Vector3 target, Vector3 myPos)
    {
        if(target.y == myPos.y)
            return 0;
        else if((Mathf.Abs(target.y - myPos.y)) == Const.CUBE_SIZE_HALF*2) 
            return 1;
        else
            return 2;
    }


    /// <summary>
    /// プレイヤーの移動処理
    /// 移動先のY座標とプレイヤーのY座標を比較して
    /// プレイヤーの回転の仕方を決める
    /// </summary>
    /// <param name="cts">キャンセルトークンソース</param>
    /// <param name="needle">針タイルの実体</param>
    /// <param name="player">プレイヤーの実体</param>
    /// <returns></returns>
    public async void Move(CancellationTokenSource cts, BaseNeedle needle)
    {
        
        // プレイヤーが回転中, 移動中またはステージが移動中はreturn
        if(player.IsRotate || player.OnMove || InGameSceneController.Stages.Moving)
        {
            resetMoveValue();
            return;
        }    

        // 座標調整 => 自身の座標取得(キャスト(int) & 偶数まるめ(整数))
        player.transform.position = (Vector3)Functions.CalcRoundingHalfUp(player.transform.position);
        

        // 移動先座標決定
        // 目標座標取得(キャスト(int) & 偶数まるめ(整数))
        player.Destination = Functions.CalcRoundingHalfUp(input());
        
        
        // input関数でnullが返ってきたらキャンセル(移動先が選択されていないため)
        if(player.Destination == null)
        {
            // 移動に使った値初期化
            resetMoveValue();
            return;
        }
            
        // プレイヤーMoveタスクキャンセル処理
        if(player.PlayerMoveCancel)
        {
            // 移動に使った値初期化
            resetMoveValue();
            return;
        }

        // プレイヤーがスタートタイルまたはムーブステージ以外のステージにいる場合
        if(InGameSceneController.Stages.StageState != Const.STATE_START
         && InGameSceneController.Stages.StageState != StageConst.STATE_MOVE_STAGE)
        {
            
            // プレイヤーの今の座標をエネミーの追跡Queueに入れる
            InGameSceneController.EnemyManager.PlayerTrace.Enqueue(player.transform.position);
        }

        // 移動方向フラグを立てる
        player.MoveFlag = Functions.SetDirection(player.transform.position, (Vector3)player.Destination);
        // 移動中フラグON
        

        // 移動先のY座標とプレイヤーのY座標を比較 => 同じ座標だった場合
        if(checkHeight((Vector3)player.Destination, player.transform.position) == Const.UPPER_ROTATE)
        {
            // 上移動
            ascendingMove(player.MoveFlag, Const.RotatePointArr, Const.GoUpRotatePointArr, cts);
        }
        // 移動先のY座標とプレイヤーのY座標を比較 => 移動先の座標がキューブ１マス分低い座標だった場合
        else if(checkHeight((Vector3)player.Destination, player.transform.position) == Const.NORMAL_ROTATE)
        {
            // 平面移動
            shiftMove(player.MoveFlag, Const.RotatePointArr, cts);
        }
        // 移動先のY座標とプレイヤーのY座標を比較 => 移動先の座標がキューブ１マス分低い座標だった場合
        else if(checkHeight((Vector3)player.Destination, player.transform.position) == Const.DOWN_ROTATE)
        {
            // 下移動
            descendingMove(player.MoveFlag, Const.RotatePointArr, Const.GoUpRotatePointArr, cts);
        }
        
        

        // ニードル変換フラグオン
        needle.OnNeedleTrans = true;    
        // moveCountインクリメント
        player.MoveCount++;
        
        // 針タイル更新待ち処理
        if(InGameSceneController.Stages.StageState == StageConst.STATE_NEEDLE_STAGE)
        {
            await UniTask.WaitWhile(() => needle.OnNeedleTrans);
        }

         // 移動に使った値を初期化
        resetMoveValue();
       
    }

    /// <summary>
    /// 移動先のタイルの座標を取得する関数
    /// </summary>
    /// <param name="player">Playerの実体</param>
    /// <returns>移動先のタイルの座標, Vector3.zero(クリックされなかったとき)</returns>
    private Vector3? input()
    {
        if(Input.GetMouseButtonDown(0))
        {
            player.OnMove = true;
            return player.ChooseObj?.transform.position/* + new Vector3(0f, 5f, 0f)*/;
        }
        return null;   
    }


    /// <summary>
    /// プレイヤー回転移動処理
    /// </summary>
    /// <param name="player">プレイヤーの実体</param>
    /// <returns></returns>
    public async UniTask RotateMove()
    {
        // 回転中のフラグを立てる
        player.IsRotate = true;

        // 回転角を設定
        if(player.CubeAngle == default)
            player.CubeAngle = InGameSceneController.Player.PlayersData.PlayerMoveTime;
        
        float sumAngle = 0f;    // 回転角の合計値保存用
        while(sumAngle < 90f)
        {
            // タスクキャンセル処理
            if(player.PlayerMoveCancel)
                break;
            
            sumAngle += player.CubeAngle;

            // 90°以上回転しないように値を制限
            if(sumAngle > 90f)
            {
                player.CubeAngle -= sumAngle - 90f;
            }

            // 回転と移動
            player.transform.RotateAround(player.RotatePoint , player.RotateAxis , player.CubeAngle);

            await UniTask.Yield(PlayerLoopTiming.Update);
           
        }
        // 回転中のフラグを倒す
        player.IsRotate = false;
    }
    

    /// <summary>
    /// 移動につかった値を初期化する関数(moveFlagとOnMoveをここで初期化)
    /// </summary>
    /// <param name="tmpPlayer">プレイヤーの実体</param>
    public void resetMoveValue()
    {   
        // 移動方向フラグ初期化
        player.MoveFlag = -1;
        // プレイヤー移動中フラグOFF
        player.OnMove = false;
    }

    /// <summary>
    /// 上昇移動処理
    /// 上昇　=>  平行移動
    /// </summary>
    /// <param name="flag">プレイヤーの移動フラグ</param>
    /// <param name="point">プレイヤーの回転中心配列</param>
    /// <param name="axisArr">プレイヤーの回転軸配列</param>
    /// <param name="goUpRotatePointArr">プレイヤー上昇回転時の回転中心配列</param>
    /// <returns></returns>
    private async void ascendingMove(int flag, Vector3[] point, Vector3[] goUpRotatePointArr, CancellationTokenSource cts)
    {
        if(player.PlayerMoveCancel)
            return;
        // Y軸(正)に回転移動
        // 移動フラグを確認して回転軸と回転中心を設定
        player.RotateAxis = Functions.SetRotateAxis(flag) * Const.CUBE_SIZE_HALF;
        player.RotatePoint = player.transform.position + Functions.SetRotatePoint(goUpRotatePointArr, flag) * Const.CUBE_SIZE_HALF;
        
        // プレイヤーが回転中でなければ
        if(!player.IsRotate)
        {
            // 回転移動のタスクが終わるまで待つ
            await UniTask.WhenAny(RotateMove());
        }

        // 平行移動(X軸またはY軸)に回転移動
        // 移動フラグを確認して回転軸と回転中心を決定
        player.RotateAxis = Functions.SetRotateAxis(flag) * Const.CUBE_SIZE_HALF;
        player.RotatePoint = player.transform.position + Functions.SetRotatePoint(point, flag) * Const.CUBE_SIZE_HALF;
        
        // プレイヤーが回転中でなければ
        if(!player.IsRotate)
        {
            // 回転移動のタスクが終わるまで待つ
            await UniTask.WhenAny(RotateMove());
        }

    }
    
    /// <summary>
    /// 下降移動処理
    /// 下降
    /// </summary>
    /// <param name="flag">プレイヤーの移動フラグ</param>
    /// <param name="axisArr">プレイヤーの回転軸配列</param>
    /// <param name="goUpRotatePointArr">プレイヤー上昇回転時の回転中心配列</param>
    /// <returns></returns>
    private async void descendingMove(int flag, Vector3[] point, Vector3[] goUpRotatePointArr, CancellationTokenSource cts)
    {
        if(player.PlayerMoveCancel)
            return;
        // 平行移動(X軸またはY軸)に回転移動
        // 移動フラグを確認して回転軸と回転中心を決定
        player.RotateAxis = Functions.SetRotateAxis(flag) * Const.CUBE_SIZE_HALF;
        player.RotatePoint = player.transform.position + Functions.SetRotatePoint(point, flag) * Const.CUBE_SIZE_HALF;
        
        // プレイヤーが回転中でなければ
        if(!player.IsRotate)
        {
            // 回転移動のタスクが終わるまで待つ
            await UniTask.WhenAny(RotateMove());
        }

        // Y軸(負)に回転移動
        // 移動フラグを確認して回転軸と回転中心を設定
        player.RotateAxis = Functions.SetRotateAxis(flag) * Const.CUBE_SIZE_HALF;
        player.RotatePoint = player.transform.position + (-1) * Functions.SetRotatePoint(goUpRotatePointArr, flag) * Const.CUBE_SIZE_HALF;
        
        // プレイヤーが回転中でなければ
        if(!player.IsRotate)
        {
            // 回転移動のタスクが終わるまで待つ
            await UniTask.WhenAny(RotateMove());
        }

        
    }

    /// <summary>
    /// 平行移動処理
    /// </summary>
    /// <param name="flag">プレイヤーの移動フラグ</param>
    /// <param name="point">プレイヤーの回転中心配列</param>
    /// <returns></returns>
    private async void shiftMove(int flag, Vector3[] point, CancellationTokenSource cts)
    {
        
        if(player.PlayerMoveCancel)
            return;

        // 移動方向フラグ向きに平行移動
        // 移動フラグを確認して回転軸と回転中心を設定
        player.RotateAxis = Functions.SetRotateAxis(flag) * Const.CUBE_SIZE_HALF;
        player.RotatePoint = player.transform.position + Functions.SetRotatePoint(point, flag) * Const.CUBE_SIZE_HALF;
        
        // プレイヤーが回転中でなければ
        if(!player.IsRotate)
        {
            // 回転移動のタスクが終わるまで待つ
            await UniTask.WhenAny(RotateMove());
        }
    }

}
