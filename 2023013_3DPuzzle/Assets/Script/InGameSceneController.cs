using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stage;

<<<<<<< .merge_file_a22432
public class InGameSceneController : MonoBehaviour
{
    // 取得用
    // ステージ
    public static BaseStage Stages;

    // Player
=======
/// <summary>
/// インゲームでのオブジェクト管理クラス
/// </summary>
public class InGameSceneController : MonoBehaviour
{
    // 取得用
    /// <summary>
    /// ステージ
    /// </summary>
    public static BaseStage Stages;

    /// <summary>
    /// Player
    /// </summary>
>>>>>>> .merge_file_a12824
    public static BasePlayer Player;

    // Start is called before the first frame update
    void Awake() 
    {
        Stages = GameObject.FindWithTag("Stage").GetComponent<BaseStage>();
        Player = GameObject.FindWithTag("Player").GetComponent<BasePlayer>();
    }

}
