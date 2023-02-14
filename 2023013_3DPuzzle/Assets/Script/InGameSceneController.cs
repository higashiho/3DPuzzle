using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stage;
using Box;
using UI;
using Enemy;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

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
    async void Awake() 
    {
        Application.targetFrameRate = 60;
        // 初期取得
        Stages = GameObject.FindWithTag("Stage").GetComponent<BaseStage>();
        Player = GameObject.FindWithTag("Player").GetComponent<BasePlayer>();
        FallTile = GameObject.FindWithTag("FallTiles").GetComponent<BaseFallTile>();
        SwitchTile = GameObject.FindWithTag("SwitchTiles").GetComponent<BaseSwitchTile>();
        MoveStage = GameObject.FindWithTag("MoveStage").GetComponent<BaseMoveStage>();
        EnemyManager = GameObject.FindWithTag("Enemy").GetComponent<EnemyManagerController>();
        Enemy = EnemyManager.transform.GetChild(0).GetComponent<BaseEnemy>();
        TreasureBoxUI = GameObject.FindWithTag("UI").GetComponent<BaseTreasureBoxUI>();

        
        // ステージのブロックが読み込み終わるフラグが立つまで待つ
        await UniTask.WaitWhile(() => !Stages.StageBlockLoadFlag);

        // ステージ生成が終わるまで待機
        await Stages.Handle.Task;
        TreasureBox = GameObject.FindWithTag("GoalTile").transform.GetChild(0).GetComponent<BaseBox>();

        

        // Tweenの最大メモリ初期化
        DG.Tweening.DOTween.SetTweensCapacity(tweenersCapacity:1500, sequencesCapacity:1000);
    }

    void OnDestroy()
    {
        
        // ステージのハンドル開放
        Addressables.Release(Stages.Handle);
        for(int i = 0; i < Stages.StageBlockHandle.Length; i++)
        {
            Addressables.Release(Stages.StageBlockHandle[i]);
        }
        Addressables.Release(Stages.PuzzleStageWallHandle);
    }
}
