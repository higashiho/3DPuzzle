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

        //以下インスタンス化
        protected  TitleSceneMove titleSceneMove = new TitleSceneMove();
        protected  EndSceneMove endSceneMove = new EndSceneMove();
        protected  MainSceneMove mainSceneMove = new MainSceneMove();
        public FadeMove MoveFade{get; private set;} = new FadeMove();

    }
}