using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Box
{
    public class MoveStageMove
    {
        // インスタンス化
        private BoxMove boxMove = new BoxMove();

        /// <summary>
        /// 挙動
        /// </summary>
        /// <param name="tmpBox"></param>
        public void Move(BaseMoveStage tmpStairs)
        {
            if(boxMove.Chack(tmpStairs.gameObject))  
            {
                // Tweenに何も入っていないとき
                if(tmpStairs.NowTween == null)
                {
                    tmpStairs.GetComponent<Renderer>().material.color = Color.green;

                    fall(tmpStairs);
                }
            }
            else   
                tmpStairs.GetComponent<Renderer>().material.color = tmpStairs.StartColor;
                

        }

        /// <summary>
        /// 回転挙動関数
        /// </summary>
        /// <param name="tmpStairs">階段の実体</param>
        private void fall(BaseMoveStage tmpStairs)
        {
            // 挙動前の向きが自身の座標と違う時自身の今の向きを取得
            if(tmpStairs.LastAngle != tmpStairs.transform.parent.transform.localEulerAngles)
                tmpStairs.LastAngle = tmpStairs.transform.parent.transform.localEulerAngles;
            
            // 右クリックで右回転
            if(Input.GetMouseButtonDown(1))
            {
                // 何度回すか
                var tmpSetAngl = Const.ONE_ROUND / 4;
                var tmpNewAngl = tmpStairs.LastAngle;
                tmpNewAngl.x -= tmpSetAngl;
                tmpStairs.NowTween = tmpStairs.transform.transform.parent.transform.DORotate(tmpNewAngl, Const.ROTATE_TIME).
                SetEase(Ease.InQuad).OnComplete(() =>
                {
                    compReset(tmpStairs);
                });
            }
            // 左クリックで左回転
            else if(Input.GetMouseButtonDown(0))
            {
                 // 何度回すか
                var tmpSetAngl = Const.ONE_ROUND / 4;
                var tmpNewAngl = tmpStairs.LastAngle;
                tmpNewAngl.x += tmpSetAngl;
                tmpStairs.NowTween = tmpStairs.transform.transform.parent.transform.DORotate(tmpNewAngl, Const.ROTATE_TIME).
                SetEase(Ease.InQuad).OnComplete(() =>
                {
                    compReset(tmpStairs);
                });
            }
        }

        /// <summary>
        /// 初期化関数
        /// </summary>
        /// <param name="tmpStairs">階段の実体</param>
        private void compReset(BaseMoveStage tmpStairs)
        {
            tmpStairs.NowTween = null;
        }
    }
}