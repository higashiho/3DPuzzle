using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

/// <summary>
/// プレイヤーの移動制御クラス
/// </summary>
public class PlayerMove 
{
    private Vector3 myPos;       // プレイヤーの座標取得用(キャスト用)
    private Vector3? destination;// 移動先のタイルの座標取得用(キャスト用)
    private Vector3 prevPos;     // 移動前のプレイヤーの座標
    private Vector3 rotatePoint; // 回転中心の座標(ワールド座標)
    private Vector3 rotateAxis;  //回転軸(ワールド座標)
    private float cubeAngle = 0;    // プレイヤーの回転角
    private bool isRotate = false;  // 回転中のフラグ
    private int moveFlag = -1;      // プレイヤーの移動フラグ
    private Stack<int> moveNum = new Stack<int>(2);     // 移動回数保存キュー  


    /// <summary>
    /// 回転軸の座標配列
    /// </summary>
    /// <value></value>
    private Vector3[] rotateAxisArr = 
    {
        new Vector3(0f, 0f, -1f),   // X(正)
        new Vector3(0f, 0f, 1f),    // X(負)
        new Vector3(1f, 0f, 0f),    // Z(正)
        new Vector3(-1f, 0f, 0f),   // Z(負)
        new Vector3(0f, 1f, 0f),    // Y(正)
        new Vector3(0f, -1f, 0f),   // Y(負)
        Vector3.zero                // 初期化用
    };
    /// <summary>
    /// 平面を転がるときの回転中心の座標配列
    /// </summary>
    /// <value></value>
    private Vector3[] rotatePointArr = 
    {
        new Vector3(1f, -1f, 0f),
        new Vector3(-1f, -1f, 0f),
        new Vector3(0f, -1f, 1f),
        new Vector3(0f, -1f, -1f)
    };

    /// <summary>
    /// Rayを飛ばす方向の配列
    /// </summary>
    /// <value></value>
    private Vector3[] rayAspect = 
    {
        new Vector3(1f, 0f, 0f),    // X(正)
        new Vector3(-1f, 0f, 0f),   // X(負)
        new Vector3(0f, 0f, 1f),    // Z(正)
        new Vector3(0f, 0f, -1f),   // Z(負)
        new Vector3(0f, -1f, 0f)    // Y(負)
        // Yの正方向には今のところ飛ばすことがないので要素なし
    };
    /// <summary>
    /// 上段のタイルに上るときの回転中心座標の配列
    /// </summary>
    /// <value></value>
    private Vector3[] goUpRotatePointArr = 
    {
        new Vector3(1f, 1f, 0f),
        new Vector3(-1f, 1f, 0f),
        new Vector3(0f, 1f, 1f),
        new Vector3(0f, 1f, -1f)
    };

    /// <summary>
    /// 下段のタイルに降りるときの回転中心座標配列
    /// </summary>
    /// <value></value>
    private Vector3[]goDownRotatePointArr = 
    {
        new Vector3(-1f, -1f, 0f),
        new Vector3(1f, -1f, 0f),
        new Vector3(0f, -1f, -1f),
        new Vector3(0f, -1f, 1f)
    };

    private Ray ray, ray2;      // 移動方向にブロックがあるか判定するRay
    private RaycastHit hit;     // Rayに当たったオブジェクト取得用
    
    // 移動関数
    public async void Move(BasePlayer tmpPlayer, CancellationTokenSource cts)
    {
        if(cts.IsCancellationRequested)
            return;
        // 回転中またはブロックが移動中ならInput受け付けない
        if(isRotate || InGameSceneController.Stages.Moving)    
            return;
        
        // 目標座標取得(キャスト(int) & 四捨五入(整数))
        destination = (calcRoundingHalfUp(input(tmpPlayer)));

        // input関数でゼロが返ってきたらキャンセル(移動先が選択されていないため)
        if(destination == null)
            return;

        // 自身の座標取得(キャスト(int) & 四捨五入(整数))
        myPos = (Vector3)calcRoundingHalfUp(tmpPlayer.transform.position);
        // 座標調整
        tmpPlayer.transform.position = myPos;
        

        // 移動方向フラグを立てる
        moveFlag = setDirection(myPos, (Vector3)destination);
        
        
        try
        {
            
            // プレイヤーが移動先のタイルにたどり着くまで繰り返す
            while(calcRoundingHalfUp(tmpPlayer.transform.position) != destination)
            {
                if(destination == null)
                    break;
                    prevPos = tmpPlayer.transform.position;
                // 移動方向前方にBoxがあるか判定するRay
                ray = new Ray(tmpPlayer.transform.position, rayAspect[moveFlag]);
                // 自身の下にBoxがあるか判定するRay
                ray2 = new Ray(tmpPlayer.transform.position, rayAspect[4]);
                
                // 移動方向前方にBoxがあったら
                if(Physics.Raycast(ray, out hit,5f))
                {
                    
                    // 移動フラグを確認して回転軸と回転中心を設定
                    rotateAxis = setRotateAxis(moveFlag);
                    rotatePoint = setRotatePoint(tmpPlayer, goUpRotatePointArr, moveFlag);
                    // 回転
                    await rotateMove(tmpPlayer, cts);
            
                }
                
                // 自身の下にBoxがなかったら
                if(!Physics.Raycast(ray2, 5f) && tmpPlayer.transform.position.y >= Const.PLAYER_MOVABLE_POSY)
                {
                    // 移動フラグを確認して回転軸と回転中心を設定
                    rotateAxis = setRotateAxis(moveFlag);
                    rotatePoint = setRotatePoint(tmpPlayer, goDownRotatePointArr, moveFlag);
                    // 回転
                    await rotateMove(tmpPlayer, cts);
                    
                }
                
                // この時点で移動先タイルに到着していたらループを抜ける
                if(calcRoundingHalfUp(tmpPlayer.transform.position) == destination)
                    break;

                // 移動フラグを確認して回転軸と回転中心を設定
                rotateAxis = setRotateAxis(moveFlag);
                rotatePoint = setRotatePoint(tmpPlayer, rotatePointArr, moveFlag);
                // 回転
                await rotateMove(tmpPlayer, cts);
                // プレイヤーのX,Z方向の座標が変わっていたら
                if(rotateCounter(tmpPlayer.transform.position, prevPos))
                {
                    // moveCountインクリメント
                    tmpPlayer.MoveCount++;
                }
                Debug.Log(tmpPlayer.MoveCount);

                // 

            }
        }
        catch(OperationCanceledException)
        {
            throw;
        }
        
        // 移動に使った値を初期化
        resetMoveValue(tmpPlayer);
       
    }

