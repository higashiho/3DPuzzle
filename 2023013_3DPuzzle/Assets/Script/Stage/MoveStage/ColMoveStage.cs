using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Box
{
    public class ColMoveStage : MonoBehaviour
    {
        [SerializeField]
        private BaseMoveStage stairs;
        // 当たり判定
        private void OnCollisionEnter(Collision col)
        {
            Debug.Log(col);
            if(col.gameObject.tag == "Box")
            {
                // 回転挙動を止めて元の座標に戻す
                DOTween.Kill(stairs);
                stairs.NowTween = stairs.transform.parent.transform.DORotate(stairs.LastAngle, Const.ROTATE_TIME).
                SetEase(Ease.InQuad).OnComplete(() =>
                {
                    stairs.NowTween = null;
                });
            }
        }
    }
}