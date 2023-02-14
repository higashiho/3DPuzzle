using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Stage
{
    /// <summary>
    /// ステージの生成管理クラス
    /// </summary>
    public class StageController : BaseStage
    {
        // Start is called before the first frame update
        async void Awake()
        {
            instance = new InstanceStage(this);
            // ステージのロード
            for(int i = 0; i < StageBlockHandle.Length; i++)
            {
                StageBlockHandle[i] = StageBlockDataAssetRef[i].LoadAssetAsync<GameObject>();

                await StageBlockHandle[i].Task;
            }

            // ブロック読み込みが終わったフラグを立てる
            StageBlockLoadFlag = true;

            // アセット参照でのロード
            Handle = csvDataAssetRef.LoadAssetAsync<TextAsset>();
            
            // 読み込み終了時のイベントハンドラー処理代入
            Handle.Completed += instance.StageMaking;
            // ロード終了まで待機
            await Handle.Task;


            // パズルステージでの壁生成
            // アセット参照でのロード
            PuzzleStageWallHandle = puzzleStageWallDataAssetRef.LoadAssetAsync<GameObject>();
            await PuzzleStageWallHandle.Task;
            Instantiate<GameObject>((GameObject)PuzzleStageWallHandle.Result,
             parent:TileParemt.transform);

            
            // オブジェクト配列取得
            KeyTiles = GameObject.FindGameObjectsWithTag("KeyTile");
            WallTiles = GameObject.FindGameObjectsWithTag("WallTile");

            PopupStartPos = GetNumPopupText.transform.parent.localPosition;
        }

        // Update is called once per frame
        void Update()
        {
            stageMove.StateUpdate(this);
        }
    }
}