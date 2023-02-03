using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using LoadingImage;

namespace Scene
{
    public class FadeMove
    {
        /// <summary>
        /// フェードイン・ロードシーン・フェードアウト
        /// </summary>
        /// <param name="tmpScene">BaseScene</param>
        /// <param name="tmpSceneName">次のシーンの名前</param>
        public void SceneMove(BaseScene tmpScene, string tmpSceneName, BaseLoadingImage tmpImage)
        {
                if(tmpScene.SceneMoveOnFlag)
                {   // フェードアウトする
                    tmpScene.fadePanel.DOFade(endValue: Const.FADE_OUT_ALPHA, duration: Const.FADE_TIMER)
                    .SetEase(Ease.Linear)
                    .OnStart(() => 
                    {   // フェードアウト開始時に暗転フラグオフ
                        SceneMoveFlagOff(tmpScene);
                    }   // フェードが終わったらシーンの状態をメインにする
                    ).OnComplete(() =>
                    {
                        // 4秒待ってシーン遷移、フェードイン
                        DOVirtual.DelayedCall(Const.WAIT_TIME, () => 
                        {
                            // 微ずれ修正
                            var tmpColor = tmpScene.fadePanel.color;
                            tmpColor.a = Const.FADE_OUT_ALPHA;
                            tmpScene.fadePanel.color = tmpColor;

                            // シーン読み込み
                            SceneManager.LoadScene(tmpSceneName);

                            // ステートをメインに変える
                            tmpScene.StateScene = BaseScene.SceneState.Main;
                        });
                        
                    });
                }
        }

        /// <summary>
        /// シーン明け、フェードイン
        /// </summary>
        /// <param name="tmpScene"></param>
        public void FadeIn(BaseScene tmpScene)
        {
            tmpScene.fadePanel.DOFade(endValue: Const.FADE_IN_ALPHA, duration: Const.FADE_TIMER).SetEase(Ease.Linear);
        }

        /// <summary>
        /// 多重してシーン遷移が起こらないようにするフラグ
        /// </summary>
        /// <param name="tmpScene"></param>
        private void SceneMoveFlagOff(BaseScene tmpScene)
        {
            tmpScene.SceneMoveOnFlag = false;
        }
    }
}
