using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stage;

public class InGameSceneController : MonoBehaviour
{
    // 取得用
    // ステージ
    public static BaseStage Stages;

    // Player
    public static BasePlayer Player;

    // Start is called before the first frame update
    void Awake() 
    {
        Stages = GameObject.FindWithTag("Stage").GetComponent<BaseStage>();
        Player = GameObject.FindWithTag("Player").GetComponent<BasePlayer>();
    }

}
