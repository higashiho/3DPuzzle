using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene;
using DG.Tweening;

namespace Player
{
    public class ColPlayer : MonoBehaviour
    {
        
        void OnCollisionStay(Collision col)
        {   
            // ニードルが表示されているニードルタイルを踏んだ時初期座標に移動
            if(col.gameObject.tag == "Needle" && col.transform.GetChild(0).gameObject.activeSelf)
            {
                // ４秒後にスタート地点に戻る
                this.transform.DORotate(Vector3.zero, Const.START_BACK_TIME).SetEase(Ease.Linear);
                this.transform.DOMove(InGameSceneController.Player.StartPos, Const.START_BACK_TIME).SetEase(Ease.Linear);
            }
        }
    }
}
