using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

namespace UI
{
    public class BaseTreasureBoxUI : MonoBehaviour
    {
        [SerializeField, Header("playerが持っている数字UI")]
        protected Text[] playerHaveNumText = new Text[4];
        public Text[] PlayerHaveNumText{get{return playerHaveNumText;}}

        [SerializeField, Header("宝箱テキスト")]
        protected TextMeshProUGUI treasureText;
        public TextMeshProUGUI TreasureText{get{return treasureText;}}
        [SerializeField, Header("クリア時のエフェクト")]
        protected ParticleSystem[] clearEfect = new ParticleSystem[2];
        public ParticleSystem[] ClearEfect{get{return clearEfect;}}
        // ErrorテキストのTween
        protected Tween errorTextTween = null;
        public Tween ErrorTextTween{get{return errorTextTween;}set{errorTextTween = value;}}

        [SerializeField, Header("入力されている値UI")]
        protected TextMeshProUGUI[] inputNumText = new TextMeshProUGUI[4];
        public TextMeshProUGUI[] InputNumText{get{return inputNumText;}}
        // 入力終了フラグ
        protected bool clearFlag = false;
        public bool ClearFlag{get{return clearFlag;}set{clearFlag = value;}}

        // 入力されているUIの数値リスト
        protected List<uint> inputNum = new List<uint>(4){9, 9, 9, 9};
        public List<uint> InputNum{get{return inputNum;}set{inputNum = value;}}

        // 各ボタン押された時の処理==============================================
        public void ChangeLeftNum()
        {
            treasureBoxUIMove.ChangeNumText(Const.BUTOON_LEFT);
        }
        public void ChangeCenterLeftNum()
        {
            treasureBoxUIMove.ChangeNumText(Const.BUTOON_CENTER_LEFT);
        }
        public void ChangeCenterRighitNum()
        {
            treasureBoxUIMove.ChangeNumText(Const.BUTOON_CENTER_RIGHIT);
        }
        public void ChangeRightNum()
        {
            treasureBoxUIMove.ChangeNumText(Const.BUTOON_RIGHIT);
        }
        public void GoEnter()
        {
            if(!ClearFlag)
                treasureBoxUIMove.CheckNum();
        }
        public void ResetNum()
        {
            treasureBoxUIMove.PlayerHaveNumReset();
        }
        // ====================================================================
        // インスタンス化
        protected TreasureBoxUIMove treasureBoxUIMove;
    }
}

