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

        public void SetCamera(BaseCamera tmpCamera)
        {   //プレイヤーが右上のエリアに着いたら、カメラがステージの中心に動く
            if(InGameSceneController.Player.transform.position == tmpCamera.PlayerIntoEareaPos[0])
            {
                tmpCamera.CameraMoveFlag = false;
                tmpCamera.camera.transform.DOMove(tmpCamera.StandCameraPos[0], Const.CAMERA_MOVE_SPEED).SetEase(Ease.OutSine);
            }
            else if(InGameSceneController.Player.transform.position == tmpCamera.PlayerIntoEareaPos[1])
            {
                tmpCamera.CameraMoveFlag = false;
                tmpCamera.camera.transform.DOMove(tmpCamera.StandCameraPos[1], Const.CAMERA_MOVE_SPEED).SetEase(Ease.OutSine);
            }
            else if(InGameSceneController.Player.transform.position == tmpCamera.PlayerIntoEareaPos[2])
            {
                tmpCamera.CameraMoveFlag = false;
                tmpCamera.camera.transform.DOMove(tmpCamera.StandCameraPos[2], Const.CAMERA_MOVE_SPEED).SetEase(Ease.OutSine);
            }
            else if(InGameSceneController.Player.transform.position == tmpCamera.PlayerIntoEareaPos[3])
            {
                tmpCamera.CameraMoveFlag = false;
                tmpCamera.camera.transform.DOMove(tmpCamera.StandCameraPos[3], Const.CAMERA_MOVE_SPEED).SetEase(Ease.OutSine);
            }
        }
    }
}