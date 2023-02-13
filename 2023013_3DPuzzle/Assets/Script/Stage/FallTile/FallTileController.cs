using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Stage
{
    /// <summary>
    /// 落下タイルの挙動管理クラス
    /// </summary>
    public class FallTileController : BaseFallTile
    {
        // Start is called before the first frame update
        async void Start()
        {
            
            // ステージのブロックが読み込み終わるフラグが立つまで待つ
            await UniTask.WaitWhile(() => !InGameSceneController.Stages.StageBlockLoadFlag);
            
            // ステージ生成が終わるまで待つ
            await InGameSceneController.Stages.Handle.Task;

            FallTiles = GameObject.FindGameObjectsWithTag("Fall");
            FallTileMoves = new FallTileMove(this);
        }

        // Update is called once per frame
        void Update()
        {
            
            // 要素が増えていないときは処理を行わない
            if(FallTiles.Length == 0)
                return;

            FallTileMoves.TimeMoveAsync();
        }
    }
}