using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

// 毎回プレイヤーと移動先の座標を比べる
/// <summary>
/// プレイヤーの移動制御クラス
/// </summary>
public class PlayerMove 
{
    private Vector3 myPos;
    private Vector3 destination;
    private Vector3 rotatePoint;
    private Vector3 rotateAxis;
    private float cubeAngle;
    private bool isRotate = false;
    private int moveFlag = -1;
    private Vector3[] rotatePointArr = 
    {
        new Vector3(0f, 0f, -1f),    // X(正)
        new Vector3(0f, 0f, 1f),   // X(負)
        new Vector3(1f, 0f, 0f),    // Z(正)
        new Vector3(-1f, 0f, 0f),   // Z(負)
        new Vector3(0f, 1f, 0f),    // Y(正)
        new Vector3(0f, -1f, 0f),   // Y(負)
        Vector3.zero                // 初期化用
    };
    private Vector3[] rayAspectArr = 
    {
        new Vector3(1f, -1f, 0f),
        new Vector3(-1f, -1f, 0f),
        new Vector3(0f, -1f, 1f),
        new Vector3(0f, -1f, -1f)
    };
    private Vector3[] rayAspect = 
    {
        new Vector3(1f, 0f, 0f),
        new Vector3(-1f, 0f, 0f),
        new Vector3(0f, 0f, 1f),
        new Vector3(0f, 0f, -1f),
        new Vector3(0f, -1f, 0f)
    };
    private Vector3[] goUpRotatePointArr = 
    {
        new Vector3(1f, 1f, 0f),
        new Vector3(-1f, 1f, 0f),
        new Vector3(0f, 1f, 1f),
        new Vector3(0f, 1f, -1f)
    };

    private Vector3[]goDownRotatePointArr = 
    {
        new Vector3(-1f, -1f, 0f),
        new Vector3(1f, -1f, 0f),
        new Vector3(0f, -1f, -1f),
        new Vector3(0f, -1f, 1f)
    };

    private Ray ray, ray2;
    private RaycastHit hit;
    
    // 移動関数
    public async void Move(BasePlayer tmpPlayer, CancellationTokenSource cts)
    {
        // 回転中またはブロックが移動中ならInput受け付けない
        if(isRotate || InGameSceneController.Stages.Moving)    
            return;
        
        // 目標座標取得
        destination = (calcRoundingHalfUp(input(tmpPlayer)));
        if(destination == Vector3.zero)
            return;

        // 自身の座標取得
        myPos = calcRoundingHalfUp(tmpPlayer.transform.position);
        tmpPlayer.transform.position = myPos;

        // 移動方向フラグを立てる
        moveFlag = setDirection(myPos, destination);
        Debug.Log(moveFlag);
        // 動く方向にレイを飛ばす
        
        try
        {
            
             
            while(calcRoundingHalfUp(tmpPlayer.transform.position) != destination)
            {
                Debug.Log("destination" + destination);
                Debug.Log("Player" + tmpPlayer.transform.position);
                ray = new Ray(tmpPlayer.transform.position, rayAspect[moveFlag]);
                ray2 = new Ray(tmpPlayer.transform.position, rayAspect[4]);
                
                if(Physics.Raycast(ray, 5f))
                {
                    // 移動フラグを確認して回転軸と回転中心を設定
                    rotateAxis = setRotateAxis(moveFlag);
                    rotatePoint = setRotatePoint(tmpPlayer, goUpRotatePointArr, moveFlag);
                    await rotateMove(tmpPlayer, cts);
                    
                }
                
                if(!Physics.Raycast(ray2, 5f))
                {
                    rotateAxis = setRotateAxis(moveFlag);
                    rotatePoint = setRotatePoint(tmpPlayer, goDownRotatePointArr, moveFlag);
                    await rotateMove(tmpPlayer, cts);
                    
                }
                if(calcRoundingHalfUp(tmpPlayer.transform.position) == destination)
                    break;
                rotateAxis = setRotateAxis(moveFlag);
                rotatePoint = setRotatePoint(tmpPlayer, rayAspectArr, moveFlag);
                await rotateMove(tmpPlayer, cts);

                
                
                
                
            }
            
            
        }
        catch(OperationCanceledException)
        {
            throw;
        }
        if(cts.IsCancellationRequested)
            return;
        
        
        resetMoveValue(tmpPlayer);
       
    }

    /// <summary>
    /// 移動関数処理
    /// </summary>
    /// <param name="tmpPlayer">プレイヤーの実体</param>
    /// <returns>移動先の座標(Vector3(0,0,0)が返ります)</returns>
    private Vector3 input(BasePlayer tmpPlayer)
    {
        Vector3? movePos = null; 
        if(Input.GetMouseButtonDown(0))
        {
            movePos = tmpPlayer.ChooseObj?.transform.position + new Vector3(0f, 5f, 0f);
            return (Vector3)movePos;
        }

        return Vector3.zero;   
    }

    /// <summary>
    /// 座標を四捨五入してintにキャストする関数
    /// </summary>
    /// <param name="pos">対象座標</param>
    /// <returns>四捨五入&intにキャストされた座標</returns>
    private Vector3 calcRoundingHalfUp(Vector3 pos)
    {
        Vector3 newPos;

        newPos.x = Mathf.RoundToInt(pos.x);
        newPos.y = Mathf.RoundToInt(pos.y);
        newPos.z = Mathf.RoundToInt(pos.z);

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
            return rotatePointArr[0]* Const.CUBE_SIZE_HALF;
        if(flag == Const.LEFT)
            return rotatePointArr[1]* Const.CUBE_SIZE_HALF;
        if(flag == Const.FORWARD)
            return rotatePointArr[2]* Const.CUBE_SIZE_HALF;
        if(flag == Const.BACK)
            return rotatePointArr[3]* Const.CUBE_SIZE_HALF;
        if(flag == Const.UP)
            return rotatePointArr[4]* Const.CUBE_SIZE_HALF;
        if(flag == Const.DOWN)
            return rotatePointArr[5]* Const.CUBE_SIZE_HALF;

        return Vector3.zero;
    }
    private Vector3 setRayAspect(int flag)
    {
        if(flag == Const.RIGHT)
            return rayAspectArr[0];
        if(flag == Const.LEFT)
            return rayAspectArr[1];
        if(flag == Const.FORWARD)
            return rayAspectArr[2];
        if(flag == Const.BACK)
            return rayAspectArr[3];
        
        return Vector3.zero;
    }

    private async UniTask rotateMove(BasePlayer tmpPlayer, CancellationTokenSource cts)
    {
        // 回転中のフラグを立てる
        isRotate = true;

        float sumAngle = 0f;    // angleの合計値を保存
        while(sumAngle < 90f)
        {
            if(cts.IsCancellationRequested)
                return;

            cubeAngle = 1f;     // 回転速度
            sumAngle += cubeAngle;

            // 90°以上回転しないように値を制限
            if(sumAngle > 90f)
            {
                cubeAngle -= sumAngle - 90f;
            }

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
        rotatePoint = rotatePointArr[6];
    }
    // 移動用変数初期化処理関数
    private void resetMoveValue(BasePlayer tmpPlayer)
    {
        moveFlag = -1;
        tmpPlayer.OnMove = false;
    }
}
