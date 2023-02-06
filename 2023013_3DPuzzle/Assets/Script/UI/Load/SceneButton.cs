using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scene;
using DG.Tweening;

namespace UI
{
    public class SceneButton
    {
        /// <summary>
        /// エンドシーンボタン表示
        /// </summary>
        /// <param name="tmpScene">BaseScene</param>
        public void OnEndButton(BaseScene tmpScene)
        {
            tmpScene.RetryButtonImage.enabled = true;
        }

        /// <summary>
        /// エンドシーンボタン非表示
        /// </summary>
        /// <param name="tmpScene">BaseScene</param>
        public void OffEndButton(BaseScene tmpScene)
        {
            tmpScene.RetryButtonImage.enabled = false;
        }

        /// <summary>
        /// タイトルのボタンを表示
        /// </summary>
        /// <param name="tmpScene"></param>
        public void OnTitleButton(BaseScene tmpScene)
        {
            tmpScene.StartButton.enabled = true;
            tmpScene.FinishButton.enabled = true;
        }

        /// <summary>
        /// タイトルのボタンを非表示
        /// </summary>
        /// <param name="tmpScene"></param>
        public void OffTitleButton(BaseScene tmpScene)
        {
            tmpScene.StartButton.DOFade(endValue: Const.FADE_IN_ALPHA, duration: Const.FADE_TIMER).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                tmpScene.StartButton.enabled = false;
            });
            tmpScene.FinishButton.DOFade(endValue: Const.FADE_IN_ALPHA, duration: Const.FADE_TIMER).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                tmpScene.FinishButton.enabled = false;
            });
        }
    }
}