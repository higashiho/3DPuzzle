using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Tile;

namespace Stage
{
    public class StageMove
    {
        /// <summary>
        /// ステージのステート更新関数
        /// </summary>
        /// <param name="tmpStage">ステージの実体</param>
        public void StateUpdate(BaseStage tmpStage)
        {
            // playerのposが一定以内の場合
            if(InGameSceneController.Player.transform.position.x <= Const.Area1Pos.x &&
                InGameSceneController.Player.transform.position.z >= Const.Area1Pos.z)
                {
                    // ステート更新されていないと更新してvoid処理終了
                    if(tmpStage.StageState != Const.STATE_NEEDLE_STAGE)
                        bitUpdate(tmpStage,Const.STATE_NEEDLE_STAGE);

                    if(InGameSceneController.Player.OnMove)
                        Debug.Log("Area1");
                    return;
                }

            // playerのposが一定以内の場合
            if(InGameSceneController.Player.transform.position.x <= Const.Area2Pos.x &&
                InGameSceneController.Player.transform.position.z <= Const.Area2Pos.z)
                {
                    // ステート更新されていないと更新してvoid処理終了
                    if(tmpStage.StageState != Const.STATE_MOVE_STAGE)
                        bitUpdate(tmpStage,Const.STATE_MOVE_STAGE);

                    if(InGameSceneController.Player.OnMove)
                        Debug.Log("Area2");
                    return;
                }

            // playerのposが一定以内の場合
            if(InGameSceneController.Player.transform.position.x >= Const.Area3Pos.x &&
                InGameSceneController.Player.transform.position.z >= Const.Area3Pos.z)
                {
                    // ステート更新されていないと更新してvoid処理終了
                    if(tmpStage.StageState != Const.STATE_FALLING_STAGE)
                        bitUpdate(tmpStage,Const.STATE_FALLING_STAGE);

                    if(InGameSceneController.Player.OnMove)
                        Debug.Log("Area3");
                    return;
                }

            // playerのposが一定以内の場合
            if(InGameSceneController.Player.transform.position.x >= Const.Area4Pos.x &&
                InGameSceneController.Player.transform.position.z <= Const.Area4Pos.z)
                {
                    // ステート更新されていないと更新してvoid処理終了
                    if(tmpStage.StageState != Const.STATE_SWITCH_STAGE)
                        bitUpdate(tmpStage,Const.STATE_SWITCH_STAGE);

                    if(InGameSceneController.Player.OnMove)
                        Debug.Log("Area4");
                    return;
                }
            
            // ステート更新されていなくどこにも入らなければ初期化
            if(tmpStage.StageState != Const.STATE_START)
                bitUpdate(tmpStage, Const.STATE_START);

            if(InGameSceneController.Player.OnMove)
                Debug.Log("None");
            
        }

        /// <summary>
        /// ビットステート更新関数
        /// </summary>
        /// <param name="tmpStage">ステージの実体</param>
        /// <param name="stateNum">変換するステート</param>
        private void bitUpdate(BaseStage tmpStage, uint stateNum)
        {
            tmpStage.StageState &= ~tmpStage.StageState;
            tmpStage.StageState |= stateNum;
        }

        /// <summary>
        /// ステージClear挙動
        /// </summary>
        public void StageClear()
        {
            // 当たり判定が消えていなかったら消す
            if(InGameSceneController.Player.GetComponent<BoxCollider>().enabled)
                InGameSceneController.Player.GetComponent<BoxCollider>().enabled = false;
            
            clearPlayerRotate();
            clearPlayerMove();
        }
        
        /// <summary>
        /// プレイヤークリア時移動挙動
        /// </summary>
        private void clearPlayerMove()
        {
            // 初期座標に帰るまでに通るルート格納配列
            Vector3[] tmpPath = new Vector3[3]
            {
                InGameSceneController.Player.transform.position,
                InGameSceneController.Player.StartPos,
                InGameSceneController.Player.StartPos
            };
            // 最大要素数 -1
            var tmpNum = tmpPath.Length - 1;
            for(int i = 0; i < tmpPath.Length; i++)
            {
                // 最後の要素は触らない
                if(i == tmpNum)
                    break;
                // y座標に１００足す
                tmpPath[i].y += Const.CLEAR_MAX_POS_Y;
            }
            // ２秒後に５秒かけて移動
            InGameSceneController.Player.transform.DOLocalPath(tmpPath, Const.CLEAR_MOVE_TIME).SetDelay(Const.CLEAR_STOP_TIME).
            SetEase(Ease.OutQuad).OnComplete(compReset);
        }

        /// <summary>
        /// 移動完了時の初期化関数
        /// </summary>
        private void compReset()
        {
            InGameSceneController.Player.transform.localEulerAngles = Vector3.zero;
            InGameSceneController.Player.PlayerClearTween.Kill();
            
            InGameSceneController.Player.PlayerClearTween = null;

            if(!InGameSceneController.Player.GetComponent<BoxCollider>().enabled)
                InGameSceneController.Player.GetComponent<BoxCollider>().enabled = true;


        }
        /// <summary>
        /// 0.5秒に一回転する挙動関数
        /// </summary>
        private void clearPlayerRotate()
        {
            // プレイヤーの向き初期化
            InGameSceneController.Player.transform.localEulerAngles = Vector3.zero;

            // 0.5秒で一周回ってそれを続ける
            var tmpAngle = Const.ONE_ROUND / 2;
            InGameSceneController.Player.PlayerClearTween = 
            InGameSceneController.Player.transform.DORotate(Vector3.up * tmpAngle, Const.CLEAR_ROTATE_TIME).
            SetEase(Ease.Linear).OnComplete(clearPlayerRotate);
        }

        /// <summary>
        /// ステージ失敗挙動
        /// </summary>
        public void StageFailure()
        {
            // ４秒後にスタート地点に戻る
            InGameSceneController.Player.transform.DORotate(Vector3.zero, Const.START_BACK_TIME).SetEase(Ease.Linear);
            InGameSceneController.Player.PlayerFailureTween =  InGameSceneController.Player.transform.DOMove(InGameSceneController.Player.StartPos, Const.START_BACK_TIME).
            SetEase(Ease.Linear).OnComplete(() => InGameSceneController.Player.PlayerFailureTween = null);

        }
    }
}