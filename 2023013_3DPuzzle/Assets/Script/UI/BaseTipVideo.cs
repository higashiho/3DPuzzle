using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.Threading;
using DG.Tweening;

namespace Video
{
    /// <summary>
    ///  ビデオ管理Baseクラス
    /// </summary>
    public class BaseTipVideo : MonoBehaviour
    {
        // 再生ビデオ
        public VideoPlayer TipVideo{get; protected set;}
        // 再生中かどうか
        protected bool onPlay = false;
        public bool OnPlay{get{return onPlay;}set{onPlay = value;}}

        // タスクキャンセルフラグ
        protected bool playTaskFlag = false;
        public bool PlayTaskFlag{get{return playTaskFlag;}set{playTaskFlag = value;}}
        
        // タスクキャンセル用クラス
        public CancellationTokenSource cts{get;private set;} = new CancellationTokenSource();

        // インスタンス化
        protected TipVideoMove tipVideoMove;
        
        [SerializeField, Header("再生する動画")]
        protected VideoClip[] tipVideoClip = new VideoClip[5];
        public VideoClip[] TipVideoClip{get{return tipVideoClip;}}

        // フェイドイン用Tween
        protected Tween buttonFadeinTween = null;
        public Tween ButtonFadeinTween{get{return buttonFadeinTween;}set{buttonFadeinTween = value;}}
        // フェイドアウト用Tween
        protected Tween buttonFadeoutTween = null;
        public Tween ButtonFadeoutTween{get{return buttonFadeoutTween;}set{buttonFadeoutTween = value;}}

        /// <summary>
        /// ビデオ再生ボタン用関数
        /// </summary>
        public void PlayTipVideo()
        {
            tipVideoMove.PlayVideo();
        }
        
        /// <summary>
        /// ビデオ停止処理
        /// </summary>
        /// <param name="vp">再生中のビデオプレイヤー</param>
        public void FinishPlayingVideo(VideoPlayer vp)
        {
            // 再生中フラグを折って停止
            tipVideoMove.EndVideo();
            
        }
    }
}

