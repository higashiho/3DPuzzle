using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class FadeMove
    {
        //フェードイン・ロードシーン・フェードアウト
        public void FadeIn(BaseScene tmpScene, string tmpSceneName)
        {
                if(Input.GetKeyDown(KeyCode.Return))
                {
                    tmpScene.fadePanel.DOFade(endValue: Const.FADE_END_VALUE,duration: Const.FADE_TIMER).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        fadeOut(tmpScene);
                        SceneManager.LoadScene(tmpSceneName);
                    });
                }
        }

            //フェードアウト
        private void fadeOut(BaseScene tmpScene)
        {
            tmpScene.fadePanel.DOFade(endValue: 1,duration: Const.FADE_TIMER).SetEase(Ease.Linear);
        }
    }
}
