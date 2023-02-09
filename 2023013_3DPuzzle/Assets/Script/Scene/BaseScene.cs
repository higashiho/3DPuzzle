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
        /// フラグをオンにする・暗転パネル出現
        /// </summary>
        public void SceneMoveFlagOn()
        {
            SceneMoveOnFlag = true;
            // パネル出現
            fadePanel.enabled = true;
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
        
        // ボタンをクリックしたかのフラグ
        [SerializeField]
        protected bool sceneMoveOnFlag;
        public bool SceneMoveOnFlag{get{return sceneMoveOnFlag;} set{sceneMoveOnFlag = value;}}

        protected Tween sceneTween = null;
        public Tween SceneTween{get{return sceneTween;}set{sceneTween = value;}}

        //インスタンス化
        protected  TitleSceneMove titleSceneMove = new TitleSceneMove();
        protected  MainSceneMove mainSceneMove = new MainSceneMove();
        public EndSceneMove EndMove{get;private set;} = new EndSceneMove();
        public FadeMove MoveFade{get; private set;} = new FadeMove();

        // BaseはnewできないのでSerializeField
        [SerializeField]
        protected BaseLoadingImage loadingImage;
        
        //自分を入れる用
        public static BaseScene TmpScene;
        
        protected void OnDestroy()
        {
            DOTween.KillAll();
        }
    }
}