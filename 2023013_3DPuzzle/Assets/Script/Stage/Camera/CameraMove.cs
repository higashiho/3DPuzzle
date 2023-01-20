using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Cam
{
    public class CameraMove
    {
        /// <summary>
        /// カメラの挙動関数
        /// </summary>
        /// <param name="tmpCamera">カメラの実体</param> 
        public void Move(BaseCamera tmpCamera)
        {
            var tmpPos = tmpCamera.transform.position;                            //カメラの座標
            var tmpPlayerPos = InGameSceneController.Player.transform.position;   //プレイヤーの座標
            
            var tmpNewPos = new Vector3(tmpPlayerPos.x, tmpPos.y,tmpPlayerPos.z); //カメラの位置計算
            tmpCamera.transform.position = tmpNewPos;                             //カメラの位置更新
        }
    }
}