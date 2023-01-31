using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Cam
{
    public class BaseCamera : MonoBehaviour
    {
        /// <summary>
        /// 各ステージのカメラの座標
        /// </summary>
        /// <value></value>
        public Vector3[] StandCameraPos{get; private set;} =
        {
            new Vector3(25, 80, 65),         //左上
            new Vector3(95, 80, 65),         //右上
            new Vector3(95, 80, -15),        //右下
            new Vector3(25, 80, -15),        //左下
            new Vector3(55, 55, 40),         //中央
        };

        /// <summary>
        /// 各ステージのカメラの回転軸
        /// </summary>
        /// <value></value>
        public Vector3[] AxisCamera{get; private set;} =
        {
            new Vector3(25, 0, 95),         //左上
            new Vector3(95, 0, 95),         //右上
            new Vector3(95, 0, 25),         //右下
            new Vector3(25, 0, 25),         //左下
            new Vector3(60, 0, 60),         //中央
        };

        /// <summary>
        /// カメラが移動する座標判定（ヨツハシ）
        /// </summary>
        /// <value></value>
        public Vector3[,] CameraMovePos{get; private set;} =
        {
            {new Vector3(50, 5, 75), new Vector3(45, 5, 70)},         //左上(上,下)
            {new Vector3(70, 5, 75), new Vector3(75, 5, 70)},         //右上
            {new Vector3(75, 5, 50), new Vector3(70, 5, 45)},         //右下
            {new Vector3(45, 5, 50), new Vector3(50, 5, 45)},         //左下
        };

        /// <summary>
        /// カメラが移動する座標判定（中央）
        /// </summary>
        /// <value></value>
        public Vector3[] CameraCenterMovePos{get; private set;} =
        {
            new Vector3(50, 5, 70),         // 左上
            new Vector3(70, 5, 70),         // 右上
            new Vector3(70, 5, 50),         // 右下
            new Vector3(50, 5, 50),         // 左下
        };

        /// <summary>
        /// カメラが移動できるようになる範囲の境界線
        /// </summary>
        /// <value></value>
        public Vector3[] CameraRotateBorderLine{get; private set;} =
        {
            new Vector3(52.5f, 5, 67.5f),   //左上
            new Vector3(67.5f, 5, 67.5f),   //右上
            new Vector3(67.5f, 5, 52.5f),   //右下
            new Vector3(52.5f, 5, 52.5f),   //左下
        };
        // // 右クリックしたときのマウスの座標
        // public Vector3 originMousePos;
        // // カメラの角度
        // public Vector3 cameraAngle;
        [Header("初期回転量")]
        protected Quaternion defoultRotation;
        public Quaternion DefoultRotation{get{return defoultRotation;}set{defoultRotation = value;}}
        // メインカメラ
        public new Camera camera;
        [SerializeField, Header("カメラのリセット位置を指定する数")]
        protected int defoultCameraPosition;
        public int DefoultCameraPosition{get{return defoultCameraPosition;}set{defoultCameraPosition = value;}}
        [SerializeField, Header("カメラが動けるようになるフラグ")]
        protected bool moveCameraFlag;
        public bool MoveCameraFlag{get{return moveCameraFlag;}set{moveCameraFlag = value;}}

        protected Tween[] cameraTween = new Tween[5]
        {
            null, null, null, null, null,
        };
        public Tween[] CameraTween{get{return cameraTween;}set{cameraTween = value;}}
        
        /// <summary>
        /// カメラの回転軸
        /// </summary>
        public Vector3 CAMERA_AXIS = Vector3.up;

        // インスタンス化
        protected CameraMove cameraMove = new CameraMove();
    }
}