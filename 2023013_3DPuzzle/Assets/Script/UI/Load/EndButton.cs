using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoadingImage;
using Scene;

namespace UI
{
    public class EndButton
    {
        /// <summary>
        /// ボタンを押したときのイベントを追加--
        /// EndからTitleの遷移はここに
        /// </summary>
        /// <param name="tmpImage">BaseLoadingImage</param>
        /// <param name="tmpScene">BaseScene</param>
        public void OnClickButton(BaseLoadingImage tmpImage, BaseScene tmpScene)
        {
            tmpScene.SceneMoveOnFlag = true;
            tmpScene.EndButton.onClick.AddListener(() => tmpScene.endSceneMove.Move(tmpScene, tmpImage));
        }

        /// <summary>
        /// エンドシーンボタン表示
        /// </summary>
        /// <param name="tmpScene">BaseScene</param>
        public void OnTitleButton(BaseScene tmpScene)
        {
            tmpScene.EndButtonImage.enabled = true;
        }

        /// <summary>
        /// エンドシーンボタン非表示
        /// </summary>
        /// <param name="tmpScene">BaseScene</param>
        public void OffTitleButton(BaseScene tmpScene)
        {
            tmpScene.EndButtonImage.enabled = false;
        }
    }
}