using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove
{
    /// <summary>
    /// 挙動関数
    /// </summary>
    /// <param name="tmpCamera"></param> カメラの実体
    public void Move(BaseCamera tmpCamera)
    {
        // キーを押したら表示していない方のカメラを表示
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(tmpCamera.Cameras[0].activeSelf)
            {
                tmpCamera.Cameras[0].SetActive(false);
                tmpCamera.Cameras[1].SetActive(true);
            }
            else
            {
                tmpCamera.Cameras[0].SetActive(true);
                tmpCamera.Cameras[1].SetActive(false);
            }
        }
    }
}
