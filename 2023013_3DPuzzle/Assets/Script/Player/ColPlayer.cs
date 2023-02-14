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
        private NeedleMove needleMove;
        
        

        void OnCollisionStay(Collision col)
        {   
            // インスタンス化がnullの場合インスタンス化する
            if(needleMove == null)
                needleMove = new NeedleMove(InGameSceneController.Player.Needle);

            // ニードルが表示されているニードルタイルを踏んだ時初期座標に移動
            if(col.gameObject.tag == "Needle" && col.transform.GetChild(0).gameObject.activeSelf)
            {
                // 挙動終わりに判定
                if(!InGameSceneController.Player.IsRotate)
                {
                    InGameSceneController.Player.PlayerMoveCancel = true;
                    InGameSceneController.Enemy.EnemyMoveCancel = true;
                    //InGameSceneController.Player.cts.Cancel();
                    stageMove.StageFailure();   
                    needleMove.ResetTile();
                }
            }

        }
    }
}
