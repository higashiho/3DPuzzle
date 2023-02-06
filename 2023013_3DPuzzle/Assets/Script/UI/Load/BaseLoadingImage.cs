using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UI;

namespace LoadingImage
{
    public class BaseLoadingImage : MonoBehaviour
    {
        [SerializeField,Header("読み込み中の画像")]
        protected Canvas loadingImages;
        public Canvas LoadingImages{get{return loadingImages;}private set{loadingImages = value;}}
        [SerializeField,Header("読み込み中の画像")]
        protected Image[] caircles;
        // 読み込み中の画像の群
        public Image[] Caircles{get{return caircles;}set{caircles = value;}}
        //インスタンス化
        public LoadingImageFill ImageFill{get;private set;} = new LoadingImageFill();
        public static BaseLoadingImage tmpImage{get; set;}
        protected SceneButton sceneButton = new SceneButton();
        public SceneButton SceneButton{get;private set;} = new SceneButton();

        /// <summary>
        /// ロード画面読み込み画像--
        /// 円形に等間隔に並べて点滅
        /// </summary>
        public void LoadingImageAnimation()
        {
            for (var i = 0; i < tmpImage.Caircles.Length; i++)
            {
                // 画像を円状に並べる
                var angle = -2 * Mathf.PI * i / tmpImage.Caircles.Length;
                // 一つ一つの画像の間隔
                tmpImage.Caircles[i].rectTransform.anchoredPosition = 
                new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * Const.LOADING_IMAGE_INTERVAL;
                // 画像を遅らせながらループして点滅
                tmpImage.Caircles[i].DOFade(Const.FADE_IN_ALPHA, Const.DURATION_SPEED).SetLoops(Const.LOADING_ANIMATION_INFINITY, LoopType.Yoyo)
                .SetDelay(Const.DURATION_SPEED * i / tmpImage.Caircles.Length);
            } 
        }

        /// <summary>
        /// 読み込み中画像表示
        /// </summary>
        public void OnLoadingImages()
        {
            LoadingImages.enabled = true;
        }

        protected void OnDestroy()
        {
            DOTween.KillAll();
        }
    }
}