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
        // 回転中またはブロックが移動中ならInput受け付けない
        if(isRotate || InGameSceneController.Stages.Moving)    
            return;

        input(tmpPlayer);
        tmpPlayer.OnMove = true;
        
        // moveCountの数だけループ
        for(int i = 0; i < moveCount; i++)
        {
            rotatePoint = setRotate(tmpPlayer, moveFlag);   // 回転の中心と軸を設定
            await rotateMove(tmpPlayer, tmpPlayer.GetCancellationTokenOnDestroy()); // 回転
        }   
        
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
    /// 何マス分移動するかの計算もこの中でする(calcBoxNum()を呼ぶ)
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
                Vector3 tmpMovePos = (Vector3)movePos;  // 移動先の座標取得(キャスト用)
                var tmpMovePosX = Mathf.RoundToInt(tmpMovePos.x);
                var tmpMovePosZ = Mathf.RoundToInt(tmpMovePos.z);
                var tmpPosX = Mathf.RoundToInt(pos.x);
                var tmpPosZ = Mathf.RoundToInt(pos.z);
                // 右
                if((tmpPosX < tmpMovePosX && tmpPosZ == tmpMovePosZ))
                {
                    moveFlag |= Const.RIGHT;            // 移動方向フラグを立てる(右)
                    calcBoxNum(pos.x, tmpMovePos.x);    // 何マス分の移動か求める
                }   

                // 左
                if((tmpPosX > tmpMovePosX && tmpPosZ == tmpMovePosZ))
                {
                    moveFlag |= Const.LEFT;             // 移動方向フラグを立てる(左)
                    calcBoxNum(pos.x, tmpMovePos.x);    // 何マス分の移動か求める
                }
                    
                // 上
                if((tmpPosX == tmpMovePosX && tmpPosZ < tmpMovePosZ))
                {
                    moveFlag |= Const.FORWARD;          // 移動方向フラグを立てる(上)
                    calcBoxNum(pos.z, tmpMovePos.z);    // 何マス分の移動か求める
                }
                // 下
                if((tmpPosX == tmpMovePosX && tmpPosZ > tmpMovePosZ))
                {
                    moveFlag |= Const.BACK;             // 移動方向フラグを立てる(下)
                    calcBoxNum(pos.z, tmpMovePos.z);    // 何マス分の移動か求める
                }    
            }
        }
    }

    /// <summary>
    /// 移動するタイルの数を求める関数(moveCountに代入)
    /// ２つのオブジェクトの距離を測ってプレイヤーのサイズで割る
    /// </summary>
    /// <param name="point"></param>比較する座標(自身)
    /// <param name="otherPoint"></param>比較する座標(移動先のオブジェクト)
    private void calcBoxNum(float point, float otherPoint)
    {
        moveCount = (int)((Mathf.Abs(point - otherPoint)) / (cubeSizeHalf * 2));
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
            case Const.RIGHT:
                point = tmpPlayer.transform.position + new Vector3(cubeSizeHalf, -cubeSizeHalf, 0f);
                rotateAxis = rotateAxisArr[0];
                return point;
                
            case Const.LEFT:
                point = tmpPlayer.transform.position + new Vector3(-cubeSizeHalf, -cubeSizeHalf, 0f);
                rotateAxis = rotateAxisArr[1];
                return point;
            
            case Const.FORWARD:
                point = tmpPlayer.transform.position + new Vector3(0f, -cubeSizeHalf, cubeSizeHalf);
                rotateAxis = rotateAxisArr[2];
                return point;
            
            case Const.BACK:
                point = tmpPlayer.transform.position + new Vector3(0f, -cubeSizeHalf, -cubeSizeHalf);
                rotateAxis = rotateAxisArr[3];
                return point;
            default:
                break;
                
        }
        return Vector3.zero;
    }
    
    private async UniTask rotateMove(BasePlayer tmpPlayer, CancellationToken cancellation_token )
    {
        
        // 回転中のフラグを立てる
        isRotate = true;

        // 回転処理
        
        float sumAngle = 0f;    // angleの合計を保存
            
        while(sumAngle < 90f)
        {
            cubeAngle = 1f;    // 回転速度
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
