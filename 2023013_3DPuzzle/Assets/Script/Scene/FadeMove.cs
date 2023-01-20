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
                        SceneManager.LoadScene(tmpSceneName);
                        fadeOut(tmpScene);
                    });
                }
        }

            //フェードアウト
        private void fadeOut(BaseScene tmpScene)
        {
            tmpScene.fadePanel.DOFade(endValue: 0,duration: Const.FADE_TIMER).SetEase(Ease.Linear);
        }
    }
}
