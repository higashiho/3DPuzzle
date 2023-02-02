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

                    if(tmpMoveStage.ChangeSwitchFlag)
                        changeSwitch();
                }
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
                // ステート初期化
                tmpMoveTile.ResetState();
            }

            // フラグ初期化
            tmpMoveStage.ChangeSwitchFlag = true;
            tmpMoveStage.ResetFlag = false;
            tmpMoveStage.NowTween = null;

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
        /// <param name="tmpStairs">階段の実体</param>
        private void fall()
        {
            // 左クリックで左回転
            if(Input.GetMouseButtonDown(0) && tmpMoveStage.MoveStageObj != null)
            {
                var tmpAngle = Const.ONE_ROUND / 4;
                switch(tmpMoveStage.MoveStageObj.GetComponent<BaseMoveStageObject>().MoveStageState)
                {
                    // 立っている場合倒す
                    case StageConst.STATE_STAND_UP:
                        // 回転挙動
                        rotateStage(StageConst.STATE_FALL, tmpAngle);
                        break;
                    // 倒れている場合立てる
                    case StageConst.STATE_FALL:
                        // 回転挙動
                        rotateStage(StageConst.STATE_STAND_UP, -tmpAngle);
                        break;
                    default:
                        break;
                }
                 
            }
        }

        /// <summary>
        /// ステージが倒れる動作関数
        /// </summary>
        private void rotateStage(uint tmpState, float tmpSetAngl)
        {
            // 現在の座標取得
            tmpMoveStage.LastAngle = tmpMoveStage.MoveStageObj.transform.transform.localEulerAngles;
            // 何度回すか
            var tmpNewAngl = tmpMoveStage.LastAngle;
            tmpNewAngl.x += tmpSetAngl;

            // 転倒挙動
            tmpMoveStage.NowTween = tmpMoveStage.MoveStageObj.transform.transform.DORotate(tmpNewAngl, StageConst.ROTATE_TIME).
            SetEase(Ease.InQuad).OnStart(() => tmpMoveStage.MoveFlag = false).OnComplete(() =>
            {
                compReset();
            });
            // ステート初期化
            tmpMoveStage.MoveStageObj.GetComponent<BaseMoveStageObject>().MoveStageState &= ~tmpMoveStage.MoveStageObj.GetComponent<BaseMoveStageObject>().MoveStageState;
            tmpMoveStage.MoveStageObj.GetComponent<BaseMoveStageObject>().MoveStageState |= tmpState;
        }


        /// <summary>
        /// 初期化関数
        /// </summary>
        private void compReset()
        {
            tmpMoveStage.ChangeSwitchFlag = true;
            tmpMoveStage.NowTween = null;
        }
    }
}