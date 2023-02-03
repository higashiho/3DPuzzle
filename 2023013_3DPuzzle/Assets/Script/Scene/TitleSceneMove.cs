using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoadingImage;

namespace Scene
{
    public class TitleSceneMove
    {
        // シーン遷移
        public void Move(BaseScene tmpScene, BaseLoadingImage tmpimage)
        {
            //タイトルシーンからメインシーンへ
            tmpScene.MoveFade.SceneMove(tmpScene, "MainScene", tmpimage);
            //tmpScene.StateScene = BaseScene.SceneState.Main;
        }
    }

}