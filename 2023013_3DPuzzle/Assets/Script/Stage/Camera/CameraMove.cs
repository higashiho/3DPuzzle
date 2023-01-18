using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove
{
    /// <summary>
    /// 挙動関数
    /// </summary>
    /// <param name="tmpCamera">カメラの実体</param> 
    public void Move(BaseCamera tmpCamera)
    {
        var tmpPos = tmpCamera.transform.position;
        var tmpPlayerPos = InGameSceneController.Player.transform.position;

        var tmpNewPos = new Vector3(tmpPlayerPos.x, tmpPos.y,tmpPos.z);
        tmpCamera.transform.position = tmpNewPos;
    }
}
