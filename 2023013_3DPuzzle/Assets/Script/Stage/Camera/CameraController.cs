using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : BaseCamera
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        cameraMove.Move(this);
    }
}
