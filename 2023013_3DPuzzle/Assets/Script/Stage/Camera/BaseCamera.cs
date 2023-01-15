using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCamera : MonoBehaviour
{
    [SerializeField, Header("カメラ")]
    protected GameObject[] cameras = new GameObject[2];
    public GameObject[] Cameras{get{return cameras;}private set{cameras = value;}}

    // インスタンス化
    protected CameraMove cameraMove = new CameraMove();
}
