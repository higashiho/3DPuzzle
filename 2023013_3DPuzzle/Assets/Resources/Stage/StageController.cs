using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage
{
    /// <summary>
    /// ステージの生成管理クラス
    /// </summary>
    public class StageController : BaseStage
    {
        // Start is called before the first frame update
        void Awake()
        {
            // ステージ生成
            instance.StageMaking(this, filePath);

            // オブジェクト配列取得
            KeyTiles = GameObject.FindGameObjectsWithTag("KeyTile");
            WallTiles = GameObject.FindGameObjectsWithTag("WallTile");
        }

        // Update is called once per frame
        void Update()
        {
            stageMove.StateUpdate(this);
        }
    }
}