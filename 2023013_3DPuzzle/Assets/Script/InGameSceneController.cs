using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stage;
using Box;
using UI;
using Enemy;

/// <summary>
/// インゲームでのオブジェクト管理クラス
/// </summary>
public class InGameSceneController : MonoBehaviour
{
    // 取得用
    /// <summary>
    /// ステージ
    /// </summary>
    public static BaseStage Stages{get; private set;}
    public static BaseFallTile FallTile{get; private set;}
    public static BaseSwitchTile SwitchTile{get; private set;}
    public static BaseMoveStage MoveStage{get; private set;}
    public static BaseBox TreasureBox{get; private set;}
    public static BaseEnemy Enemy{get; private set;}
    public static EnemyManagerController EnemyManager{get; private set;}

    /// <summary>
    /// 宝箱UI
    /// </summary>
    public static BaseTreasureBoxUI TreasureBoxUI{get;private set;}

    /// <summary>
    /// Player
    /// </summary>
    public static BasePlayer Player{get; private set;}

    // Start is called before the first frame update
    void Awake() 
    {
        Stages = GameObject.FindWithTag("Stage").GetComponent<BaseStage>();
        Player = GameObject.FindWithTag("Player").GetComponent<BasePlayer>();
        FallTile = GameObject.FindWithTag("FallTiles").GetComponent<BaseFallTile>();
        SwitchTile = GameObject.FindWithTag("SwitchTiles").GetComponent<BaseSwitchTile>();
        MoveStage = GameObject.FindWithTag("MoveStage").GetComponent<BaseMoveStage>();
        TreasureBox = GameObject.FindWithTag("GoalTile").transform.GetChild(0).GetComponent<BaseBox>();

        EnemyManager = GameObject.FindWithTag("Enemy").GetComponent<EnemyManagerController>();
        Enemy = EnemyManager.transform.GetChild(0).GetComponent<BaseEnemy>();

        TreasureBoxUI = GameObject.FindWithTag("UI").GetComponent<BaseTreasureBoxUI>();


        // Tweenの最大メモリ初期化
        DG.Tweening.DOTween.SetTweensCapacity(tweenersCapacity:1000, sequencesCapacity:250);
    }

}
