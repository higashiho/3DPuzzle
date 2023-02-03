using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Box
{
    /// <summary>
    /// ボックスの挙動関数管理クラス
    /// </summary>
    public class BoxMove
    {
       
        // インスタンス化
        private BaseBox tmpBox;
        public BoxMove(BaseBox tmp)
        {
            tmpBox = tmp;
        }
        
        /// <summary>
        /// 挙動関数
        /// </summary>
        public void Move()
        {
            // 接地しているか
            if(Chack())  
            {
                // UIが開いているときに左クリックでUI表示
                if(Input.GetMouseButtonDown(0) && tmpBox.OverMouse)
                {
                    startBoxUI();
                }

                // 右クリックでUI削除
                if(Input.GetMouseButtonDown(1))
                    resetBoxUI();
            }
            else
                resetBoxUI();

        }

        /// <summary>
        /// プレイヤーとの接地判定関数
        /// </summary>
        /// <returns>接地しているかどうか</returns> 
        public bool Chack()
        {
            // 宝箱付きタイルとプレイヤーの座標を比較
            Vector3 tmpPos = tmpBox.transform.position - InGameSceneController.Player.transform.position;

            // 誤差で計算が出来ないためイントにキャスト
            tmpPos.x = Mathf.RoundToInt(tmpPos.x);
            tmpPos.z = Mathf.RoundToInt(tmpPos.z);
            // プレイヤーの周りにいる確認
            if(tmpPos.x == Const.CHECK_POS_X || tmpPos.x == -Const.CHECK_POS_X || tmpPos.z == Const.CHECK_POS_Z || tmpPos.z == -Const.CHECK_POS_Z)
            {

                // プレイヤーの隣にいるか確認
                if(tmpPos.x == 0 || tmpPos.z == 0)   
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 宝箱のUIを開いた時の処理
        /// </summary>
        private void startBoxUI()
        {
            if(!tmpBox.OpenBoxUI.gameObject.activeSelf)
            {
                tmpBox.OpenBoxUI.gameObject.SetActive(true);
                tmpBox.OpenFlag = true;
                tmpBox.TipButton.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 宝箱のUI初期化
        /// </summary>
        private void resetBoxUI()
        {
            if(tmpBox.OpenBoxUI.gameObject.activeSelf)
            {
                tmpBox.OpenBoxUI.gameObject.SetActive(false);
                tmpBox.OpenFlag = false;
                tmpBox.TipButton.gameObject.SetActive(true);
            }

        }
    }
}