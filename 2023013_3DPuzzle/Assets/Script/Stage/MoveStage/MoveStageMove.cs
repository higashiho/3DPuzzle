using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Box;

namespace Stage
{
    /// <summary>
    /// 動かせるステージの挙動関数管理クラス
    /// </summary>
    public class MoveStageMove
    {
        /// <summary>
        /// 接地判定取得関数使用用インスタンス化
        /// </summary>
        /// <returns></returns>
        private BoxMove boxMove = new BoxMove();

        /// <summary>
        /// 挙動関数
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
            
            // 左クリックのみの挙動にするため一旦コメント
                // // 右クリックで右回転
                // if(Input.GetMouseButtonDown(1))
                // {
                //     // 何度回すか
                //     var tmpSetAngl = Const.ONE_ROUND / 4;
                //     var tmpNewAngl = tmpStairs.LastAngle;
                //     tmpNewAngl.x -= tmpSetAngl;
                //     tmpStairs.NowTween = tmpStairs.transform.transform.parent.transform.DORotate(tmpNewAngl, Const.ROTATE_TIME).
                //     SetEase(Ease.InQuad).OnComplete(() =>
                //     {
                //         compReset(tmpStairs);
                //     });
                // }


            // 左クリックで左回転
            else if(Input.GetMouseButtonDown(0))
            {
                switch(tmpStairs.MoveStageState)
                {
                    // 立っている場合倒す
                    case Const.STATE_STAND_UP:
                        // 何度回すか
                        var tmpSetAngl = Const.ONE_ROUND / 4;
                        var tmpNewAngl = tmpStairs.LastAngle;
                        tmpNewAngl.x += tmpSetAngl;
                        tmpStairs.NowTween = tmpStairs.transform.transform.parent.transform.DORotate(tmpNewAngl, Const.ROTATE_TIME).
                        SetEase(Ease.InQuad).OnComplete(() =>
                        {
                            compReset(tmpStairs);
                        });
                        tmpStairs.MoveStageState |= Const.STATE_FALL;
                        break;
                    // 倒れている場合立てる
                    case Const.STATE_FALL:
                        // 何度回すか
                        tmpSetAngl = Const.ONE_ROUND / 4;
                        tmpNewAngl = tmpStairs.LastAngle;
                        tmpNewAngl.x -= tmpSetAngl;
                        tmpStairs.NowTween = tmpStairs.transform.transform.parent.transform.DORotate(tmpNewAngl, Const.ROTATE_TIME).
                        SetEase(Ease.InQuad).OnComplete(() =>
                        {
                            compReset(tmpStairs);
                        });
                        tmpStairs.MoveStageState |= Const.STATE_STAND_UP;
                        break;
                    default:
                        break;
                }
                 
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