using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scene;
using UnityEngine.UI;

namespace UI
{
    public class BaseButton : MonoBehaviour
    {

        [SerializeField, Header("ゲーム開始ボタン")]
        protected Button startButton;
        public Button StartButton{get{return startButton;}private set{startButton = value;}}
        // [SerializeField, Header("オプション画面")]
        // protected Button optionButton;
        // public Button OptionButton{get{return optionButton;}private set{optionButton = value;}}
        [SerializeField, Header("ゲーム終了ボタン")]
        protected Button endButton;
        public Button EndButton{get{return endButton;}private set{endButton = value;}}

        [SerializeField, Header("ロード画面のゲージ")]
        protected Slider loadSlider;
        public Slider LoadSlider{get{return loadSlider;}private set{loadSlider = value;}}
        // [SerializeField, Header("ロート中の画面のUI")]
        // protected GameObject loadUI;
        // public GameObject LoadUI{get{return loadUI;}private set{loadUI = value;}}

        //public BaseButton TmpButton{get; set;} = new BaseButton();
    }
}