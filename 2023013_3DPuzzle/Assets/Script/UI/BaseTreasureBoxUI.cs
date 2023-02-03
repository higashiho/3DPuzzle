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

        [SerializeField, Header("Errorテキスト")]
        protected TextMeshProUGUI   errorText;
        public TextMeshProUGUI  ErrorText{get{return errorText;}}
        // ErrorテキストのTween
        protected Tween errorTextTween = null;
        public Tween ErrorTextTween{get{return errorTextTween;}set{errorTextTween = value;}}

        [SerializeField, Header("入力されている値UI")]
        protected Text[] inputNumText = new Text[4];
        public Text[] InputNumText{get{return inputNumText;}}
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
            treasureBoxUIMove.CheckNum();
        }
        // ====================================================================
        // インスタンス化
        protected TreasureBoxUIMove treasureBoxUIMove;
    }
}

