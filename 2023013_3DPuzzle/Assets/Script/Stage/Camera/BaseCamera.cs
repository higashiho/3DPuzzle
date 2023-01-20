using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BaseCamera : MonoBehaviour
{
    //今実行しているtween
    protected Tween acttionTween = null;
    public Tween ActtinTween{get{return acttionTween;} set{acttionTween = value;}}


    // インスタンス化
    protected CameraMove cameraMove = new CameraMove();
}
