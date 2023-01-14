using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Box
{
    public class BoxMove
    {

        // y座標固定
        public void FixationPosY(BaseBox tmpBox)
        {
            // y座標を一定値で固定する
            var tmpPos = tmpBox.transform.position;
            tmpPos.y = (float)tmpBox.PosY;
            tmpBox.transform.position = tmpPos;
        }
        // 挙動
        public void Move(BaseBox tmpBox)
        {
            if(chack(tmpBox))  
            {
                tmpBox.GetComponent<Renderer>().material.color = Color.green;
                if(Input.GetMouseButtonDown(1))
                    behavior(tmpBox);
            }
            else   
                tmpBox.GetComponent<Renderer>().material.color = tmpBox.StartColor;
                

        }

        // プレイヤーの隣にいるか確認
        private bool chack(BaseBox tmpBox)
        {
            // 自分の座標とプレイヤーの座標を比較
            var tmpPos = tmpBox.transform.position - InGameSceneController.Player.transform.position;

            // 誤差で計算が出来ないためイントにキャスト
            int tmpPosX = (int)Mathf.Round(tmpPos.x);
            int tmpPosZ = (int)Mathf.Round(tmpPos.z);
            // プレイヤーの周りにいる確認
            if(tmpPosX == Const.CHECK_POS_X || tmpPosX == -Const.CHECK_POS_X || tmpPosZ == Const.CHECK_POS_Z || tmpPosZ == -Const.CHECK_POS_Z)
            {

                // プレイヤーの隣にいるか確認
                if(tmpPosX == 0 || tmpPosZ == 0)   
                    return true;
            }
            return false;
        }

        // Boxの挙動
        private void behavior(BaseBox tmpBox)
        {
            // プレイヤーを自身の方向に向けて移動させる
            InGameSceneController.Player.transform.LookAt(tmpBox.transform);
            InGameSceneController.Player.transform.DOMove(
                tmpBox.transform.position, 
                InGameSceneController.Player.PlayersData.PlayerMoveTime
            ).SetEase(Ease.OutSine).OnComplete(() => compMove(tmpBox));
        }

        // プレイヤーの移動動作が終わった時の処理
        private void compMove(BaseBox tmpBox)
        {
            // プレイヤーのconstrainsをすべて外す
            InGameSceneController.Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            // Boxの座標移動
            var tmpNewBoxPos = tmpBox.Tille.transform.position;
            tmpNewBoxPos.y = Const.BOX_POS_Y;
            tmpBox.transform.DOMove(
                tmpNewBoxPos,
                Const.BOX_MOVE_SPEED
                ).SetEase(Ease.Linear).OnComplete(() =>
                {
                    // TODO : プレイヤーを初期位置に戻す挙動作成
                    Debug.Log("MoveComp");
                });
        }
    }
}