using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;

public class PlayerMove 
{
    private Vector3 rotatePoint = Vector3.zero; // 回転中心
    private Vector3 rotateAxis;
    private Vector3[] rotateAxisArr =   // 回転軸
    {
        new Vector3(0, 0, -1),      // 右
        new Vector3(0, 0, 1),       // 左
        new Vector3(1, 0, 0),       // 上
        new Vector3(-1, 0, 0),      // 下
        Vector3.zero                // ０
    };
    private float cubeAngle = 0f;               // 回転角度
    private int moveCount = 0;  // 移動回数
    private float cubeSizeHalf = 2.5f;     // キューブ半分のサイズ
    private bool isRotate = false;  // 回転中に立つフラグ(タスク) 
    private uint moveFlag = 0;       

    public async void Move(BasePlayer tmpPlayer)
    {
        Vector3 pos = tmpPlayer.transform.position;                 // 自身の座標取得
        
        if(isRotate)
            return;
        
        
        if(Input.GetMouseButtonDown(0) )
        {
            Vector3? movePos = tmpPlayer.ChooseObj?.transform.position;   // 移動先の座標取得
            
                // 右
            if(movePos != null)
            {
                var tmpMovePos = (Vector3)movePos;  // 移動先の座標取得(キャスト用)
                if((pos.x < tmpMovePos.x && (int)Mathf.Round(pos.z) == tmpMovePos.z))
                {
                    moveCount = (int)((tmpMovePos.x - pos.x) / (cubeSizeHalf * 2));    // 移動回数設定
                    moveFlag |= Const.RIGHT;
                        //rotatePoint = new Vector3(cubeSizeHalf, -cubeSizeHalf, 0f);
                        rotateAxis = rotateAxisArr[0];

                }
                // 左
                if((pos.x > tmpMovePos.x && (int)Mathf.Round(pos.z) == tmpMovePos.z))
                {
                    moveCount = (int)((pos.x - tmpMovePos.x) / (cubeSizeHalf * 2));
                    moveFlag |= Const.LEFT;
                    //rotatePoint = new Vector3(-cubeSizeHalf, -cubeSizeHalf, 0f);
                    rotateAxis = rotateAxisArr[1];
                    
                }
                // 上
                if(((int)Mathf.Round(pos.x) == tmpMovePos.x && pos.z < tmpMovePos.z))
                {
                    moveCount = (int)((tmpMovePos.z - pos.z) / (cubeSizeHalf * 2));
                    moveFlag |= Const.FORWARD;
                        //rotatePoint = new Vector3(0f, -cubeSizeHalf, cubeSizeHalf);
                        rotateAxis = rotateAxisArr[2];
                    
                }
                // 下
                if((Mathf.Round(pos.x) == tmpMovePos.x && pos.z > tmpMovePos.z))
                {
                    moveCount = (int)((pos.z - tmpMovePos.z) / (cubeSizeHalf * 2));
                    moveFlag |= Const.BACK;
                    //rotatePoint = new Vector3(0f, -cubeSizeHalf, -cubeSizeHalf);
                    rotateAxis = rotateAxisArr[3];
                    
                }
                
            }
            
        }

        for(int i = 0; i < moveCount; i++)
        {
            rotatePoint = tmpPlayer.transform.position + setRotatePointOffset(moveFlag);
            await rotateMove(tmpPlayer, tmpPlayer.GetCancellationTokenOnDestroy());
        }   
        
        
        moveCount = 0;
        moveFlag &= 0;
        
        
    }
    private Vector3 setRotatePointOffset(uint moveFlag)
    {
        Vector3 point;
        switch(moveFlag)
        {
            case Const.RIGHT:
                point = new Vector3(cubeSizeHalf, -cubeSizeHalf, 0f);
                return point;
                
            case Const.LEFT:
                point = new Vector3(-cubeSizeHalf, -cubeSizeHalf, 0f);
                return point;
            
            case Const.FORWARD:
                point = new Vector3(0f, -cubeSizeHalf, cubeSizeHalf);
                return point;
            
            case Const.BACK:
                point = new Vector3(0f, -cubeSizeHalf, -cubeSizeHalf);
                return point;
            default:
                break;
                
        }
        return Vector3.zero;
    }
    // public Vector3 SetRotatePoint(BasePlayer tmpPlayer)
    // {
    //     Vector3 rotatePos;
    //     rotatePos = 
    // }
    private async UniTask rotateMove(BasePlayer tmpPlayer, CancellationToken cancellation_token )
    {
        
        // 回転中のフラグを立てる
        isRotate = true;

        // 回転処理
        
        float sumAngle = 0f;    // angleの合計を保存
            
        while(sumAngle < 90f)
        {
            cubeAngle = 3f;    // 回転速度
            sumAngle += cubeAngle;

            // 90°以上回転しないように値を制限
            if(sumAngle > 90f)
            {
                cubeAngle -= sumAngle - 90f;
            }
            
            tmpPlayer.transform.RotateAround(rotatePoint, rotateAxis, cubeAngle);

            await UniTask.Yield(PlayerLoopTiming.Update, cancellation_token);
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
