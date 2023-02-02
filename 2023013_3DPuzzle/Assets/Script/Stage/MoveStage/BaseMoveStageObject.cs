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

        // マウスが乗っているときの処理
        private void OnMouseOver()
        { 
            // ムーブステージオブジェクトに自分を格納
            if(InGameSceneController.MoveStage.MoveFlag)
                InGameSceneController.MoveStage.MoveStageObj = this.gameObject;
        }

        private void OnMouseExit() 
        {
            InGameSceneController.MoveStage.MoveStageObj = null;
            
        }
    }
}

