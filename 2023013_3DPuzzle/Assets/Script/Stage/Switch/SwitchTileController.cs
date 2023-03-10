using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tile;
using Cysharp.Threading.Tasks;

namespace Stage
{
    /// <summary>
    /// スイッチタイルの挙動管理クラス
    /// </summary>
    public class SwitchTileController : BaseSwitchTile
    {
        async void Start() 
        {
            
            // ステージのブロックが読み込み終わるフラグが立つまで待つ
            await UniTask.WaitWhile(() => !InGameSceneController.Stages.StageBlockLoadFlag);

            // ステージ生成が終わるまで待つ
            await InGameSceneController.Stages.Handle.Task;
            SwutchTiles = GameObject.FindGameObjectsWithTag("SwitchTile");

            switchTileMove = new SwitchTileMove(this);
        }

        void Update()
        {
            // ステートがスイッチステージではないとき初期化
            if(InGameSceneController.Stages.StageState != StageConst.STATE_SWITCH_STAGE && ResetFlag)
            {
                Debug.Log("初期化");
                // 初期化
                switchTileMove.SwitchTileReset();
            }
        }
    }
}

