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
        void Start()
        {
            // ステージ生成
            instance.StageMaking(this, filePath);

        }

        // Update is called once per frame
        void Update()
        {
            stageMove.StateUpdate(this);
        }
    }
}