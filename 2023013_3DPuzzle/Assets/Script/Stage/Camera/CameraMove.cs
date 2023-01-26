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
            SetCamera(tmpCamera);

            rotateCamera(tmpCamera);

            ZomeIO(tmpCamera);

            CameraReset(tmpCamera);
        }

        private void SetCamera(BaseCamera tmpCamera)
        {   //プレイヤーが左上のエリアに着いたら、カメラが左上のステージの中心に動く
            if(InGameSceneController.Stages.StageState == Const.STATE_NEEDLE_STAGE)
            {
                tmpCamera.camera.transform.DOMove(tmpCamera.StandCameraPos[0], Const.CAMERA_MOVE_SPEED)
                .SetEase(Ease.OutSine);

                if(Input.GetMouseButton(1))
                {
                    tmpCamera.transform.RotateAround(InGameSceneController.Player.transform.position, tmpCamera.CAMERA_AXIS, 
                    360.0f / (1.0f / Const.CAMERA_MOVE_SPEED) * Time.deltaTime);
                }
                return;
            }
            if(InGameSceneController.Stages.StageState == Const.STATE_FALLING_STAGE)
            {
                tmpCamera.camera.transform.DOMove(tmpCamera.StandCameraPos[1], Const.CAMERA_MOVE_SPEED)
                .SetEase(Ease.OutSine);

                if(Input.GetMouseButton(1))
                {
                    tmpCamera.transform.RotateAround(InGameSceneController.Player.transform.position, tmpCamera.CAMERA_AXIS, 
                    360.0f / (1.0f / Const.CAMERA_MOVE_SPEED) * Time.deltaTime);
                }
                return;
            }
            if(InGameSceneController.Stages.StageState == Const.STATE_SWITCH_STAGE)
            {
                tmpCamera.camera.transform.DOMove(tmpCamera.StandCameraPos[2], Const.CAMERA_MOVE_SPEED)
                .SetEase(Ease.OutSine);

                if(Input.GetMouseButton(1))
                {
                    tmpCamera.transform.RotateAround(InGameSceneController.Player.transform.position, tmpCamera.CAMERA_AXIS, 
                    360.0f / (1.0f / Const.CAMERA_MOVE_SPEED) * Time.deltaTime);
                }
                return;
            }
            if(InGameSceneController.Stages.StageState == Const.STATE_MOVE_STAGE)
            {
                tmpCamera.camera.transform.DOMove(tmpCamera.StandCameraPos[3], Const.CAMERA_MOVE_SPEED)
                .SetEase(Ease.OutSine);

                if(Input.GetMouseButton(1))
                {
                    tmpCamera.transform.RotateAround(InGameSceneController.Player.transform.position, tmpCamera.CAMERA_AXIS, 
                    360.0f / (1.0f / Const.CAMERA_MOVE_SPEED) * Time.deltaTime);
                }
                return;
            }
                tmpCamera.camera.transform.DOMove(tmpCamera.StandCameraPos[4], Const.CAMERA_MOVE_SPEED)
                .SetEase(Ease.OutSine);

                if(Input.GetMouseButton(1))
                {
                    tmpCamera.transform.RotateAround(InGameSceneController.Player.transform.position, tmpCamera.CAMERA_AXIS, 
                    360.0f / (1.0f / Const.CAMERA_MOVE_SPEED) * Time.deltaTime);
                }
        }

        /// <summary>
        /// カメラを回転させる関数
        /// </summary>
        /// <param name="tmpCamera"></param>カメラの実体
        private void rotateCamera(BaseCamera tmpCamera)
        {

            

            // // 右クリックした瞬間
            // if(Input.GetMouseButtonDown(1))
            // {
            //     // （右クリックしたときの）マウスの座標を原点とする
            //     tmpCamera.originMousePos = Input.mousePosition;
            // }
            // // 右クリックしている間　かつ　特定の範囲内のみ
            // if(Input.GetMouseButton(1))
            // {
            //     // （右クリックしたときの）ローカル座標でのカメラのrotate
            //     tmpCamera.cameraAngle = tmpCamera.transform.localEulerAngles;
            //     // カメラの回転量
            //     //左右と上下が反転しているため、代入先もｘとｙ座標を反対側に代入している
            //     tmpCamera.cameraAngle.y += (Input.mousePosition.x - tmpCamera.originMousePos.x) * Const.ROTATE_CAMERA_SPEED;
            //     tmpCamera.cameraAngle.x += (Input.mousePosition.y - tmpCamera.originMousePos.y) * Const.ROTATE_CAMERA_SPEED;

            //     //カメラの回転の上限、ｙ座標であることに注意
            //     //360を超えられないため、直前の359にしておく
            //     if(tmpCamera.cameraAngle.x >= 90)
            //     {
            //         tmpCamera.cameraAngle.x = 90;
            //     }
            //     if(tmpCamera.cameraAngle.x <= 50)
            //     {
            //         tmpCamera.cameraAngle.x = 50;
            //     }
            //     // 増減した回転量を更新
            //     tmpCamera.transform.localEulerAngles = tmpCamera.cameraAngle;
            //     // 原点を更新、現在のマウスの座標にする
            //     tmpCamera.originMousePos = Input.mousePosition;
            // }
        }

        /// <summary>
        /// 初期化・カメラ初期地点設定
        /// </summary>
        /// <param name="tmpCamera"></param>
        public void CameraInsetance(BaseCamera tmpCamera)
        {
            tmpCamera.camera = Camera.main;               // メインカメラを探す
            tmpCamera.DefoultRotation = tmpCamera.gameObject.transform.rotation;// 初期位置を設定
        }

        /// <summary>
        /// ズームイン・ズームアウト関数
        /// </summary>
        /// <param name="tmpCamera"></param>
        private void ZomeIO(BaseCamera tmpCamera)
        {
            // マウスホイール回転量に係数をかけて数値に変換
            float zomeIn = Input.GetAxis("Mouse ScrollWheel") * Const.ZOME_POWER;
            // カメラの視野を狭くする　＝　ズームイン
                tmpCamera.camera.fieldOfView -= zomeIn;
            if(tmpCamera.camera.fieldOfView <= Const.UNDER_ROTATE_LIMIT)
                tmpCamera.camera.fieldOfView = Const.UNDER_ROTATE_LIMIT;
            if(tmpCamera.camera.fieldOfView >= Const.OVER_ROTATE_LIMIT)
                tmpCamera.camera.fieldOfView = Const.OVER_ROTATE_LIMIT;
        }

        /// <summary>
        /// カメラリセット関数
        /// </summary>
        /// <param name="tmpCamera"></param>
        private void CameraReset(BaseCamera tmpCamera)
        {
            // カメラリセット(マウスホイールクリック)
            if(Input.GetMouseButtonDown(2))
            {
                tmpCamera.gameObject.transform.rotation = tmpCamera.DefoultRotation;
                tmpCamera.camera.fieldOfView = Const.FIELD_OF_VIEW_DEFAULT;
            }
        }
    }
}