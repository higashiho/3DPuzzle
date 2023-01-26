using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stage;

namespace Tile
{
    /// <summary>
    /// スイッチタイルの当たり判定管理クラス
    /// </summary>
    public class ColSwitchTile : MonoBehaviour
    {
        // インスタンス化
        private NeedleMove needleMove = new NeedleMove();
        private StageMove stageMove = new StageMove();
        private FallTileMove fallTileMove = new FallTileMove();
        [SerializeField]
        private BaseTile tile;

        /// <summary>
        /// ステージClear挙動
        /// </summary>
        private void stageClearMove()
        {
            switch(InGameSceneController.Stages.StageState)
            {
                // 左上ステージ
                case Const.STATE_NEEDLE_STAGE:
                    stageMove.StageClear();
                    needleMove.ResetTile();
                    break;
                // 左下ステージ
                case Const.STATE_MOVE_STAGE:
                    stageMove.StageClear();
                    break;
                // 右上ステージ
                case Const.STATE_FALLING_STAGE:
                    stageMove.StageClear();
                    fallTileMove.FallTileReset(InGameSceneController.Stages.transform.GetChild(2).GetComponent<BaseFallTile>());
                    break;
                // 右下ステージ
                case Const.STATE_SWITCH_STAGE:
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
            if(col.gameObject.tag == "Player" && this.gameObject.tag == "SwitchTile")
            {
                Debug.Log(InGameSceneController.Player.PlayerClearTween);

                Debug.Log("Stay");
                if(!InGameSceneController.Player.OnMove && InGameSceneController.Player.PlayerClearTween == null)
                    stageClearMove();
            }
        }
    }
}