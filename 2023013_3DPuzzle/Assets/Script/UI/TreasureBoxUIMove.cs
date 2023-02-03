using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace UI
{
    public class TreasureBoxUIMove
    {
        // インスタンス化
        private BaseTreasureBoxUI tmpTreasureBoxUI;
        public TreasureBoxUIMove(BaseTreasureBoxUI tmp)
        {
            tmpTreasureBoxUI = tmp;
        }

        /// <summary>
        /// テキストの値変更関数
        /// </summary>
        /// <param name="num">変更する要素数</param>
        public void ChangeNumText(int num)
        {
            // クリアフラグがたっていないときのみ処理
            if(!tmpTreasureBoxUI.ClearFlag)
            {
                // 変数の値追加
                tmpTreasureBoxUI.InputNum[num]++;
                // 最大値になったら０に戻して０を出さないようにするために１増やす
                if(tmpTreasureBoxUI.InputNum[num] >= Const.NUM_MAX)
                {
                    tmpTreasureBoxUI.InputNum[num] = 0;
                    tmpTreasureBoxUI.InputNum[num]++;
                }
                // テキストの値が変更されていなければ値変更
                var tmpTextNum = int.Parse(tmpTreasureBoxUI.InputNumText[num].text);
                if(tmpTextNum != tmpTreasureBoxUI.InputNum[num])
                {
                    tmpTreasureBoxUI.InputNumText[num].text = "" + tmpTreasureBoxUI.InputNum[num];
                }
            }
            
        }

        public void CheckNum()
        {
            
            // 違いを探索
            var tmpList = new List<uint>();
            for(int i = 0; i < InGameSceneController.Player.HaveNum.Count; i++)
            {
                if(tmpTreasureBoxUI.InputNum[i] != InGameSceneController.Player.HaveNum[i])
                    tmpList.Add(tmpTreasureBoxUI.InputNum[i]);
            }

            // 違いがなかったらクリア処理
            if(tmpList.Count == 0)
            {
                tmpTreasureBoxUI.ClearFlag = true;
                foreach(var tmpText in tmpTreasureBoxUI.InputNumText)
                {
                    tmpText.color = Color.red;
                }
                // UIが開いていたら閉じる
                if(InGameSceneController.TreasureBox.OpenBoxUI.gameObject.activeSelf)
                    InGameSceneController.TreasureBox.OpenBoxUI.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("NoClear" + tmpList.Count);
                // Tweenが再生していたら削除して再生し直し
                if(tmpTreasureBoxUI.ErrorTextTween != null)
                {
                    tmpTreasureBoxUI.ErrorTextTween.Kill();
                    var tmpColor = tmpTreasureBoxUI.ErrorText.color;
                    tmpColor.a = 0;
                    tmpTreasureBoxUI.ErrorText.color = tmpColor;
                }
                // 通常フェイドでは遅いため早める
                var tmpTimer = Const.FADE_TIMER * 0.5f;
                tmpTreasureBoxUI.ErrorTextTween = tmpTreasureBoxUI.ErrorText.DOFade(Const.FADE_MAX_ALPHA, tmpTimer).
                SetEase(Ease.Linear).SetLoops(Const.LOOP_NUM, LoopType.Yoyo);
            }


        }
    }
}

