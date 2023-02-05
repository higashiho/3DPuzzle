using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoadingImage;

namespace Scene
{
    public class TitleSceneMove
    {
        /// <summary>
        /// TitleからMainへ
        /// </summary>
        /// <param name="tmpScene">BaseScene</param>
        /// <param name="tmpimage">BaseLoadingImage</param>
        public void Move(BaseScene tmpScene, BaseLoadingImage tmpimage)
        {
            //タイトルシーンからメインシーンへ
            tmpScene.MoveFade.SceneMove(tmpScene, "MainScene", tmpimage, BaseScene.SceneState.Main);
        }
    }

}