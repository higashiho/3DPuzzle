using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using LoadingImage;

namespace Scene
{
    public class EndSceneMove
    {
        // シーン遷移
        public void Move(BaseScene tmpScene, BaseLoadingImage tmpImage)
        {
            BaseLoadingImage.tmpImage.SceneButton.OnEndButton(tmpScene);
            // エンドシーンからタイトルシーンへ
            tmpScene.MoveFade.SceneMove(tmpScene, "TitleScene", tmpImage, BaseScene.SceneState.Title);
            if(BaseScene.TmpScene.RetryButton.enabled)
            {
                // エンドシーンのボタン非表示
                tmpImage.SceneButton.OffEndButton(tmpScene);
            }
        }
    }
}
