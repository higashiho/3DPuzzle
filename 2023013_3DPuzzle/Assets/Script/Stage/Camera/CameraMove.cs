using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMove
{
    /// <summary>
    /// 挙動関数
    /// </summary>
    /// <param name="tmpCamera">カメラの実体</param> 
    public void Move(BaseCamera tmpCamera)
    {
        var tmpPos = tmpCamera.transform.position;                            //カメラの座標
        var tmpPlayerPos = InGameSceneController.Player.transform.position;   //プレイヤーの座標
        
        var tmpNewPos = new Vector3(tmpPlayerPos.x, tmpPos.y,tmpPlayerPos.z); //カメラの位置計算
        tmpCamera.transform.position = tmpNewPos;                             //カメラの位置更新

        //要修正　かつ　Tweenがnullの時に入る(多重tween防止用)
    //     if(tmpCamera.ActtinTween == null)
    //     {
    //         tmpCamera.ActtinTween = tmpCamera.transform.DOMove(
    //             tmpPlayerPos,                                                   //プレイヤーの位置に
    //             InGameSceneController.Player.PlayersData.PlayerMoveTime         //時間をかけて移動
    //         ).SetEase(Ease.OutSine).OnComplete( () =>
    //         {
    //             reset(tmpCamera);
    //         }
    //         );
    //     }
    // }

    // /// <summary>
    // /// 初期化関数
    // /// </summary>
    // /// <param name="tmpCamera">カメラの実体</param>
    // private void reset(BaseCamera tmpCamera)
    // {
    //     tmpCamera.ActtinTween = null;
       }
}
