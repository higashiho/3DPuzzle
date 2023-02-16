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
        // シーン移動のTWeen
        protected Tween sceneTween = null;
        public Tween SceneTween{get{return sceneTween;} set{sceneTween = value;}}
        // エンドシーンボックスの移動先
        protected Vector3Int endBoxPos = new Vector3Int(0, 1, 0);
        public Vector3Int EndBoxPos{get{return endBoxPos;} set{endBoxPos = value;}}

        //インスタンス化
        public  TitleSceneMove titleSceneMove{get;protected set;} = new TitleSceneMove();
        protected  MainSceneMove mainSceneMove = new MainSceneMove();
        public EndSceneMove EndMove{get;private set;} = new EndSceneMove();
        public FadeMove MoveFade{get; private set;} = new FadeMove();

        // BaseはnewできないのでSerializeField
        [SerializeField]
        protected BaseLoadingImage loadingImage;
        public BaseLoadingImage LoadingImage{get{return loadingImage;}}
        
        
        //自分を入れる用
        public static BaseScene TmpScene{get; protected set;}
        
    }
}