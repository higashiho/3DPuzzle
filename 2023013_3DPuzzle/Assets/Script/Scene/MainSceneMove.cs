using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UI;

namespace Scene
{
    public class MainSceneMove
    {
        public void Move(BaseScene tmpScene)
            {
                //メインシーンからエンドシーンへ
                tmpScene.MoveFade.FadeIn(tmpScene, "EndScene");
                //tmpScene.StateScene = BaseScene.SceneState.End;
            }
    }
}