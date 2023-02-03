using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using LoadingImage;

namespace Scene
{
    public class MainSceneMove
    {
        public void Move(BaseScene tmpScene, BaseLoadingImage tmpImage)
        {
            //メインシーンからエンドシーンへ
            tmpScene.MoveFade.SceneMove(tmpScene, "EndScene", tmpImage);
            //tmpScene.StateScene = BaseScene.SceneState.End;
        }
    }
}