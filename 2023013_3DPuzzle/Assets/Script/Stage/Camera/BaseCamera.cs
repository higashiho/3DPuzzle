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
            new Vector3(0, 50, 120),         //左上
            new Vector3(150, 50, 120),         //右上
            new Vector3(150, 50, -90),         //右下
            new Vector3(25, 50, -10),         //左下
            new Vector3(55, 55, 30),         //中央
        };

        // 右クリックしたときのマウスの座標
        public Vector3 originMousePos;
        // カメラの角度
        public Vector3 cameraAngle;
        // 初期回転位置
        public Quaternion DefoultRotation;
        public new Camera camera;
        //カメラがプレイヤーに追従するかのフラグ
        public bool CameraMoveFlag = true;
        // インスタンス化
        protected CameraMove cameraMove = new CameraMove();
        protected CameraRotate cameraRotate = new CameraRotate();
    }
}