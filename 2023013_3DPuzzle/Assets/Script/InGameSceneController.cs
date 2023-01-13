using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneController : MonoBehaviour
{
    // 取得用
    // ステージ
    public static BaseStage Stages;

    // Start is called before the first frame update
    void Awake() 
    {
        Stages = GameObject.FindWithTag("Stage").GetComponent<BaseStage>();
    }
}
