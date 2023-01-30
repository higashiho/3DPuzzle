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

            //rotateCamera(tmpCamera);

            ZomeIO(tmpCamera);

            CameraReset(tmpCamera);
        }

        /// <summary>
        /// DoTweenのOnComplete内の内容
        /// </summary>
        /// <param name="tmpCamera"></param>
        private void compReset(BaseCamera tmpCamera)
        {
            // 偶数丸めで正確な値を求める
            var tmpPos = tmpCamera.camera.transform.position;
            tmpPos.x = Mathf.RoundToInt(tmpPos.x);
            tmpPos.y = Mathf.RoundToInt(tmpPos.y);
            tmpPos.z = Mathf.RoundToInt(tmpPos.z);
            tmpCamera.camera.transform.position = tmpPos;
            // カメラを移動できるようにするフラグ
            tmpCamera.MoveCameraFlag = true;
            Debug.Log("comp");
            // カメラの初期位置を決める
            tmpCamera.DefoultRotation = tmpCamera.camera.transform.rotation;
            // DoMoveの重複防止
            tmpCamera.CameraTween = null;
        }

        /// <summary>
        /// カメラを移動させる準備-
        /// カメラを回転させなくする・リセット座標更新・視野リセット
        /// </summary>
        /// <param name="tmpCamera"></param>
        private void readyMoveCamera(BaseCamera tmpCamera)
        {
            tmpCamera.MoveCameraFlag = false;
            tmpCamera.camera.transform.rotation = tmpCamera.DefoultRotation;
            tmpCamera.camera.fieldOfView = Const.FIELD_OF_VIEW_DEFAULT;
            // switch(InGameSceneController.Stages.StageState)
            // {
            //     case InGameSceneController.Stages.StageState:
            // }
        }

        /// <summary>
        /// カメラ移動
        /// </summary>
        /// <param name="tmpCamera"></param>
        private void SetCamera(BaseCamera tmpCamera)
        {   // プレイヤーの位置を偶数丸めで正確にする
            var tmpPlayerPos = InGameSceneController.Player.transform.position;
            tmpPlayerPos.x = Mathf.RoundToInt(tmpPlayerPos.x);
            tmpPlayerPos.y = Mathf.RoundToInt(tmpPlayerPos.y);
            tmpPlayerPos.z = Mathf.RoundToInt(tmpPlayerPos.z);

            // プレイヤーが左上の無敵エリアに着いたら、カメラが右上のステージの中心に動く-------------------------
             if(tmpPlayerPos == tmpCamera.CameraMovePos[0,0]
             || tmpPlayerPos == tmpCamera.CameraMovePos[0,1])
            {
                readyMoveCamera(tmpCamera);
                if(tmpCamera.CameraTween == null)
                {
                    tmpCamera.DefoultCameraPosition = 0;
                    tmpCamera.CameraTween = tmpCamera.camera.transform.DOMove(tmpCamera.StandCameraPos[0], Const.CAMERA_MOVE_SPEED)
                    .SetEase(Ease.OutSine).OnComplete(() =>
                    {
                        compReset(tmpCamera);
                    } );
                }
            }

            if(tmpPlayerPos.x <= tmpCamera.CameraMovePos[0,1].x
            && tmpPlayerPos.z >= tmpCamera.CameraMovePos[0,0].z)
            {
                // 右クリックしながら移動したマウスの大きさによって
                // カメラを各ステージの中心を軸に回転
                if(Input.GetMouseButton(1) && tmpCamera.MoveCameraFlag)
                {
                    // マウスの移動量
                    float InputMouseX = Input.GetAxis("Mouse X");
                    tmpCamera.transform.RotateAround(tmpCamera.AxisCamera[0], tmpCamera.CAMERA_AXIS, 
                    InputMouseX * Time.deltaTime * Const.MOVE_AROUND_SPEED);
                }
            }
                
            // プレイヤーが右上の無敵エリアに着いたら、カメラが右上のステージの中心に動く-------------------------
             if(tmpPlayerPos == tmpCamera.CameraMovePos[1,0]
             || tmpPlayerPos == tmpCamera.CameraMovePos[1,1])
            {
                readyMoveCamera(tmpCamera);
                if(tmpCamera.CameraTween == null)
                tmpCamera.camera.transform.DOMove(tmpCamera.StandCameraPos[1], Const.CAMERA_MOVE_SPEED)
                .SetEase(Ease.OutSine).OnComplete(() => 
                {
                    compReset(tmpCamera);
                } );
                tmpCamera.DefoultCameraPosition = 1;
            }

            if(tmpPlayerPos.x >= tmpCamera.CameraMovePos[1,1].x
            && tmpPlayerPos.z >= tmpCamera.CameraMovePos[1,0].z)
            {
                // 右クリックしながら移動したマウスの大きさによって
                // カメラを各ステージの中心を軸に回転
                if(Input.GetMouseButton(1) && tmpCamera.MoveCameraFlag)
                {
                    // マウスの移動量
                    float InputMouseX = Input.GetAxis("Mouse X");
                    tmpCamera.transform.RotateAround(tmpCamera.AxisCamera[1], tmpCamera.CAMERA_AXIS, 
                    InputMouseX * Time.deltaTime * Const.MOVE_AROUND_SPEED);
                }
            }

            // プレイヤーが右下の無敵エリアに着いたら、カメラが右下のステージの中心に動く----------------------
             if(tmpPlayerPos == tmpCamera.CameraMovePos[2,0]
             || tmpPlayerPos == tmpCamera.CameraMovePos[2,1])
            {
                readyMoveCamera(tmpCamera);
                if(tmpCamera.CameraTween == null)
                tmpCamera.camera.transform.DOMove(tmpCamera.StandCameraPos[2], Const.CAMERA_MOVE_SPEED)
                .SetEase(Ease.OutSine).OnComplete(() => 
                {
                    compReset(tmpCamera);
                } );
                tmpCamera.DefoultCameraPosition = 2;
            }

            if(tmpPlayerPos.x >= tmpCamera.CameraMovePos[2,0].x
            && tmpPlayerPos.z <= tmpCamera.CameraMovePos[2,1].z)
            {
                // 右クリックしながら移動したマウスの大きさによって
                // カメラを各ステージの中心を軸に回転
                if(Input.GetMouseButton(1) && tmpCamera.MoveCameraFlag)
                {
                    // マウスの移動量
                    float InputMouseX = Input.GetAxis("Mouse X");
                    tmpCamera.transform.RotateAround(tmpCamera.AxisCamera[0], tmpCamera.CAMERA_AXIS, 
                    InputMouseX * Time.deltaTime * Const.MOVE_AROUND_SPEED);
                }
            }

            // プレイヤーが左下の無敵エリアに着いたら、カメラが左下のステージの中心に動く-------------------
             if(tmpPlayerPos == tmpCamera.CameraMovePos[3,0]
             || tmpPlayerPos == tmpCamera.CameraMovePos[3,1])
            {
                readyMoveCamera(tmpCamera);
                if(tmpCamera.CameraTween == null)
                tmpCamera.camera.transform.DOMove(tmpCamera.StandCameraPos[3], Const.CAMERA_MOVE_SPEED)
                .SetEase(Ease.OutSine).OnComplete(() => 
                {
                    compReset(tmpCamera);
                } );
                tmpCamera.DefoultCameraPosition = 3;
            }

            if(tmpPlayerPos.x <= tmpCamera.CameraMovePos[3,0].x
            && tmpPlayerPos.z <= tmpCamera.CameraMovePos[3,1].z)
            {
                // 右クリックしながら移動したマウスの大きさによって
                // カメラを各ステージの中心を軸に回転
                if(Input.GetMouseButton(1) && tmpCamera.MoveCameraFlag)
                {
                    // マウスの移動量
                    float InputMouseX = Input.GetAxis("Mouse X");
                    tmpCamera.transform.RotateAround(tmpCamera.AxisCamera[3], tmpCamera.CAMERA_AXIS, 
                    InputMouseX * Time.deltaTime * Const.MOVE_AROUND_SPEED);
                }
            }
            // プレイヤーが中央の四つ角のどこかに着いたら、カメラが中央のステージの中心に動く-------------------------
            if(tmpPlayerPos == tmpCamera.CameraCenterMovePos[0]
            || tmpPlayerPos == tmpCamera.CameraCenterMovePos[1]
            || tmpPlayerPos == tmpCamera.CameraCenterMovePos[2]
            || tmpPlayerPos == tmpCamera.CameraCenterMovePos[3])
            {
                readyMoveCamera(tmpCamera);
                if(tmpCamera.CameraTween == null)
                tmpCamera.camera.transform.DOMove(tmpCamera.StandCameraPos[4], Const.CAMERA_MOVE_SPEED)
                .SetEase(Ease.OutSine).OnComplete(() => 
                {
                    compReset(tmpCamera);
                } );
                tmpCamera.DefoultCameraPosition = 4;
            }

            if(tmpPlayerPos.x >= tmpCamera.CameraCenterMovePos[0].x
            && tmpPlayerPos.x <= tmpCamera.CameraCenterMovePos[1].x
            && tmpPlayerPos.z <= tmpCamera.CameraCenterMovePos[0].z
            && tmpPlayerPos.z >= tmpCamera.CameraCenterMovePos[2].z)
            {
                // 右クリックしながら移動したマウスの大きさによって
                // カメラを各ステージの中心を軸に回転
                if(Input.GetMouseButton(1) && tmpCamera.MoveCameraFlag)
                {
                    // マウスの移動量
                    float InputMouseX = Input.GetAxis("Mouse X");
                    tmpCamera.transform.RotateAround(tmpCamera.AxisCamera[4], tmpCamera.CAMERA_AXIS, 
                    InputMouseX * Time.deltaTime * Const.MOVE_AROUND_SPEED);
                }
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
            tmpCamera.DefoultRotation = tmpCamera.camera.transform.rotation;// 初期回転位置を設定
            tmpCamera.transform.position = tmpCamera.StandCameraPos[4];
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
            if(Input.GetMouseButtonDown(2) && tmpCamera.MoveCameraFlag)
            {
                //回転を初期化
                tmpCamera.camera.transform.rotation = tmpCamera.DefoultRotation;
                //視野リセット
                tmpCamera.camera.fieldOfView = Const.FIELD_OF_VIEW_DEFAULT;
                //位置を戻す
                tmpCamera.camera.transform.position = tmpCamera.StandCameraPos[tmpCamera.DefoultCameraPosition];
            }
        }
    }
}