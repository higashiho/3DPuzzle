using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Cam
{
    public class CameraMove
    {
        /// <summary>
        /// カメラの挙動をまとめた関数
        /// </summary>
        /// <param name="tmpCamera">BaseCamera</param> 
        public void Move(BaseCamera tmpCamera)
        {       
            SetCamera(tmpCamera);

            ZomeIO(tmpCamera);
        }

        /// <summary>
        /// DoTweenのOnComplete内の内容
        /// カメラ移動フラグをオン・リセット座標更新
        /// </summary>
        /// <param name="tmpCamera">BaseCamera</param>
        private void compReset(BaseCamera tmpCamera)
        {
            // カメラを移動できるようにするフラグ
            tmpCamera.MoveCameraFlag = true;
            // カメラの初期位置を決める
            tmpCamera.DefoultRotation = tmpCamera.camera.transform.rotation;
        }

        /// <summary>
        /// Tween配列を初期化
        /// </summary>
        /// <param name="tmpCamera">BaseCamera</param>
        /// <param name="num">Tween配列の指定をする数字</param>
        /// <param name="tmpTween">カメラが各ステージの中央に移動するTween</param>
        private void startReset(BaseCamera tmpCamera, int num, Tween tmpTween)
        {
            // Tween初期化
            for(int i = 0; i < tmpCamera.CameraTween.Length; i++)
            {
                tmpCamera.CameraTween[i] = null;
            }
            tmpCamera.CameraTween[num] = tmpTween;
        }

        /// <summary>
        /// カメラを移動させる準備-
        /// カメラを回転させなくする・リセット座標更新・視野リセット
        /// </summary>
        /// <param name="tmpCamera">BaseCamera</param>
        private void readyMoveCamera(BaseCamera tmpCamera)
        {
            tmpCamera.MoveCameraFlag = false;
            tmpCamera.camera.transform.rotation = tmpCamera.DefoultRotation;
            tmpCamera.camera.fieldOfView = Const.FIELD_OF_VIEW_DEFAULT;
        }

        /// <summary>
        /// カメラ移動
        /// </summary>
        /// <param name="tmpCamera">BaseCamera</param>
        private void SetCamera(BaseCamera tmpCamera)
        {   
            // プレイヤーの位置を偶数丸めで正確にする
            var tmpPlayerPos = InGameSceneController.Player.transform.position;
            tmpPlayerPos.x = Mathf.RoundToInt(tmpPlayerPos.x);
            tmpPlayerPos.y = Mathf.RoundToInt(tmpPlayerPos.y);
            tmpPlayerPos.z = Mathf.RoundToInt(tmpPlayerPos.z);

            
            var tmpPos = tmpCamera.camera.transform.position;
            // 偶数丸めで正確な値を求める
            tmpPos.x = Mathf.RoundToInt(tmpPos.x);
            tmpPos.y = Mathf.RoundToInt(tmpPos.y);
            tmpPos.z = Mathf.RoundToInt(tmpPos.z);

            // プレイヤーが左上の無敵エリアに着いたら、カメラが左上のステージの中心に動く-------------------------
             if(tmpPlayerPos == tmpCamera.CameraMovePos[0,0]
             || tmpPlayerPos == tmpCamera.CameraMovePos[0,1])
            {
                MoveCamera(tmpCamera, 0, 0);
            }

            if(tmpPlayerPos.x <= tmpCamera.CameraRotateBorderLine[0].x
            && tmpPlayerPos.z >= tmpCamera.CameraRotateBorderLine[0].z)
            {
                RotateCamera(tmpCamera, 0);
                CameraReset(tmpCamera, 0);
            }
                
            // プレイヤーが右上の無敵エリアに着いたら、カメラが右上のステージの中心に動く-------------------------
             if(tmpPlayerPos == tmpCamera.CameraMovePos[1,0]
             || tmpPlayerPos == tmpCamera.CameraMovePos[1,1])
            {
                MoveCamera(tmpCamera, 1, 1);
            }

            if(tmpPlayerPos.x >= tmpCamera.CameraRotateBorderLine[1].x
            && tmpPlayerPos.z >= tmpCamera.CameraRotateBorderLine[1].z)
            {
                RotateCamera(tmpCamera, 1);
                CameraReset(tmpCamera, 1);
            }

            // プレイヤーが右下の無敵エリアに着いたら、カメラが右下のステージの中心に動く----------------------
             if(tmpPlayerPos == tmpCamera.CameraMovePos[2,0]
             || tmpPlayerPos == tmpCamera.CameraMovePos[2,1])
            {
                MoveCamera(tmpCamera, 2, 2);

            }

            if(tmpPlayerPos.x >= tmpCamera.CameraRotateBorderLine[2].x
            && tmpPlayerPos.z <= tmpCamera.CameraRotateBorderLine[2].z)
            {
                RotateCamera(tmpCamera, 2);
                CameraReset(tmpCamera, 2);
            }

            // プレイヤーが左下の無敵エリアに着いたら、カメラが左下のステージの中心に動く-------------------
             if(tmpPlayerPos == tmpCamera.CameraMovePos[3,0]
             || tmpPlayerPos == tmpCamera.CameraMovePos[3,1])
            {
                MoveCamera(tmpCamera, 3, 3);
            }

            if(tmpPlayerPos.x <= tmpCamera.CameraRotateBorderLine[3].x
            && tmpPlayerPos.z <= tmpCamera.CameraRotateBorderLine[3].z)
            {
                RotateCamera(tmpCamera, 3);
                CameraReset(tmpCamera, 3);
            }

            // プレイヤーが中央の四つ角のどこかに着いたら、カメラが中央のステージの中心に動く-------------------------
            if(tmpPlayerPos == tmpCamera.CameraCenterMovePos[0]
            || tmpPlayerPos == tmpCamera.CameraCenterMovePos[1]
            || tmpPlayerPos == tmpCamera.CameraCenterMovePos[2]
            || tmpPlayerPos == tmpCamera.CameraCenterMovePos[3])
            {
                MoveCamera(tmpCamera, 4, 4);
            }

            if(tmpPlayerPos.x >= tmpCamera.CameraCenterMovePos[0].x
            && tmpPlayerPos.x <= tmpCamera.CameraCenterMovePos[1].x
            && tmpPlayerPos.z <= tmpCamera.CameraCenterMovePos[0].z
            && tmpPlayerPos.z >= tmpCamera.CameraCenterMovePos[2].z)
            {
                RotateCamera(tmpCamera, 4);
                CameraReset(tmpCamera, 4);
            }
        }

        /// <summary>
        /// 初期化・カメラ初期地点設定
        /// </summary>
        /// <param name="tmpCamera">BaseCamera</param>
        public void CameraInsetance(BaseCamera tmpCamera)
        {
            tmpCamera.DefoultRotation = tmpCamera.camera.transform.rotation;// 初期回転位置を設定
            tmpCamera.camera.transform.position = tmpCamera.StandCameraPos[4];
        }

        /// <summary>
        /// ズームイン・ズームアウト関数
        /// </summary>
        /// <param name="tmpCamera">BaseCamera</param>
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
        /// <param name="tmpCamera">BaseCamera</param>
        /// <param name="num">各ステージの中央を指定する数字
        private void CameraReset(BaseCamera tmpCamera, int num)
        {
            // カメラリセット(マウスホイールクリック)
            if(Input.GetMouseButtonDown(2) && tmpCamera.MoveCameraFlag)
            {
                //回転を初期化
                tmpCamera.camera.transform.rotation = tmpCamera.DefoultRotation;
                //視野リセット
                tmpCamera.camera.fieldOfView = Const.FIELD_OF_VIEW_DEFAULT;
                //位置を戻す
                tmpCamera.camera.transform.position = tmpCamera.StandCameraPos[num];
            }
        }

        /// <summary>
        /// カメラの回転
        /// </summary>
        /// <param name="tmpCamera"></param>
        /// <param name="num">各ステージの回転軸を指定する数字</param>
        private void RotateCamera(BaseCamera tmpCamera, int num)
        {
            // 右クリックしながら移動したマウスの大きさによって
            // カメラを各ステージの中心を軸に回転
            if(Input.GetMouseButton(1) && tmpCamera.MoveCameraFlag)
            {
                // マウスの移動量
                float InputMouseX = Input.GetAxis("Mouse X");
                tmpCamera.transform.RotateAround(tmpCamera.AxisCamera[num], tmpCamera.CAMERA_AXIS, 
                InputMouseX * Time.deltaTime * Const.MOVE_AROUND_SPEED);
            }
        }

        /// <summary>
        /// カメラ移動<=カメラリセット・配列の初期化・移動準備
        /// </summary>
        /// <param name="tmpCamera">BaseCamera</param>
        /// <param name="num">Tweenを指定する数字</param>
        /// <param name="point">各ステージの中央を指定する数字</param>
        private void MoveCamera(BaseCamera tmpCamera, int num, int point)
        {
            if(tmpCamera.CameraTween[num] == null)
            {
                Debug.Log(tmpCamera.StandCameraPos[point]);
                var tmpTween = tmpCamera.camera.transform.DOMove(tmpCamera.StandCameraPos[point], Const.CAMERA_MOVE_SPEED)
                .SetEase(Ease.OutSine).OnStart(() => readyMoveCamera(tmpCamera)).OnComplete(() =>
                {
                    compReset(tmpCamera);
                } );
                startReset(tmpCamera, num, tmpTween);
            }
        }
    }
}