using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scene;
using DG.Tweening;
using UnityEngine.UI;

namespace LoadingImage
{
    public class LoadingImageFill
    {
        /// <summary>
        /// 読み込み中の画像を非表示にする
        /// </summary>
        public void OffLoadingImages(BaseLoadingImage tmpImage, BaseScene tmpScene)
        {
            if(!tmpScene.SceneMoveOnFlag)
            {
                tmpImage.LoadingImages.enabled = false;
            }
        }
    }
}