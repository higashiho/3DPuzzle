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
        private NeedleMove needleMove = new NeedleMove();
        
        void OnCollisionStay(Collision col)
        {   //ゴールパネルを踏んだらMainFinishに変更
            if(col.gameObject.tag == "GoalTile")
            {
                if(!InGameSceneController.Player.OnMove)
                {
                    SceneController.tmpScene.StateScene = BaseScene.SceneState.MainFinish;
                }
            }

            // ニードルが表示されているニードルタイルを踏んだ時初期座標に移動
            if(col.gameObject.tag == "Needle" && col.transform.GetChild(0).gameObject.activeSelf)
            {
                needleMove.PlayerReset();   
            }
        }
    }
}
