using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scene;
using UnityEngine.UI;
using DG.Tweening;

namespace button
{
    public class BaseButton : MonoBehaviour
    {
        // タイトルのスタートボタン
        protected Button startButton;
        public Button StartButton{get{return startButton;} set{startButton = value;}}
        // タイトルの続きからボタン
        protected Button restartButton;
        public Button RestartButton{get{return restartButton;} set{restartButton = value;}}
        // タイトルのやめるボタン
        protected Button finishButton;
        public Button FinishButton{get{return finishButton;} set{finishButton = value;}}
        // エンドのタイトルに戻るボタン
        protected Button titleBackButton;
        public Button TitleBackButton{get{return titleBackButton;} set{titleBackButton = value;}}
        // メインのセーブしてやめるボタン
        protected  Button saveButton;
        public Button SaveButton{get{return saveButton;} set{saveButton = value;}}
        // セーブボタンが画面内にいるかのフラグ
        protected bool stayScreenSaveButton = true;

        // インスタンス化
        protected ActSaveButton saveButtonAct = new ActSaveButton();
        public ActSaveButton SaveButtonAct{get{return saveButtonAct;}}

        /// <summary>
        /// シーン遷移の条件を満たす関数を呼び出す
        /// </summary>
        public void CallOnSceneMoveFlag()
        {
            if(BaseScene.TmpScene.SceneTween == null)
            {
                BaseScene.TmpScene.SceneMoveFlagOn();
            }
        }

        
        void OnDestroy()
        {
            if(SaveButton != null)
            {
                DOTween.Kill(SaveButton.transform);
            }
        }
    }
}