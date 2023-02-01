using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stage;
using DG.Tweening;

namespace Tile
{
    /// <summary>
    /// スイッチタイルの当たり判定管理クラス
    /// </summary>
    public class ColTile : MonoBehaviour
    {
        // インスタンス化
        private NeedleMove needleMove = new NeedleMove();
        private StageMove stageMove = new StageMove();
        private TileMove tileMove = null;
        private FallTileMove fallTileMove = null;

        [SerializeField]
        private BaseTile tile;

        /// <summary>
        /// ステージClear挙動
        /// </summary>
        private void stageClearMove()
        {
            // タイルムーブのインスタンス化がされていない場合はインスタンス化をする
            if(tileMove == null)
                tileMove = new TileMove(tile);
            
            if(fallTileMove == null)
                fallTileMove = new FallTileMove(InGameSceneController.FallTile);
            
            switch(InGameSceneController.Stages.StageState)
            {
                // 左上ステージ
                case StageConst.STATE_NEEDLE_STAGE:
                    stageMove.StageClear();
                    needleMove.ResetTile();
                    break;
                // 左下ステージ
                case StageConst.STATE_MOVE_STAGE:
                    stageMove.StageClear();
                    break;
                // 右上ステージ
                case StageConst.STATE_FALLING_STAGE:
                    // カウントが1以下になったらクリア処理
                    if(InGameSceneController.Stages.ClearCount <= StageConst.GOAL_TILE_NUM)
                    {
                        BaseFallTile.Cts.Cancel();
                        DOTween.Kill(InGameSceneController.FallTile.WarningPanel);
                        stageMove.StageClear();
                        fallTileMove.FallTileReset();
                    }
                    // プレイヤーから一番遠いタイルをスイッチタイルに変更
                    else if(InGameSceneController.Stages.TileChangeFlag)
                    {
                        Debug.Log("In");
                        tileMove.ChangeSwitchTile();
                    }
                    break;
                // 右下ステージ
                case StageConst.STATE_SWITCH_STAGE:
                    stageMove.StageClear();
                    break;
                default:
                    break;
            }
        }

        // 当たり判定
        private void OnCollisionStay(Collision col)
        {
            // Playerと当たった時に自分がスイッチの場合
            if(col.gameObject.tag == "Player" && this.gameObject.tag == "KeyTile")
            {
                Debug.Log("Stay");
                if(!InGameSceneController.Player.OnMove && InGameSceneController.Player.PlayerClearTween == null)
                    stageClearMove();
            }
        }
    }
}