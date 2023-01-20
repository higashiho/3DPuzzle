using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cam
{
    public class BaseCamera : MonoBehaviour
    {
        //カメラの四方の座標
        public Vector3[] StandCameraPos{get; private set;} =   //各ステージの定点カメラ
        {
            new Vector3(25, 50, 95),         //左上
            new Vector3(95, 50, 45),         //右上
            new Vector3(95, 50, 25),         //右下
            new Vector3(25, 50, 25),         //左下
        };

        //カメラが始点変更になるトリガー座標
        public Vector3[] PlayerIntoEareaPos{get; private set;} =   //プレイヤーが各ステージに入った判定がある座標
        {
            new Vector3(50, 5, 70),         //左上
            new Vector3(70, 5, 70),         //右上
            new Vector3(70, 5, 50),         //右下
            new Vector3(50, 5, 50)          //左下
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