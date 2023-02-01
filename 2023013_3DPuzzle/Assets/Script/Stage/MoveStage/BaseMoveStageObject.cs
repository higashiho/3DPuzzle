using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stage;

namespace Tile
{
    /// <summary>
    /// 挙動ステージの動かせるオブジェクトベースクラス
    /// </summary>
    public class BaseMoveStageObject : MonoBehaviour
    {
        // 初期Angle
        public Vector3 StartAngle{get;protected set;} = new Vector3(0,0,0);
        
        /// <summary>
        /// 挙動ステージ管理ステート
        /// </summary>
        protected uint moveStageState;
        /// <summary>
        /// 挙動ステージ管理ステートプロパティ
        /// </summary>
        public uint MoveStageState{get{return moveStageState;}set{moveStageState = value;}}

        /// <summary>
        /// ステート初期化
        /// </summary>
        public void ResetState()
        {
            if(this.transform.localEulerAngles.x == 0)
                moveStageState = StageConst.STATE_STAND_UP;
            else
                moveStageState = StageConst.STATE_FALL;
        }
        // マウスが乗っているときの処理
        private void OnMouseOver()
        { 
            // ムーブステージオブジェクトに自分を格納
            InGameSceneController.MoveStage.MoveStageObj = this.gameObject;
            
        }
    }
}

