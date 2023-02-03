using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Tile;

namespace Stage
{
    /// <summary>
    /// 動かせるステージの当たり判定管理クラス
    /// </summary>
    public class ColMoveStage : MonoBehaviour
    {
        [SerializeField]
        private BaseMoveStageObject moveTile;
        // 当たり判定
        private void OnCollisionEnter(Collision col)
        {
            Debug.Log(col);
            if(col.gameObject.tag == "MoveTile")
            {
                // 回転挙動を止めて元の座標に戻す
                DOTween.Kill(this.transform);
                InGameSceneController.MoveStage.NowTween = this.transform.transform.DORotate
                (InGameSceneController.MoveStage.LastAngle, StageConst.ROTATE_TIME).
                SetEase(Ease.InQuad).OnComplete(() =>
                {
                    InGameSceneController.MoveStage.ChangeSwitchFlag = true;
                    InGameSceneController.MoveStage.MoveStageObj = null;
                    InGameSceneController.MoveStage.NowTween = null;
                });
            }
        }
    }
}