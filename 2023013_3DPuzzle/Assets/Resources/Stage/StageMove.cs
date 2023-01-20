using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}