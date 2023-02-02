using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.Threading;

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
        protected VideoClip[] TipVideoClip{get{return tipVideoClip;}}

        /// <summary>
        /// 再生するビデオ判断用
        /// </summary>
        protected void changeClip()
        {

            switch(InGameSceneController.Stages.StageState)
            {
                case Const.STATE_START:
                    // クリップが変更されていないとクリップ変更
                    if(TipVideo.clip != tipVideoClip[0])
                        TipVideo.clip = tipVideoClip[0];
                    break;
                case StageConst.STATE_NEEDLE_STAGE:
                    // クリップが変更されていないとクリップ変更
                    if(TipVideo.clip != tipVideoClip[1])
                        TipVideo.clip = tipVideoClip[1];
                        break;
                case StageConst.STATE_FALLING_STAGE:
                    // クリップが変更されていないとクリップ変更
                    if(TipVideo.clip != tipVideoClip[2])
                        TipVideo.clip = tipVideoClip[2];
                    break;
                case StageConst.STATE_MOVE_STAGE:
                    // クリップが変更されていないとクリップ変更
                    if(TipVideo.clip != tipVideoClip[3])
                        TipVideo.clip = tipVideoClip[3];
                    break;
                case StageConst.STATE_SWITCH_STAGE:
                    // クリップが変更されていないとクリップ変更
                    if(TipVideo.clip != tipVideoClip[4])
                        TipVideo.clip = tipVideoClip[4];
                    break;
                default:   
                    break;
            }
        }
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
        /// <param name="vp">ビデオplayer</param>
        public void FinishPlayingVideo(VideoPlayer vp)
        {
            // 再生中フラグを折って停止
            tipVideoMove.EndVideo();
            
        }
    }
}

