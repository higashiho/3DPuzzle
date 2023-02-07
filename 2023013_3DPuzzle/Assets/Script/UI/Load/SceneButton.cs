using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scene;
using TMPro;
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
            tmpScene.RestartButton.enabled = true;
        }

        /// <summary>
        /// タイトルのボタンを非表示
        /// </summary>
        /// <param name="tmpScene"></param>
        public void OffTitleButton(BaseScene tmpScene)
        {
            // 子のテキストとボタンをフェイドインさせる
            tmpScene.StartButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>()
            .DOFade(endValue: Const.FADE_IN_ALPHA, duration: Const.FADE_TIMER).SetEase(Ease.Linear);
            tmpScene.StartButton.DOFade(endValue: Const.FADE_IN_ALPHA, duration: Const.FADE_TIMER).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                tmpScene.StartButton.gameObject.SetActive(false);
            });
            // 子のテキストとボタンをフェイドインさせる
            tmpScene.FinishButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>()
            .DOFade(endValue: Const.FADE_IN_ALPHA, duration: Const.FADE_TIMER).SetEase(Ease.Linear);
            tmpScene.FinishButton.DOFade(endValue: Const.FADE_IN_ALPHA, duration: Const.FADE_TIMER).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                tmpScene.FinishButton.gameObject.SetActive(false);
            });
            // 子のテキストとボタンをフェイドインさせる
            tmpScene.RestartButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>()
            .DOFade(endValue: Const.FADE_IN_ALPHA, duration: Const.FADE_TIMER).SetEase(Ease.Linear);
            tmpScene.RestartButton.DOFade(endValue: Const.FADE_IN_ALPHA, duration: Const.FADE_TIMER).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                tmpScene.RestartButton.gameObject.SetActive(false);
            });
        }
    }
}