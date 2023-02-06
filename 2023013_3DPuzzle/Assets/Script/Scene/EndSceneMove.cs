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
            DOVirtual.DelayedCall(Const.WAIT_TIME, () =>
            {
                tmpScene.RetryButtonImage.DOFade(endValue: Const.FADE_OUT_ALPHA, duration: Const.FADE_TIMER).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    tmpImage.SceneButton.OnEndButton(tmpScene);
                });
            });
            // エンドシーンからタイトルシーンへ
            tmpScene.MoveFade.SceneMove(tmpScene, "TitleScene", tmpImage, BaseScene.SceneState.Title);
        }
    }
}
