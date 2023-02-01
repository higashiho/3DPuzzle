using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scene
{
    public class BaseScene : MonoBehaviour
    {   
        //今のシーンがどこか
        public enum SceneState
        {
            Title,
            Main,
            MainFinish,
            End
        }

        //以下定義
        [Header("今のシーンがどこか")]
        [SerializeField]
        protected SceneState sceneState;
        public SceneState StateScene{get{return sceneState;}set{sceneState = value;}}
        
        [Header("タイトル暗転パネル")]
        [SerializeField]
        public Image fadePanel;

        [SerializeField, Header("ゲーム開始ボタン")]
        protected Button startButton;
        public Button StartButton{get{return startButton;}private set{startButton = value;}}
        // [SerializeField, Header("オプション画面")]
        // protected Button optionButton;
        // public Button OptionButton{get{return optionButton;}private set{optionButton = value;}}
        [SerializeField, Header("ゲーム終了ボタン")]
        protected Button endButton;
        public Button EndButton{get{return endButton;}private set{endButton = value;}}
        // マウスがボタンの上にあるかのフラグ
        protected bool onToButtonFlag;
        public bool OnToButtonFlag{get{return onToButtonFlag;}set{onToButtonFlag = value;}}

        [SerializeField, Header("ロード画面のゲージ")]
        protected Slider loadSlider;
        public Slider LoadSlider{get{return loadSlider;}private set{loadSlider = value;}}
        [SerializeField, Header("ロート中の画面のUI")]
        protected GameObject loadUI;
        public GameObject LoadUI{get{return loadUI;}private set{loadUI = value;}}

        // 非同期処理コントロール変数
        protected AsyncOperation asyncOperation;
        public AsyncOperation Async{get{return asyncOperation;}set{asyncOperation = value;}}

        //以下インスタンス化
        protected  TitleSceneMove titleSceneMove = new TitleSceneMove();
        protected  EndSceneMove endSceneMove = new EndSceneMove();
        protected  MainSceneMove mainSceneMove = new MainSceneMove();
        public FadeMove MoveFade{get; private set;} = new FadeMove();

    }
}