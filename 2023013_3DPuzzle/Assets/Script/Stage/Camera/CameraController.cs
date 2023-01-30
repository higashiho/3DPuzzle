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
            cameraMove.CameraInsetance(this);
        }

        // Update is called once per frame
        void LateUpdate()
        {
            cameraMove.Move(this);
        }
    }
}