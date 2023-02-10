using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage
{
    /// <summary>
    /// 針ステージ挙動管理クラス
    /// </summary>
    public class NeedleController : BaseNeedle
    {
        // Start is called before the first frame update
        async void Start()
        {

            // ステージ生成が終わるまで待つ
            await InGameSceneController.Stages.Handle.Task;
            // ニードル取得
            NeedleTiles = GameObject.FindGameObjectsWithTag("Needle");

            needleMove = new NeedleMove(this);
        }

        // Update is called once per frame
        void Update()
        {
            // 要素が増えていないときは処理を行わない
            if(NeedleTiles.Length == 0)
                return;
            needleMove.Move();
        }
    }
}