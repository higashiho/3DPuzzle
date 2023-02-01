using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stage;
using Tile;

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

        // Tweenの最大メモリ初期化
        DG.Tweening.DOTween.SetTweensCapacity(tweenersCapacity:1000, sequencesCapacity:100);
    }

}
