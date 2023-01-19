using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Cam
{
    public class BaseCamera : MonoBehaviour
    {
        // 今実行しているtween
        protected Tween acttionTween = null;
        public Tween ActtinTween{get{return acttionTween;} set{acttionTween = value;}}
        // 右クリックしたときのマウスの座標
        public Vector3 originMousePos;
        // カメラの角度
        public Vector3 cameraAngle = new Vector3(0, 0, 0);
        // 見る相手
        public Quaternion CameraRotation;
        // 初期回転位置
        public Quaternion DefoultRotation;
        public new Camera camera;

        // インスタンス化
        protected CameraMove cameraMove = new CameraMove();
        protected CameraRotate cameraRotate = new CameraRotate();
    }
}