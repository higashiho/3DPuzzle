using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;

public class PlayerMove 
{
    private Vector3 rotatePoint = Vector3.zero; // 回転中心
    private Vector3 rotateAxis = Vector3.zero;  // 回転軸
    private float cubeAngle = 0f;               // 回転角度
    private int moveCount = 0;  // 移動回数
    private float cubeSizeHalf = 2.5f;     // キューブ半分のサイズ
    private bool isRotate = false;  // 回転中に立つフラグ(タスク)        

    public async void Move(BasePlayer tmpPlayer)
    {
        Vector3 pos = tmpPlayer.transform.position;                 // 自身の座標取得
        
        if(isRotate)
            return;
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 movePos = tmpPlayer.ChooseObj.transform.position;   // 移動先の座標取得
            
                // 右
            if((pos.x < movePos.x && pos.z == movePos.z))
            {
                moveCount = (int)((movePos.x - pos.x) / (cubeSizeHalf * 2));    // 移動回数設定
                
                for(int i = 0; i < moveCount; i++)
                {
                    rotatePoint = tmpPlayer.transform.position + new Vector3(cubeSizeHalf, -cubeSizeHalf, 0f);
                    rotateAxis = new Vector3 (0, 0, -1);
                }

            }
            // 左
            if((pos.x > movePos.x && pos.z == movePos.z))
            {
                moveCount = (int)((pos.x - movePos.x) / cubeSizeHalf * 2);
                for(int i = 0; i < moveCount; i++)
                {
                    rotatePoint = tmpPlayer.transform.position + new Vector3(-cubeSizeHalf, -cubeSizeHalf, 0f);
                    rotateAxis = new Vector3(0, 0, 1);
                }
            }
            // 上
            if((pos.x == movePos.x && pos.z < movePos.z))
            {
                Debug.Log("a");
                moveCount = (int)((movePos.z - pos.z) / (cubeSizeHalf * 2));
                for(int i = 0; i < moveCount; i++)
                {
                    rotatePoint = tmpPlayer.transform.position + new Vector3(0f, -cubeSizeHalf, cubeSizeHalf);
                    rotateAxis = new Vector3(1, 0, 0);
                }
            }
            // 下
            if((pos.x == movePos.x && pos.z > movePos.z))
            {
                moveCount = (int)((pos.z - movePos.z) / (cubeSizeHalf * 2));
                for(int i = 0; i < moveCount; i++)
                {
                    rotatePoint = tmpPlayer.transform.position + new Vector3(0f, -cubeSizeHalf, -cubeSizeHalf);
                    rotateAxis = new Vector3 (-1, 0, 0);
                }
            }
        }
         
        
       

        if (rotatePoint == Vector3.zero)
			return;

        await rotateMove(tmpPlayer, tmpPlayer.GetCancellationTokenOnDestroy());
        
    }
    
    public async UniTask rotateMove(BasePlayer tmpPlayer, CancellationToken cancellation_token )
    {
        // 回転中のフラグを立てる
        isRotate = true;

        // 回転処理
        float sumAngle = 0f;    // angleの合計を保存
        while(sumAngle < 90f)
        {
            cubeAngle = 15f;    // 回転速度
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
        rotateAxis = Vector3.zero;

        return;
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
