using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UI;

namespace Scene
{
    public class EndSceneMove
    {
        // シーン遷移
        public void Move(BaseScene tmpScene)
        {
            //エンドシーンからタイトルシーンへ
            tmpScene.MoveFade.FadeIn(tmpScene, "TitleScene");
            //tmpScene.StateScene = BaseScene.SceneState.Title;
        }
    }
}
