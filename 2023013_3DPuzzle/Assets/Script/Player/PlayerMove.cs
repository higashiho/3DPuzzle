using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

// X, Z方向の移動回数を求める
// RayをとばしてBlockにHitすればY座標を１マス分Y方向に移動させる(回転)
/// <summary>
/// プレイヤーの移動制御クラス
/// </summary>
public class PlayerMove 
{
    private Vector3 rotatePoint = Vector3.zero; // 回転中心
    private Vector3 rotateAxis;
    private Vector3[] rotateAxisArr =   // 回転軸
    {
        new Vector3(0, 0, -1),      // 右
        new Vector3(0, 0, 1),       // 左
        new Vector3(1, 0, 0),       // 前
        new Vector3(-1, 0, 0),      // 後
        Vector3.zero                // ０
    };
    private float cubeAngle = 0f;       // 回転角度
    private int moveCount = 0;          // 移動回数
    private bool isRotate = false;      // 回転中に立つフラグ(タスク) 
    private uint moveFlag = 0;       
    
    public async void Move(BasePlayer tmpPlayer, CancellationTokenSource cts)
    {
        // 回転中またはブロックが移動中ならInput受け付けない
        if(isRotate || InGameSceneController.Stages.Moving)    
            return;

        input(tmpPlayer);
        tmpPlayer.OnMove = true;
        
        try
        {
        // moveCountが2以下の間ループ
        for(int i = 0; i < moveCount; i++)
        {
            rotatePoint = setRotate(tmpPlayer, moveFlag);   // 回転の中心と軸を設定
            await rotateMove(tmpPlayer, cts); // 回転
        }   
        }
        catch(OperationCanceledException)
        {
            Debug.Log("キャンセルされた");
            throw;
        }
        if(cts.IsCancellationRequested)
            return;
        resetMoveValue(tmpPlayer);
        
    }

    /// <summary>
    /// Move()の変数を初期化する関数
    /// </summary>
    /// <param name="tmpPlayer"></param>Playerの実体
    private void resetMoveValue(BasePlayer tmpPlayer)
    {
        var tmpPlayerPos = tmpPlayer.transform.position;
        var tmpPosX = Mathf.RoundToInt(tmpPlayerPos.x);
        var tmpPosY = Mathf.RoundToInt(tmpPlayerPos.y);
        var tmpPosZ = Mathf.RoundToInt(tmpPlayerPos.z);
        var tmpNewPos = new Vector3(tmpPosX, tmpPosY, tmpPosZ);
        tmpPlayer.transform.position = tmpNewPos;
        moveCount = 0;  // 回転回数0
        moveFlag &= 0;  // moveFlag全部折る
        tmpPlayer.OnMove = false;
    }
    
    /// <summary>
    /// プレイヤーの座標と移動先のタイルの座標を比較して移動する方向(十字)のフラグを立てる関数
    /// 何マス分移動するかの計算もこの中でする(BasePlayer.calcBoxNum()を呼ぶ)
    /// </summary>
    /// <param name="tmpPlayer"></param>プレイヤーの実体
    private void input(BasePlayer tmpPlayer)
    {
        Vector3 pos = tmpPlayer.transform.position;                 // 自身の座標取得
        
        // 左クリックが押されたら
        if(Input.GetMouseButton(0))
        {
            Vector3? movePos = tmpPlayer.ChooseObj?.transform.position;   // 移動先の座標取得
            
            if(movePos != null)
            {
                tmpPlayer.Needle.PlyaerMoveCount++;
                Vector3 tmpMovePos = (Vector3)movePos;  // 移動先の座標取得(キャスト用)

                // 移動先の座標をintに変換
                var tmpMovePosX = Mathf.RoundToInt(tmpMovePos.x);
                var tmpMovePosY = Mathf.RoundToInt(tmpMovePos.y);
                var tmpMovePosZ = Mathf.RoundToInt(tmpMovePos.z);

                // 自身の座標をintに変換
                var tmpPosX = Mathf.RoundToInt(pos.x);
                var tmpPosY = Mathf.RoundToInt(pos.y);
                var tmpPosZ = Mathf.RoundToInt(pos.z);
                // X(正)
                if((tmpPosX < tmpMovePosX) && (tmpPosZ == tmpMovePosZ))
                {
                    moveFlag |= Const.RIGHT;            // 移動方向フラグを立てる(正)
                    calcMoveCount(tmpPosX, tmpMovePosX);
                }   
                // X(負)
                else if((tmpPosX > tmpMovePosX) && (tmpPosZ == tmpMovePosZ))
                {
                    moveFlag |= Const.LEFT;             // 移動方向フラグを立てる(負)
                    calcMoveCount(tmpPosX, tmpMovePosX);
                }
                    
                // Z(正)
                if((tmpPosX == tmpMovePosX) && (tmpPosZ < tmpMovePosZ))
                {
                    moveFlag |= Const.FORWARD;          // 移動方向フラグを立てる(上)
                    calcMoveCount(tmpPosZ, tmpMovePosZ);
                }
                // Z(負)
                if((tmpPosX == tmpMovePosX) && (tmpPosZ > tmpMovePosZ))
                {
                    moveFlag |= Const.BACK;             // 移動方向フラグを立てる(下)
                    calcMoveCount(tmpPosZ, tmpMovePosZ);
                }    
            }
        }
    }


