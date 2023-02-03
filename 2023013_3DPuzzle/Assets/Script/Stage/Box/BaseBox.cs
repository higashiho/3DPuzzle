using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Box
{
    /// <summary>
    /// ボックスのベースクラス
    /// </summary>
    public class BaseBox : MonoBehaviour
    {       
        // UI
        public Image OpenBoxUI{get; protected set;}

        // TipButton
        public Button TipButton{get; protected set;}

        // UIが開いているか
        protected bool openFlag = false;
        public bool OpenFlag{get{return openFlag;}set{openFlag = value;}}

        // マウスが乗っているかフラグ
        public bool OverMouse{get; private set;} = false;


        //　マウスカーソルがBoxに乗った時の処理
        private void OnMouseOver()
        {
            OverMouse = true;
        }

        //　マウスカーソルがBoxから離れた時の処理
        private void OnMouseExit()
        {
            OverMouse = false;
        }
        // インスタンス化
        protected BoxMove boxMove;
    }
}