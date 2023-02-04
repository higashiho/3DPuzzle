using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using Stage;


/// <summary>
/// プレイヤーの移動制御クラス
/// </summary>
public class PlayerMove 
{
    // 移動先のタイルの座標取得用(キャスト用)
    private Vector3? destination = null;
    
    /// <summary>
    /// 移動先Y座標とプレイヤーY座標比較
    /// </summary>
    /// <param name="target">移動先座標</param>
    /// <param name="myPos">自身の座標</param>
    /// <returns></returns>
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
    public async void Move(CancellationTokenSource cts, BaseNeedle needle, BasePlayer player)
    {
        
        // プレイヤーが回転中, 移動中またはステージが移動中はinputを受け付けない
        if(player.IsRotate || player.OnMove || InGameSceneController.Stages.Moving)
        {
            resetMoveValue(player);
            return;
        }    

        // 座標調整 => 自身の座標取得(キャスト(int) & 四捨五入(整数))
        Vector3 myPos = (Vector3)calcRoundingHalfUp(player.transform.position);
        player.transform.position = myPos;

        // 移動先座標決定
        // 目標座標取得(キャスト(int) & 四捨五入(整数))
        destination = calcRoundingHalfUp(input(player));
        
        
        // input関数でnullが返ってきたらキャンセル(移動先が選択されていないため)
        if(destination == null)
        {
            // 移動に使った値初期化
            resetMoveValue(player);
            return;
        }
            
        // プレイヤーMoveタスクキャンセル処理
        if(player.PlayerMoveCancel)
        {
            // 移動に使った値初期化
            resetMoveValue(player);
            return;
        }
        
        // 移動方向フラグを立てる
        player.MoveFlag = setDirection(player.transform.position, (Vector3)destination);
        // 移動中フラグON
        
        

        // 移動先のY座標とプレイヤーのY座標を比較 => 同じ座標だった場合
        if(checkHeight((Vector3)destination, player.transform.position) == Const.UPPER_ROTATE)
        {
            // 上移動
            ascendingMove(player, player.MoveFlag, player.RotatePointArr, player.GoUpRotatePointArr, cts);
        }
        // 移動先のY座標とプレイヤーのY座標を比較 => 移動先の座標がキューブ１マス分低い座標だった場合
        else if(checkHeight((Vector3)destination, player.transform.position) == Const.NORMAL_ROTATE)
        {
            // 平面移動
            shiftMove(player, player.MoveFlag, player.RotatePointArr, cts);
        }
        // 移動先のY座標とプレイヤーのY座標を比較 => 移動先の座標がキューブ１マス分低い座標だった場合
        else if(checkHeight((Vector3)destination, player.transform.position) == Const.DOWN_ROTATE)
        {
            // 下移動
            descendingMove(player, player.MoveFlag, player.RotatePointArr, player.GoUpRotatePointArr, cts);
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
        resetMoveValue(player);
       
    }

    /// <summary>
    /// 移動先のタイルの座標を取得する関数
    /// </summary>
    /// <param name="player">Playerの実体</param>
    /// <returns>移動先のタイルの座標, Vector3.zero(クリックされなかったとき)</returns>
    private Vector3? input(BasePlayer player)
    {
        if(Input.GetMouseButtonDown(0))
        {
            player.OnMove = true;
            return player.ChooseObj?.transform.position/* + new Vector3(0f, 5f, 0f)*/;
        }
        return null;   
    }

    /// <summary>
    /// 座標を四捨五入してintにキャストする関数
    /// </summary>
    /// <param name="pos">対象座標</param>
    /// <returns>四捨五入&intにキャストされた座標</returns>
    private Vector3? calcRoundingHalfUp(Vector3? pos)
    {
        if(pos == null)
            return null;

        Vector3 newPos  = (Vector3)pos;

        newPos.x = Mathf.RoundToInt(newPos.x);
        newPos.y = Mathf.RoundToInt(newPos.y);
        newPos.z = Mathf.RoundToInt(newPos.z);

        return newPos;
    }


    /// <summary>
    /// 移動方向フラグを立てる関数
    /// </summary>
    /// <param name="mySelf">自身の座標</param>
    /// <param name="other">目的地の座標</param>
    private int setDirection(Vector3 mySelf, Vector3 other)
    {
        if((mySelf.x < other.x) && (mySelf.z == other.z))
        {   
            return Const.RIGHT;
        }
        else if((mySelf.x > other.x) && (mySelf.z == other.z))
        {
            return Const.LEFT;
        }
        if((mySelf.x == other.x) && (mySelf.z < other.z))
        {
            return Const.FORWARD;
        }
        else if((mySelf.x == other.x) && (mySelf.z > other.z))
        {
            return Const.BACK;
        }
        return 0;
    }

    /// <summary>
    /// 回転中心を設定する関数
    /// </summary>
    /// <param name="pos">回転させるオブジェクトの座標</param>
    /// <param name="flag">移動方向フラグ</param>
    /// <returns>回転中心</returns>
    private Vector3 setRotatePoint(Vector3[] Arr, int flag)
    {
        if(flag == Const.RIGHT)
            return Arr[0] * Const.CUBE_SIZE_HALF;
        if(flag == Const.LEFT)
            return Arr[1] * Const.CUBE_SIZE_HALF;
        if(flag == Const.FORWARD)
            return Arr[2] * Const.CUBE_SIZE_HALF;
        if(flag == Const.BACK)
            return Arr[3] * Const.CUBE_SIZE_HALF;
        if(flag == Const.UP)
            return Arr[4] * Const.CUBE_SIZE_HALF;
        if(flag == Const.DOWN)
            return Arr[5] * Const.CUBE_SIZE_HALF;

        return Vector3.zero;
    }
    /// <summary>
    /// 回転軸を設定する関数
    /// </summary>
    /// <param name="flag">移動方向フラグ</param>
    /// <returns>回転軸</returns>
    private Vector3 setRotateAxis(int flag, BasePlayer player)
    {
        if(flag == Const.RIGHT)
            return player.RotateAxisArr[0] * Const.CUBE_SIZE_HALF;
        if(flag == Const.LEFT)
            return player.RotateAxisArr[1] * Const.CUBE_SIZE_HALF;
        if(flag == Const.FORWARD)
            return player.RotateAxisArr[2] * Const.CUBE_SIZE_HALF;
        if(flag == Const.BACK)
            return player.RotateAxisArr[3] * Const.CUBE_SIZE_HALF;
        if(flag == Const.UP)
            return player.RotateAxisArr[4] * Const.CUBE_SIZE_HALF;
        if(flag == Const.DOWN)
            return player.RotateAxisArr[5] * Const.CUBE_SIZE_HALF;

        return Vector3.zero;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    private async UniTask rotateMove(BasePlayer player)
    {
        // 回転中のフラグを立てる
        player.IsRotate = true;
        // 回転角を設定
        if(player.CubeAngle == default)
            player.CubeAngle = InGameSceneController.Player.PlayersData.PlayerMoveTime;
        
        float sumAngle = 0f;    // 回転角の合計値保存用
        while(sumAngle < 90f)
        {
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
    public void resetMoveValue(BasePlayer player)
    {
        player.MoveFlag = -1;
        player.OnMove = false;
        player.WaitMove = null;
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
    private async void ascendingMove(BasePlayer player, int flag, Vector3[] point, Vector3[] goUpRotatePointArr, CancellationTokenSource cts)
    {
        if(player.PlayerMoveCancel)
            return;
        // Y軸(正)に回転移動
        // 移動フラグを確認して回転軸と回転中心を設定
        player.RotateAxis = setRotateAxis(flag, player);
        player.RotatePoint = player.transform.position + setRotatePoint(goUpRotatePointArr, flag);
        if(!player.IsRotate)
        {
            await UniTask.WhenAny(rotateMove(player));
        }

        player.RotateAxis = setRotateAxis(flag, player);
        player.RotatePoint = player.transform.position + setRotatePoint(point, flag);
        
        
        if(!player.IsRotate)
        {
            await UniTask.WhenAny(rotateMove(player));
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
    private async void descendingMove(BasePlayer player, int flag, Vector3[] point, Vector3[] goUpRotatePointArr, CancellationTokenSource cts)
    {
        player.RotateAxis = setRotateAxis(flag, player);
        player.RotatePoint = player.transform.position + setRotatePoint(point, flag);
        
        
        if(!player.IsRotate)
        {
            await UniTask.WhenAny(rotateMove(player));
        }

        if(player.PlayerMoveCancel)
            return;

        // Y軸(負)に回転移動
        // 移動フラグを確認して回転軸と回転中心を設定
        player.RotateAxis = setRotateAxis(flag, player);
        player.RotatePoint = player.transform.position + (-1) * setRotatePoint(goUpRotatePointArr, flag);
        
        if(!player.IsRotate)
        {
            await UniTask.WhenAny(rotateMove(player));
        }

        
    }

    /// <summary>
    /// 平行移動処理
    /// </summary>
    /// <param name="flag">プレイヤーの移動フラグ</param>
    /// <param name="point">プレイヤーの回転中心配列</param>
    /// <returns></returns>
    private async void shiftMove(BasePlayer player, int flag, Vector3[] point, CancellationTokenSource cts)
    {
        
        if(player.PlayerMoveCancel)
            return;
        // 移動方向フラグ向きに平行移動
        // 回転軸と回転中心を設定
        // // 移動フラグを確認して回転軸と回転中心を設定
        player.RotateAxis = setRotateAxis(flag, player);
        player.RotatePoint = player.transform.position + setRotatePoint(point, flag);
        
        
        if(!player.IsRotate)
        {
            await UniTask.WhenAny(rotateMove(player));
        }
    }

}