    /// <summary>
    /// 移動するタイルの数を求める関数
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="otherPos"></param>
    private void calcMoveCount(float pos, float otherPos)
    {
        moveCount = ((int)(Mathf.Abs(pos - otherPos)) / (int)(Const.CUBE_SIZE_HALF * 2));
        if(moveCount >= 2)
        {
            moveCount = 2;
        }
    }

    /// <summary>
    /// 回転軸と回転中心を設定する関数
    /// </summary>
    /// <param name="tmpPlayer"></param>プレイヤーの実体
    /// <param name="moveFlag"></param>移動方向フラグ(十字)
    /// <returns></returns>回転中心
    private Vector3 setRotate(BasePlayer tmpPlayer ,uint moveFlag)
    {
        Vector3 point;
        switch(moveFlag)
        {
            // X(正)
            case Const.RIGHT:
                point = tmpPlayer.transform.position + new Vector3(Const.CUBE_SIZE_HALF, -Const.CUBE_SIZE_HALF, 0f);
                rotateAxis = rotateAxisArr[0];
                return point;

            // X(負)
            case Const.LEFT:
                point = tmpPlayer.transform.position + new Vector3(-Const.CUBE_SIZE_HALF, -Const.CUBE_SIZE_HALF, 0f);
                rotateAxis = rotateAxisArr[1];
                return point;

            // Z(正)
            case Const.FORWARD:
                point = tmpPlayer.transform.position + new Vector3(0f, -Const.CUBE_SIZE_HALF, Const.CUBE_SIZE_HALF);
                rotateAxis = rotateAxisArr[2];
                return point;
            // Z(負)
            case Const.BACK:
                point = tmpPlayer.transform.position + new Vector3(0f, -Const.CUBE_SIZE_HALF, -Const.CUBE_SIZE_HALF);
                rotateAxis = rotateAxisArr[3];
                return point;
            default:
                break;

            
                
        }
        return Vector3.zero;
    }
    
    private async UniTask rotateMove(BasePlayer tmpPlayer, CancellationTokenSource cts)
    {
        
        // 回転中のフラグを立てる
        isRotate = true;

        // 回転処理
        
        float sumAngle = 0f;    // angleの合計を保存
            
        while(sumAngle < 90f)
        {
            //if(cancellation_token.IsCancellationRequested)
            //    return;

            
            cubeAngle = 1f;    // 回転速度
            sumAngle += cubeAngle;

            // 90°以上回転しないように値を制限
            if(sumAngle > 90f)
            {
                cubeAngle -= sumAngle - 90f;
            }
            
            tmpPlayer.transform.RotateAround(rotatePoint, rotateAxis, cubeAngle);

            
            try
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            catch(OperationCanceledException)
            {
                Debug.Log("キャンセルされました");
                throw;
            }
            if(cts.IsCancellationRequested)
                return;
        }

        // 回転中のフラグを倒す
        isRotate = false;
        rotatePoint = Vector3.zero;
        //rotateAxis = rotateAxisArr[4];
     
    }
    // public async void Move(BasePlayer tmpPlayer)
    // {
    //     // 目的地初期化(最初のプレイヤーの位置)
    //     Vector3 destination = new Vector3(0,1.3f,0);
    //     //bool setDestination = false;()
    //     if(Input.GetMouseButtonDown(0) && tmpPlayer.ChooseObj)
    //     {
    //         destination = tmpPlayer.ChooseObj.transform.position + new Vector3(0, Const.PLAYER_POSY, 0);
    //         //setDestination = true;
    //         var tmpPlayerTween = tmpPlayer.transform.DOMove(destination, tmpPlayer.PlayersData.PlayerMoveTime).SetEase(Ease.Linear)
    //         .OnStart(() => startMove(tmpPlayer));

    //         await tmpPlayerTween.AsyncWaitForCompletion();
    //         tmpPlayer.MoveCounter.GetComponent<MoveCounter>().MoveCount++;
    //         compMove(tmpPlayer);
    //     }

           
    // }

    // private void startMove(BasePlayer tmpPlayer)
    // {
    //     tmpPlayer.ChooseObj = null;
    //     tmpPlayer.OnMove = true;
    // }

    // private void compMove(BasePlayer tmpPlayer)
    // {
    //     tmpPlayer.OnMove = false;
    // }
}
