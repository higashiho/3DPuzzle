using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LoadingImage;

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
        
        [SerializeField]
        protected BaseLoadingImage loadingImage;
        

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
        public bool SceneMoveOnFlag{get{return sceneMoveOnFlag;}set{sceneMoveOnFlag = value;}}

        //以下インスタンス化
        protected  TitleSceneMove titleSceneMove = new TitleSceneMove();
        protected  EndSceneMove endSceneMove = new EndSceneMove();
        protected  MainSceneMove mainSceneMove = new MainSceneMove();
        public FadeMove MoveFade{get; private set;} = new FadeMove();

    }
}