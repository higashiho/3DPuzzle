using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tile;

namespace Stage
{
    /// <summary>
    /// スイッチタイルの挙動管理クラス
    /// </summary>
    public class SwitchTileController : BaseSwitchTile
    {
        void Start() 
        {
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

