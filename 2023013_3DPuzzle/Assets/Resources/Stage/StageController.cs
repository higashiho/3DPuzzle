using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            // アセット参照でのロード
            Handle = csvDataAssetRef.LoadAssetAsync<TextAsset>();
            
            // 読み込み終了時のイベントハンドラー処理代入
            Handle.Completed += instance.StageMaking;
            // ロード終了まで待機
            await Handle.Task;
            // 読み込み終了時のイベントハンドラーに読み込み成功後の処理を実装。
            // ステージ生成
            Instantiate(puzzleStageWall, parent:TileParemt.transform);

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