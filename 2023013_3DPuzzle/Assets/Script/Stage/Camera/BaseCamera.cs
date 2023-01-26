using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cam
{
    public class BaseCamera : MonoBehaviour
    {
        //各ステージのカメラの座標
        public Vector3[] StandCameraPos{get; private set;} =   //各ステージの定点カメラ
        {
            new Vector3(0, 80, 130),         //左上
            new Vector3(150, 80, 130),       //右上
            new Vector3(150, 80, -110),      //右下
            new Vector3(25, 80, -40),          //左下
            new Vector3(55, 55, 40),         //中央
        };
        // // 右クリックしたときのマウスの座標
        // public Vector3 originMousePos;
        // // カメラの角度
        // public Vector3 cameraAngle;
        // 初期回転位置
        public Quaternion DefoultRotation;
        public new Camera camera;
        //カメラの回転軸
        public Vector3 CAMERA_AXIS = Vector3.up;

        // インスタンス化
        protected CameraMove cameraMove = new CameraMove();
    }
}