    /// <summary>
    /// 移動先のタイルの座標を取得する関数
    /// </summary>
    /// <param name="tmpPlayer">Playerの実体</param>
    /// <returns>移動先のタイルの座標, Vector3.zero(クリックされなかったとき)</returns>
    private Vector3? input(BasePlayer tmpPlayer)
    {
        Vector3? movePos = null; 
        if(Input.GetMouseButtonDown(0))
        {
            movePos = tmpPlayer.ChooseObj?.transform.position + new Vector3(0f, 5f, 0f);
            return movePos;
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
    private Vector3 setRotatePoint(BasePlayer tmpPlayer, Vector3[] Arr, int flag)
    {
        if(flag == Const.RIGHT)
            return tmpPlayer.transform.position + Arr[0] * Const.CUBE_SIZE_HALF;
        if(flag == Const.LEFT)
            return tmpPlayer.transform.position + Arr[1] * Const.CUBE_SIZE_HALF;
        if(flag == Const.FORWARD)
            return tmpPlayer.transform.position + Arr[2] * Const.CUBE_SIZE_HALF;
        if(flag == Const.BACK)
            return tmpPlayer.transform.position + Arr[3] * Const.CUBE_SIZE_HALF;
        if(flag == Const.UP)
            return tmpPlayer.transform.position + Arr[4] * Const.CUBE_SIZE_HALF;
        if(flag == Const.DOWN)
            return tmpPlayer.transform.position + Arr[5] * Const.CUBE_SIZE_HALF;

        return tmpPlayer.transform.position + Vector3.zero;
    }
    /// <summary>
    /// 回転軸を設定する関数
    /// </summary>
    /// <param name="flag">移動方向フラグ</param>
    /// <returns>回転軸</returns>
    private Vector3 setRotateAxis(int flag)
    {
        if(flag == Const.RIGHT)
            return rotateAxisArr[0]* Const.CUBE_SIZE_HALF;
        if(flag == Const.LEFT)
            return rotateAxisArr[1]* Const.CUBE_SIZE_HALF;
        if(flag == Const.FORWARD)
            return rotateAxisArr[2]* Const.CUBE_SIZE_HALF;
        if(flag == Const.BACK)
            return rotateAxisArr[3]* Const.CUBE_SIZE_HALF;
        if(flag == Const.UP)
            return rotateAxisArr[4]* Const.CUBE_SIZE_HALF;
        if(flag == Const.DOWN)
            return rotateAxisArr[5]* Const.CUBE_SIZE_HALF;

        return Vector3.zero;
    }

    private async UniTask rotateMove(BasePlayer tmpPlayer, CancellationTokenSource cts)
    {
        // 回転中のフラグを立てる
        isRotate = true;
        if(cubeAngle == default)
            cubeAngle = InGameSceneController.Player.PlayersData.PlayerMoveTime;

        float sumAngle = 0f;    // angleの合計値を保存
        while(sumAngle < 90f)
        {
            if(cts.IsCancellationRequested)
                return;

            
            sumAngle += cubeAngle;

            // 90°以上回転しないように値を制限
            if(sumAngle > 90f)
            {
                cubeAngle -= sumAngle - 90f;
            }

            // 回転と移動
            tmpPlayer.transform.RotateAround(rotatePoint , rotateAxis , cubeAngle);

            try
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            catch(OperationCanceledException)
            {
                Debug.Log("キャンセルされました");
                throw;
            }
        
        }
        // 回転中のフラグを倒す
        isRotate = false;
    }
    
    /// <summary>
    /// 現在の座標と移動前の座標を比べて移動していたらtrueを返す関数
    /// </summary>
    /// <param name="pos">現在の座標</param>
    /// <param name="prevPos">移動前の座標</param>
    /// <returns>true:移動した, false:移動していない</returns>
    private bool rotateCounter(Vector3 pos, Vector3 prevPos)
    {
        if(pos.x != prevPos.x)      return true;
        else if(pos.z != prevPos.z) return true; 
        return false;
    }
    /// <summary>
    /// 移動につかった値を初期化する関数(moveFlagとOnMoveをここで初期化)
    /// </summary>
    /// <param name="tmpPlayer"></param>
    private void resetMoveValue(BasePlayer tmpPlayer)
    {
        moveFlag = -1;
        // tmpPlayer.MoveCount = 0;
        tmpPlayer.OnMove = false;
    }
}
