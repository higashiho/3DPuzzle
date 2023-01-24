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
        {   //プレイヤーが左上のエリアに着いたら、カメラが左上のステージの中心に動く
            if(InGameSceneController.Stages.StageState == Const.STATE_NEEDLE_STAGE)
            {
                tmpCamera.CameraMoveFlag = false;
                tmpCamera.camera.transform.DOMove(tmpCamera.StandCameraPos[0], Const.CAMERA_MOVE_SPEED).SetEase(Ease.OutSine);
            }
            if(InGameSceneController.Stages.StageState == Const.STATE_FALLING_STAGE)
            {
                tmpCamera.CameraMoveFlag = false;
                tmpCamera.camera.transform.DOMove(tmpCamera.StandCameraPos[1], Const.CAMERA_MOVE_SPEED).SetEase(Ease.OutSine);
            }
            if(InGameSceneController.Stages.StageState == Const.STATE_SWITCH_STAGE)
            {
                tmpCamera.CameraMoveFlag = false;
                tmpCamera.camera.transform.DOMove(tmpCamera.StandCameraPos[2], Const.CAMERA_MOVE_SPEED).SetEase(Ease.OutSine);
            }
            if(InGameSceneController.Stages.StageState == Const.STATE_MOVE_STAGE)
            {
                tmpCamera.CameraMoveFlag = false;
                tmpCamera.camera.transform.DOMove(tmpCamera.StandCameraPos[3], Const.CAMERA_MOVE_SPEED).SetEase(Ease.OutSine);
            }
            else
            {
                tmpCamera.CameraMoveFlag = false;
                tmpCamera.camera.transform.DOMove(tmpCamera.StandCameraPos[4], Const.CAMERA_MOVE_SPEED).SetEase(Ease.OutSine);
            }
        }
    }
}