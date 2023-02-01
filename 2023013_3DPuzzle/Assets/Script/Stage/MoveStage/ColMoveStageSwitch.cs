using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tile
{
    /// <summary>
    /// 挙動ステージのスイッチ当たり判定管理クラス
    /// </summary>
    public class ColMoveStageSwitch : MonoBehaviour
    {
        private void OnCollisionEnter(Collision col) 
        {
            if(col.gameObject.tag == "Player" && this.tag == "MoveTileSwitch")
            {
                InGameSceneController.MoveStage.MoveFlag = true;
            }
        }
    }  
}
