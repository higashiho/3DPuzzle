using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

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
                if(Input.GetMouseButtonDown(0) && tmpBox.OverMouse)
                {
                    // クリアフラグがたっていないときはUI表示
                    if(!InGameSceneController.TreasureBoxUI.ClearFlag)
                        startBoxUI();
                    
                    // クリアフラグがたっていたらClear挙動
                    else
                        clearMove();
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
            // 表示されていなければ表示を初期化
            if(!tmpBox.OpenBoxUI.gameObject.activeSelf)
            {
                tmpBox.OpenBoxUI.gameObject.SetActive(true);
                tmpBox.OpenFlag = true;
                tmpBox.TipButton.gameObject.SetActive(false);

                // 持っている数字をランダムに表示
                bool[] tmpHaveNums = new bool[4];
                var tmpTreasureBox = tmpBox.OpenBoxUI.transform.parent.GetComponent<BaseTreasureBoxUI>();
                int tmpCount = 0;
                while(tmpCount < tmpTreasureBox.PlayerHaveNumText.Length)
                {
                    var tmpNum = UnityEngine.Random.Range(0, tmpTreasureBox.PlayerHaveNumText.Length);

                    // １回格納されていなければ格納
                    if(!tmpHaveNums[tmpNum])
                    {
                        tmpHaveNums[tmpNum] = true;
                        // プレイヤーの所持数値が０出なければ代入
                        if(InGameSceneController.Player.HaveNum[tmpNum] != 0)
                        {
                            tmpTreasureBox.PlayerHaveNumText[tmpCount].text = "" + InGameSceneController.Player.HaveNum[tmpNum];
                            // 表示を消されていたら再表示
                            if(!tmpTreasureBox.PlayerHaveNumText[tmpCount].transform.parent.gameObject.activeSelf)
                                tmpTreasureBox.PlayerHaveNumText[tmpCount].transform.parent.gameObject.SetActive(true);
                        }
                        // ０であれば表示削除
                        else if(tmpTreasureBox.PlayerHaveNumText[tmpCount].transform.parent.gameObject.activeSelf)
                            tmpTreasureBox.PlayerHaveNumText[tmpCount].transform.parent.gameObject.SetActive(false);

                        // カウント増加
                        tmpCount++;
                    }
                }
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

        /// <summary>
        /// Clear挙動
        /// </summary>
        private void clearMove()
        {
            // エフェクトを両方再生
            InGameSceneController.TreasureBoxUI.ClearEfect[0].Play();
            InGameSceneController.TreasureBoxUI.ClearEfect[1].Play();
        }
    }
}