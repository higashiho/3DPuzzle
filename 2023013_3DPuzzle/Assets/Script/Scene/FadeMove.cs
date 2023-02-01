using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class FadeMove
    {
        /// <summary>
        /// フェードイン・ロードシーン・フェードアウト
        /// </summary>
        /// <param name="tmpScene"></param>
        /// <param name="tmpSceneName"></param>
        public void FadeIn(BaseScene tmpScene, string tmpSceneName)
        {
                if(tmpScene.OnToButtonFlag && Input.GetMouseButtonDown(0))
                {
                    tmpScene.fadePanel.DOFade(endValue: Const.FADE_END_VALUE, duration: Const.FADE_TIMER)
                    .SetEase(Ease.Linear).OnComplete(() =>
                    {
                        // 非同期処理とする??
                        tmpScene.Async = SceneManager.LoadSceneAsync(tmpSceneName);
                        fadeOut(tmpScene);
                        SceneManager.LoadScene(tmpSceneName);
                    });
                }
        }

        /// <summary>
        /// フェードアウト
        /// </summary>
        /// <param name="tmpScene"></param>
        private void fadeOut(BaseScene tmpScene)
        {
            tmpScene.fadePanel.DOFade(endValue: 1, duration: Const.FADE_TIMER).SetEase(Ease.Linear);
        }
    }
}
