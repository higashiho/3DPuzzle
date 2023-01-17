using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Scene
{
    public class EndSceneMove
    {
        public void Move(BaseScene tmpScene)
        {
            //エンドシーンからタイトルシーンへ
            tmpScene.MoveFade.FadeIn(tmpScene, "TitleScene");
            tmpScene.StateScene = BaseScene.SceneState.Title;
        }
    }
}
