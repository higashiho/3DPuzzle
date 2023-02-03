using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Video
{
    public class TipVideoMove
    {
        // インスタンス化
        private BaseTipVideo tmpVideo;
        public TipVideoMove(BaseTipVideo tmp)
        {
            tmpVideo = tmp;
        }

        /// <summary>
        /// 再生処理
        /// </summary>
        public async void PlayVideo()
        {
            if(!tmpVideo.OnPlay)
            {
                // フラグを折って停止コールバックに追加して再生
                tmpVideo.OnPlay = true;
                tmpVideo.TipVideo.loopPointReached += tmpVideo.FinishPlayingVideo;
                tmpVideo.TipVideo.Play();
            }
            // フラグがたっているときに押されたら消す
            else
            {
                tmpVideo.FinishPlayingVideo(tmpVideo.TipVideo);
                return;
            }
            // アルファ値を1秒かけて最大値にする
            while(true)
            {
                // アルファ値が最大値になったら抜ける
                if(tmpVideo.TipVideo.targetCameraAlpha >= Const.FADE_MAX_ALPHA)
                    break;

                // １０ミリ秒に0.1アルファ値を増やす
                tmpVideo.TipVideo.targetCameraAlpha += Const.FADE_VIDEO_NUM;
                await UniTask.Delay(Const.FADE_VIDEO_TIME);
            }
        }

        /// <summary>
        /// 動が終了挙動
        /// </summary>
        /// <returns></returns>
        public async void EndVideo()
        {
            // アルファ値を1秒かけて0にする
            while(true)
            {
                // アルファ値が0になったら抜ける
                if(tmpVideo.TipVideo.targetCameraAlpha <= 0)
                    break;

                // １０ミリ秒に0.1アルファ値を減らす
                tmpVideo.TipVideo.targetCameraAlpha -= Const.FADE_VIDEO_NUM;
                await UniTask.Delay(Const.FADE_VIDEO_TIME);
            }
            
            // 初期化
            tmpVideo.OnPlay = false;
            tmpVideo.TipVideo.Stop();
        }
    }
}

