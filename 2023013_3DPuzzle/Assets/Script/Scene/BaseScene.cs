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

        [SerializeField,Header("エンドシーンのボタン")]
        protected Button endButton;
        public Button EndButton{get{return endButton;} set{endButton = value;}}
        [SerializeField, Header("エンドシーンのボタンのテクスチャ")]
        protected Image endButtonImage;
        public Image EndButtonImage{get{return endButtonImage;} set{endButtonImage = value;}}
        
        // ボタンをクリックしたかのフラグ
        [SerializeField]
        protected bool sceneMoveOnFlag;
        public bool SceneMoveOnFlag{get{return sceneMoveOnFlag;}set{sceneMoveOnFlag = value;}}

        protected Tween sceneTween;
        public Tween SceneTween{get{return sceneTween;}set{sceneTween = value;}}

        //インスタンス化
        protected  TitleSceneMove titleSceneMove = new TitleSceneMove();
        public  EndSceneMove endSceneMove{get; private set;} = new EndSceneMove();
        protected  MainSceneMove mainSceneMove = new MainSceneMove();
        public FadeMove MoveFade{get; private set;} = new FadeMove();

        [SerializeField]
        protected BaseLoadingImage loadingImage;
        
        //自分を入れる用
        protected static BaseScene  tmpScene;
        public static BaseScene TmpScene{get;private set;}

        // protected void OnDestroy()
        // {
        //     DOTween.KillAll();
        // }
    }
}