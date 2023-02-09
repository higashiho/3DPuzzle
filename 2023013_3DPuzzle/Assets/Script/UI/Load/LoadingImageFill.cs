using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scene;

namespace LoadingImage
{
    public class LoadingImageFill
    {
        /// <summary>
        /// 読み込み中の画像を非表示にする
        /// </summary>
        public void OffLoadingImages(BaseLoadingImage tmpImage)
        {
            if(!BaseScene.TmpScene.SceneMoveOnFlag)
            {
                tmpImage.LoadingImages.enabled = false;
            }
        }
    }
}