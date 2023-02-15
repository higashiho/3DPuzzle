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
             if(InGameSceneController.Player.PlayerFailureTween == null && InGameSceneController.Player.PlayerClearTween == null)
            {     
                
                setCamera(tmpCamera);

                ZomeIO(tmpCamera);
            }
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
        /// 初期化・カメラ初期地点設定
        /// </summary>
        /// <param name="tmpCamera">BaseCamera</param>
        public void CameraInsetance(BaseCamera tmpCamera)
        {
            // 初期回転位置を設定
            tmpCamera.DefoultRotation = tmpCamera.camera.transform.rotation;
            // カメラ初期座標
            tmpCamera.camera.transform.position = tmpCamera.StandCameraPos[Const.CENTER];
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
        /// カメラリセット
        /// </summary>
        /// <param name="tmpCamera">BaseCamera</param>
        /// <param name="num">各ステージの中央を指定する数字</param>
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
        /// カメラの移動
        /// </summary>
        /// <param name="tmpCamera">BaseCamera</param>
        /// <param name="num">移動tween</param>
        /// <param name="point">各ステージのカメラの位置の配列を指定する数字</param>
        private void MoveCamera(BaseCamera tmpCamera, int num, int point)
        {
            // まだ移動していないなら
            if(tmpCamera.CameraTween[num] == null)
            {
                var tmpTween = tmpCamera.camera.transform.DOMove(tmpCamera.StandCameraPos[point], Const.CAMERA_MOVE_SPEED)
                .SetEase(Ease.OutSine).OnStart(() => readyMoveCamera(tmpCamera)).OnComplete(() =>
                {
                    compReset(tmpCamera);
                } );
                startReset(tmpCamera, num, tmpTween);
            }
        }

        /// <summary>
        /// プレイヤーが中央に帰ってきたら中央に戻る
        /// </summary>
        public void followToPlayer(BaseCamera tmpCamera)
        {
            tmpCamera.camera.transform.rotation = tmpCamera.DefoultRotation;
            tmpCamera.camera.transform.position = tmpCamera.StandCameraPos[Const.CENTER];
            tmpCamera.camera.fieldOfView = Const.FIELD_OF_VIEW_DEFAULT;
        }

        /// <summary>
        /// カメラ移動
        /// </summary>
        /// <param name="tmpCamera">BaseCamera</param>
        private void setCamera(BaseCamera tmpCamera)
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
             if(tmpPlayerPos == tmpCamera.CameraMovePos[Const.LEFT_UP, Const.TO_CENTER_UP_POS]
             || tmpPlayerPos == tmpCamera.CameraMovePos[Const.LEFT_UP, Const.TO_CENTER_UNDER_POS])
            {
                MoveCamera(tmpCamera, Const.LEFT_UP, Const.LEFT_UP);
            }

            // プレイヤーが左上にいる時は、回転範囲が左上中心になる
            if(tmpPlayerPos.x <= tmpCamera.CameraRotateBorderLine[Const.LEFT_UP].x
            && tmpPlayerPos.z >= tmpCamera.CameraRotateBorderLine[Const.LEFT_UP].z)
            {
                RotateCamera(tmpCamera, Const.LEFT_UP);
                CameraReset(tmpCamera, Const.LEFT_UP);
            }
                
            // プレイヤーが右上の無敵エリアに着いたら、カメラが右上のステージの中心に動く-------------------------
             if(tmpPlayerPos == tmpCamera.CameraMovePos[Const.RIGHT_UP, Const.TO_CENTER_UP_POS]
             || tmpPlayerPos == tmpCamera.CameraMovePos[Const.RIGHT_UP, Const.TO_CENTER_UNDER_POS])
            {
                MoveCamera(tmpCamera, Const.RIGHT_UP, Const.RIGHT_UP);
            }

            if(tmpPlayerPos.x >= tmpCamera.CameraRotateBorderLine[Const.RIGHT_UP].x
            && tmpPlayerPos.z >= tmpCamera.CameraRotateBorderLine[Const.RIGHT_UP].z)
            {
                RotateCamera(tmpCamera, Const.RIGHT_UP);
                CameraReset(tmpCamera, Const.RIGHT_UP);
            }

            // プレイヤーが右下の無敵エリアに着いたら、カメラが右下のステージの中心に動く----------------------
             if(tmpPlayerPos == tmpCamera.CameraMovePos[Const.RIGHT_UNDER, Const.TO_CENTER_UP_POS]
             || tmpPlayerPos == tmpCamera.CameraMovePos[Const.RIGHT_UNDER, Const.TO_CENTER_UNDER_POS])
            {
                MoveCamera(tmpCamera, Const.RIGHT_UNDER, Const.RIGHT_UNDER);

            }

            if(tmpPlayerPos.x >= tmpCamera.CameraRotateBorderLine[Const.RIGHT_UNDER].x
            && tmpPlayerPos.z <= tmpCamera.CameraRotateBorderLine[Const.RIGHT_UNDER].z)
            {
                RotateCamera(tmpCamera, Const.RIGHT_UNDER);
                CameraReset(tmpCamera, Const.RIGHT_UNDER);
            }

            // プレイヤーが左下の無敵エリアに着いたら、カメラが左下のステージの中心に動く-------------------
             if(tmpPlayerPos == tmpCamera.CameraMovePos[Const.LEFT_UNDER, Const.TO_CENTER_UP_POS]
             || tmpPlayerPos == tmpCamera.CameraMovePos[Const.LEFT_UNDER, Const.TO_CENTER_UNDER_POS])
            {
                MoveCamera(tmpCamera, Const.LEFT_UNDER, Const.LEFT_UNDER);
            }

            if(tmpPlayerPos.x <= tmpCamera.CameraRotateBorderLine[Const.LEFT_UNDER].x
            && tmpPlayerPos.z <= tmpCamera.CameraRotateBorderLine[Const.LEFT_UNDER].z)
            {
                RotateCamera(tmpCamera, Const.LEFT_UNDER);
                CameraReset(tmpCamera, Const.LEFT_UNDER);
            }

            // プレイヤーが中央の四つ角のどこかに着いたら、カメラが中央のステージの中心に動く-------------------------
            if(tmpPlayerPos == tmpCamera.CameraCenterMovePos[Const.LEFT_UP]
            || tmpPlayerPos == tmpCamera.CameraCenterMovePos[Const.RIGHT_UP]
            || tmpPlayerPos == tmpCamera.CameraCenterMovePos[Const.RIGHT_UNDER]
            || tmpPlayerPos == tmpCamera.CameraCenterMovePos[Const.LEFT_UNDER])
            {
                MoveCamera(tmpCamera, Const.CENTER, Const.CENTER);
            }

            if(tmpPlayerPos.x >= tmpCamera.CameraCenterMovePos[Const.LEFT_UP].x
            && tmpPlayerPos.x <= tmpCamera.CameraCenterMovePos[Const.RIGHT_UP].x
            && tmpPlayerPos.z <= tmpCamera.CameraCenterMovePos[Const.LEFT_UP].z
            && tmpPlayerPos.z >= tmpCamera.CameraCenterMovePos[Const.RIGHT_UNDER].z)
            {
                RotateCamera(tmpCamera, Const.CENTER);
                CameraReset(tmpCamera, Const.CENTER);
            }
        }
    }
}