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
        public void SceneMove(BaseScene tmpScene, string tmpSceneName, BaseLoadingImage tmpImage, BaseScene.SceneState sceneState)
        {
            if(tmpScene.SceneMoveOnFlag && tmpScene.SceneTween == null)
            {   
                // フェードアウトする
                tmpScene.SceneTween = tmpScene.fadePanel.DOFade(endValue: Const.FADE_OUT_ALPHA, duration: Const.FADE_TIMER)
                .SetEase(Ease.Linear)
                .OnStart(() => 
                {   
                    // フェードアウト開始時に暗転フラグオフ
                    SceneMoveFlagOff(tmpScene);
                }   // フェードが終わったら
                ).OnComplete(() =>
                {   
                    // シーン読み込み
                    SceneManager.LoadScene(tmpSceneName);
                    // 読み込み中の画像を表示する
                    tmpImage.OnLoadingImages();
                    // ロード中の挙動
                    tmpImage.LoadingImageAnimation();
                    // 4秒待ってフェードイン
                    DOVirtual.DelayedCall(Const.WAIT_TIME, () => 
                    {                           
                        // ステートを変える
                        tmpScene.StateScene = sceneState;

                        sceneFadeIn(tmpScene, tmpImage);
                    }, false);               
                });
            }
        }

        /// <summary>
        /// シーン明け、フェードイン
        /// </summary>
        /// <param name="tmpScene"></param>
        public void FadeIn(BaseScene tmpScene)
        {
            tmpScene.fadePanel.DOFade(endValue: Const.FADE_IN_ALPHA, duration: Const.FADE_TIMER).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                tmpScene.fadePanel.enabled = false;
                tmpScene.SceneTween = null;
            });
        }

        /// <summary>
        /// 多重してシーン遷移が起こらないようにするフラグ
        /// </summary>
        /// <param name="tmpScene"></param>
        private void SceneMoveFlagOff(BaseScene tmpScene)
        {
            tmpScene.SceneMoveOnFlag = false;
        }

        /// <summary>
        /// 暗転が終わった後の処理
        /// </summary>
        /// <param name="tmpScene"></param>
        /// <param name="tmpImage"></param>
        private void sceneFadeIn(BaseScene tmpScene, BaseLoadingImage tmpImage)
        {
            // 微ずれ修正
            var tmpColor = tmpScene.fadePanel.color;
            tmpColor.a = Const.FADE_OUT_ALPHA;
            tmpScene.fadePanel.color = tmpColor;
            // シーン明け、フェードイン
            FadeIn(tmpScene);
            // 読み込み中画像を非表示にする
            tmpImage.ImageFill.OffLoadingImages(tmpImage);

            // 透明度、ロード中Tweenの初期化
            for(int i = 0; i < tmpImage.Caircles.Length; i++)
            {
                DOTween.Kill(tmpImage.Caircles[i]);
                tmpColor = tmpImage.Caircles[i].color;
                tmpColor.a = Const.FADE_MAX_ALPHA;
                tmpImage.Caircles[i].color = tmpColor;
            }
        }
    }
}
