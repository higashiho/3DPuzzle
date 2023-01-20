using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Scene
{
    public class TitleSceneMove
    {
        //シーン遷移
        public void Move(BaseScene tmpScene)
        {
            //タイトルシーンからメインシーンへ
                tmpScene.MoveFade.FadeIn(tmpScene, "MainScene");
                tmpScene.StateScene = BaseScene.SceneState.Main;
        } 
    }
}