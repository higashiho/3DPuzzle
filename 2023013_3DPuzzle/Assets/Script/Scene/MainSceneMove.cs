using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using LoadingImage;

namespace Scene
{
    public class MainSceneMove
    {
        /// <summary>
        /// MainシーンからEndシーンへ
        /// </summary>
        /// <param name="tmpScene">BaseScene</param>
        /// <param name="tmpImage">BaseLoadingImage</param>
        public void Move(BaseScene tmpScene, BaseLoadingImage tmpImage)
        {
            //メインシーンからエンドシーンへ
            tmpScene.MoveFade.SceneMove(tmpScene, "EndScene", tmpImage, BaseScene.SceneState.End);
        }
    }
}