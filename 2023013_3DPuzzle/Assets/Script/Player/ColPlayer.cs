using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Stage;
using Scene;
using DG.Tweening;

namespace Player
{
    public class ColPlayer : MonoBehaviour
    {
        // インスタンス化
        private StageMove stageMove = new StageMove();
        private NeedleMove needleMove = new NeedleMove();
        
        void OnCollisionStay(Collision col)
        {   
            // ニードルが表示されているニードルタイルを踏んだ時初期座標に移動
            if(col.gameObject.tag == "Needle" && col.transform.GetChild(0).gameObject.activeSelf)
            {
                // 挙動終わりに判定
                if(!InGameSceneController.Player.OnMove)
                {
                    InGameSceneController.Player.PlayerMoveCancel = true;
                    //InGameSceneController.Player.cts.Cancel();
                    stageMove.StageFailure();   
                    needleMove.ResetTile();
                }
            }
        }
    }
}
