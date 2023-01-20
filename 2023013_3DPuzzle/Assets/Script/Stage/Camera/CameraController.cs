using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cam
{
    public class CameraController : BaseCamera
    {
        // Start is called before the first frame update
        void Start()
        {
            camera = Camera.main;               //メインカメラを探す
            cameraRotate.CameraTween(this);
            NowAngle = camera.transform.localEulerAngles;       //カメラの現在回転位置を設定
            PrimaryAngle = camera.transform.localEulerAngles;   //カメラの回転変化量（今後これに足していく）
        }

        // Update is called once per frame
        void LateUpdate()
        {
            cameraMove.Move(this);

            cameraRotate.rotateCamera(this);

            cameraRotate.ZomeIO(this);

            cameraRotate.CameraReset(this);

            cameraRotate.RimitAngle(this);
        }
    }
}