using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Tile;

namespace Stage
{
    /// <summary>
    /// 動かせるステージの挙動関数管理クラス
    /// </summary>
    public class MoveStageMove
    {
        private BaseMoveStage tmpMoveStage;

        public MoveStageMove(BaseMoveStage tmp)
        {
            tmpMoveStage = tmp;
        }
        /// <summary>
        /// 挙動関数
        /// </summary>
        /// <param name="tmpBox"></param>
        public void Move()
        {
            if(InGameSceneController.Stages.StageState == StageConst.STATE_MOVE_STAGE)
            {
                if(tmpMoveStage.MoveFlag)
                {
                    tmpMoveStage.ResetFlag = true;
                    // Tweenに何も入っていないとき
                    if(tmpMoveStage.NowTween == null)
                    {
                        fall();
                    }
                }
                if(tmpMoveStage.ChangeSwitchFlag)
                        changeSwitch();
            }
            else if(tmpMoveStage.ResetFlag)
                ResetMoveStage();
        }

        /// <summary>
        /// ステージリセット関数
        /// </summary>
        public void ResetMoveStage()
        {
            // 全てのムーブスイッチをタイルに変更
            foreach(var tmpSwitch in tmpMoveStage.OnMoveSwitchs)
            {
                if(tmpSwitch == null)
                    break;
                
                if(tmpSwitch.tag == "MoveTileSwitch")
                {
                    tmpSwitch.tag = "WhiteTile";
                    tmpSwitch.GetComponent<Renderer>().material.color = Color.white;
                    tmpSwitch.GetComponent<BaseTile>().StartColor = tmpSwitch.GetComponent<Renderer>().material.color;
                }
            }

            // 挙動タイル初期化
            foreach(var tmpObj in tmpMoveStage.MoveStageTiles)
            {
                // tweenのCancel
                DOTween.Kill(tmpObj.transform);
                // 移動していたら初期化
                var tmpMoveTile = tmpObj.GetComponent<BaseMoveStageObject>();
                if(tmpObj.transform.localEulerAngles != tmpMoveTile.StartAngle)
                    tmpObj.transform.localEulerAngles = tmpMoveTile.StartAngle;
            }

            // フラグ初期化
            tmpMoveStage.ChangeSwitchFlag = true;
            tmpMoveStage.ResetFlag = false;
            tmpMoveStage.MoveFlag = false;
            tmpMoveStage.NowTween = null;
            tmpMoveStage.MoveStageObj = null;

        }

        /// <summary>
        /// Switch変更挙動
        /// </summary>
        private void changeSwitch()
        {
            // 全てのムーブスイッチをタイルに変更
            foreach(var tmpSwitch in tmpMoveStage.OnMoveSwitchs)
            {
                if(tmpSwitch == null)
                    break;
                
                if(tmpSwitch.tag == "MoveTileSwitch")
                {
                    tmpSwitch.tag = "WhiteTile";
                    tmpSwitch.GetComponent<Renderer>().material.color = Color.white;
                    tmpSwitch.GetComponent<BaseTile>().StartColor = tmpSwitch.GetComponent<Renderer>().material.color;
                }
            }

            // ランダムなSwitchを1つだけSwitchにする
            var tmpNum = UnityEngine.Random.Range(0, tmpMoveStage.OnMoveSwitchs.Length);
            tmpMoveStage.OnMoveSwitchs[tmpNum].GetComponent<Renderer>().material.color = Color.magenta;
            tmpMoveStage.OnMoveSwitchs[tmpNum].GetComponent<BaseTile>().StartColor = tmpMoveStage.OnMoveSwitchs[tmpNum].GetComponent<Renderer>().material.color;
            tmpMoveStage.OnMoveSwitchs[tmpNum].tag = "MoveTileSwitch";
            tmpMoveStage.ChangeSwitchFlag = false;
        }

        /// <summary>
        /// 回転挙動関数
        /// </summary>
        private void fall()
        {
            // 左クリックで左回転
            if(Input.GetMouseButtonDown(0) && tmpMoveStage.MoveStageObj != null)
            {
                var tmpAngle = Const.ONE_ROUND / 4;
                // 左回りにして自分の上の段と下の段を逆回転させる
                rotateStage(tmpAngle, tmpMoveStage.MoveStageObj);
                var tmpNum = 0;
                // 自身のオブジェクトの要素数を取得
                for(int i = 0; i < tmpMoveStage.MoveStageTiles.Length; i++)
                {
                    if(tmpMoveStage.MoveStageTiles[i] == tmpMoveStage.MoveStageObj)
                    {
                        tmpNum = i;
                        break;
                    }
                }
                // 取った要素数の上下を取得用変数
                var tmpUpNum = tmpNum;
                var tmpDownNum = tmpNum;
                // 要素数の範囲内であれば右回り
                if(++tmpUpNum < tmpMoveStage.MoveStageTiles.Length)
                    rotateStage(-tmpAngle, tmpMoveStage.MoveStageTiles[tmpUpNum]);
                if(--tmpDownNum >= 0)
                    rotateStage(-tmpAngle, tmpMoveStage.MoveStageTiles[tmpDownNum]);

                 
            }
        }

        /// <summary>
        /// ステージが倒れる動作関数
        /// </summary>
        /// <param name="tmpSetAngl">回す角度</param>
        /// <param name="tmpObj">回すオブジェクト</param>
        private void rotateStage(float tmpSetAngl, GameObject tmpObj)
        {
            // 現在の座標取得
            tmpMoveStage.LastAngle = tmpObj.transform.parent.localEulerAngles;
            // 何度回すか
            var tmpNewAngl = tmpMoveStage.LastAngle;
            tmpNewAngl.y += tmpSetAngl;

            // 転倒挙動
            tmpMoveStage.NowTween = null;
            tmpMoveStage.NowTween = tmpObj.transform.parent.DORotate(tmpNewAngl, StageConst.ROTATE_TIME).
            SetEase(Ease.InQuad).OnStart(() => 
            {
                tmpMoveStage.MoveFlag = false;
            }).OnComplete(() =>
            {
                compReset();
            });
        }


        /// <summary>
        /// 初期化関数
        /// </summary>
        private void compReset()
        {
            tmpMoveStage.ChangeSwitchFlag = true;
            tmpMoveStage.NowTween = null;
            tmpMoveStage.MoveStageObj = null;
        }
    }
}