using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Cam
{
    public class CameraRotate
    {
        /// <summary>
        /// カメラを回転させる関数
        /// </summary>
        /// <param name="tmpCamera"></param>カメラの実体
        public void rotateCamera(BaseCamera tmpCamera)
        {
            //右クリックした瞬間
            if(Input.GetMouseButtonDown(1))
            {
                // （右クリックしたときの）ローカル座標でのカメラの回転量
                tmpCamera.cameraAngle = tmpCamera.transform.localEulerAngles;
                // （右クリックしたときの）マウスの座標を原点とする
                tmpCamera.originMousePos = Input.mousePosition;
            }
            // 右クリックしている間
            else if(Input.GetMouseButton(1))
            {
                // カメラの回転量を増減
                tmpCamera.cameraAngle.y -= (Input.mousePosition.x - tmpCamera.originMousePos.x) * 0.1f;
                tmpCamera.cameraAngle.x -= (Input.mousePosition.y - tmpCamera.originMousePos.y) * 0.1f;
                // 増減した回転量を更新
                tmpCamera.gameObject.transform.localEulerAngles = tmpCamera.cameraAngle;
                // 原点を更新、現在のマウスの座標にする
                tmpCamera.originMousePos = Input.mousePosition;
            }
            
            // 特定の方向を向く
            // if(Input.GetMouseButton(0))
            // {   // プレイヤーのほうを向く
            //     tmpCamera.CameraRotation = 
            //     Quaternion.LookRotation(InGameSceneController.Player.transform.position - tmpCamera.transform.position);
            //     // カメラをグローバル座標の角度量を、0.5sかけてカメラの角度だけ回転する。
            //     // 回転が終わったらデバッグログを出す
            //     tmpCamera.transform.DORotateQuaternion(tmpCamera.CameraRotation, 0.5f)
            //     .SetEase(Ease.OutQuad).OnComplete(()=> Debug.Log("Finished"));
            // }
        }

        /// <summary>
        /// 初期化・カメラ初期地点設定
        /// </summary>
        /// <param name="tmpCamera"></param>
        public void CameraTween(BaseCamera tmpCamera)
        {
            // 初期化　（再利用するか、処理中に破棄されたときにセーフモードにするか、エラーの出し方）
            DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
            tmpCamera.DefoultRotation = tmpCamera.gameObject.transform.rotation;
        }

        /// <summary>
        /// ズームイン・ズームアウト関数
        /// </summary>
        /// <param name="tmpCamera"></param>
        public void ZomeIO(BaseCamera tmpCamera)
        {
            // マウスホイール回転量に係数をかけて数値に変換
            float zomeIn = Input.GetAxis("Mouse ScrollWheel") * Const.ZOME_POWER;
            //カメラの視野を狭くする　＝　ズームイン
            tmpCamera.camera.fieldOfView -= zomeIn;
        }

        /// <summary>
        /// カメラリセット関数
        /// </summary>
        /// <param name="tmpCamera"></param>
        public void CameraReset(BaseCamera tmpCamera)
        {
            // カメラリセット(マウスホイールクリック)
            if(Input.GetMouseButtonDown(2))
            {
                tmpCamera.gameObject.transform.rotation = tmpCamera.DefoultRotation;
            }
        }

        /// <summary>
        /// カメラの移動範囲制限関数
        /// </summary>
        /// <param name="tmpCamera"></param>
        public void RimitAngle(BaseCamera tmpCamera)
        {
            if(Input.GetMouseButton(1))
            {
            //マウスの移動量を代入
            tmpCamera.NowAngle.y += Input.GetAxis("Mouse X");
            tmpCamera.NowAngle.x -= Input.GetAxis("Mouse Y");
            }

            //マウスの移動量が、基点から設定された数だけ移動したら、移動量を増やさないようにする
            if(tmpCamera.NowAngle.y <= tmpCamera.PrimaryAngle.y - Const.LIMIT_CAMERA_ANGLE_Y)
            {
                tmpCamera.NowAngle.y = tmpCamera.PrimaryAngle.y - Const.LIMIT_CAMERA_ANGLE_Y;
            }
            if(tmpCamera.NowAngle.y >= tmpCamera.PrimaryAngle.y + Const.LIMIT_CAMERA_ANGLE_Y)
            {
                tmpCamera.NowAngle.y = tmpCamera.PrimaryAngle.y + Const.LIMIT_CAMERA_ANGLE_Y;
            }

            if(tmpCamera.NowAngle.x <= tmpCamera.PrimaryAngle.x - Const.LIMIT_CAMERA_ANGLE_X)
            {
                tmpCamera.NowAngle.x = tmpCamera.PrimaryAngle.x - Const.LIMIT_CAMERA_ANGLE_X;
            }
            if(tmpCamera.NowAngle.x >= tmpCamera.PrimaryAngle.x + Const.LIMIT_CAMERA_ANGLE_X)
            {
                tmpCamera.NowAngle.x = tmpCamera.PrimaryAngle.x + Const.LIMIT_CAMERA_ANGLE_X;
            }
        }
    }
}