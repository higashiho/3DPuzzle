using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoadingImage;
using DG.Tweening;

namespace Scene
{
    public class BaseScene : MonoBehaviour
    {   
        /// <summary>
        /// フラグをオンにする
        /// </summary>
        public void SceneMoveFlagOn()
        {
            SceneMoveOnFlag = true;
        }

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

        [SerializeField,Header("エンドシーンのボタンのボタンインスペクター")]
        protected Button retryButton;
        public Button RetryButton{get{return retryButton;} set{retryButton = value;}}
        [SerializeField, Header("エンドシーンのボタンのImage")]
        protected Image retryButtonImage;
        public Image RetryButtonImage{get{return retryButtonImage;} set{retryButtonImage = value;}}
        [SerializeField,Header("スタートボタンのImage")]
        protected Image startButton;
        public Image StartButton{get{return startButton;} set{startButton = value;}}
        [SerializeField,Header("ゲーム終了Image")]
        protected Image finishButton;
        public Image FinishButton{get{return finishButton;} set{finishButton = value;}}
        
        // ボタンをクリックしたかのフラグ
        [SerializeField]
        protected bool sceneMoveOnFlag;
        public bool SceneMoveOnFlag{get{return sceneMoveOnFlag;} set{sceneMoveOnFlag = value;}}

        protected Tween sceneTween;
        public Tween SceneTween{get{return sceneTween;}set{sceneTween = value;}}

        //インスタンス化
        protected  TitleSceneMove titleSceneMove = new TitleSceneMove();
        protected  MainSceneMove mainSceneMove = new MainSceneMove();
        protected EndSceneMove endSceneMove = new EndSceneMove();
        public FadeMove MoveFade{get; private set;} = new FadeMove();

        protected BaseLoadingImage loadingImage;
        
        //自分を入れる用
        protected static BaseScene  tmpScene;
        [SerializeField]
        public static BaseScene TmpScene{get;private set;}

        protected void OnDestroy()
        {
            DOTween.KillAll();
        }
    }
}