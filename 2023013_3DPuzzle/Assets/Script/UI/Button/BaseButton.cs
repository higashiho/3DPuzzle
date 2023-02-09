using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scene;
using UnityEngine.UI;

namespace button
{
    public class BaseButton : MonoBehaviour
    {
        protected Button startButton;
        public Button StartButton{get{return startButton;} set{startButton = value;}}
        protected Button restartButton;
        public Button RestartButton{get{return restartButton;} set{restartButton = value;}}
        protected Button finishButton;
        public Button FinishButton{get{return finishButton;} set{finishButton = value;}}
        protected Button titleBackButton;
        public Button TitleBackButton{get{return titleBackButton;} set{titleBackButton = value;}}

        public EndSceneButtonCon EndSceneButton{get; set;} = new EndSceneButtonCon();

        /// <summary>
        /// タイトルシーンに戻る関数を呼ぶ
        /// </summary>
        public void CallRestart()
        {
            EndSceneButton.Restart();
        }

        /// <summary>
        /// シーン遷移の条件を満たす関数を呼び出す
        /// </summary>
        public void CallOnSceneMoveFlag()
        {
            BaseScene.TmpScene.SceneMoveFlagOn();
        }
    }
}