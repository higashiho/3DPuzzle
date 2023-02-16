using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;

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
            if(InGameSceneController.Player.PlayerClearTween == null && InGameSceneController.Player.PlayerFailureTween == null)
            {    
                if(!tmpVideo.OnPlay)
                {
                    // フラグを折って停止コールバックに追加して再生
                    tmpVideo.PlayTaskFlag = false;
                    tmpVideo.OnPlay = true;
                    tmpVideo.TipVideo.loopPointReached += tmpVideo.FinishPlayingVideo;
                    tmpVideo.TipVideo.Play();
                    // アルファ値を1秒かけて最大値にする
                    while(true)
                    {
                        // アルファ値が最大値になったら抜ける
                        if(tmpVideo.TipVideo.targetCameraAlpha >= Const.FADE_MAX_ALPHA || tmpVideo.PlayTaskFlag)
                            break;

                        // １０ミリ秒に0.1アルファ値を増やす
                        tmpVideo.TipVideo.targetCameraAlpha += Const.FADE_VIDEO_NUM;
                        await UniTask.Delay(Const.FADE_VIDEO_TIME);
                    }
                }
                // フラグがたっているときに押されたら消す
                else
                {
                    tmpVideo.FinishPlayingVideo(tmpVideo.TipVideo);
                    return;
                }
                
            }
        }

        /// <summary>
        /// ボタンTween挙動関数
        /// </summary>
        /// <param name="alpha">目標アルファ値</param>
        /// <param name="resetAlpha">初期化アルファ値</param>
        /// <returns>動作Tween</returns>
        private Tween buttonMove(float alpha, float resetAlpha)
        {
            // フェイド時間調整
            var tmpNum = Const.FADE_TIMER * 0.5f;
            // 取得
            var tmpImage = InGameSceneController.TreasureBox.TipButton.GetComponent<Image>();
            var tmpText = InGameSceneController.TreasureBox.TipButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            // Tweenを削除して初期化
            DOTween.Kill(tmpImage);
            DOTween.Kill(tmpText);
            // 色初期化
            var tmpColor = tmpImage.color;
            tmpColor.a = resetAlpha;
            tmpImage.color = tmpColor;
            tmpColor = tmpText.color;
            tmpColor.a = resetAlpha;
            tmpText.color = tmpColor;

            // Tween挙動開始
            var tmpTween = tmpImage.DOFade(alpha, tmpNum).SetEase(Ease.Linear);
            tmpText.DOFade(alpha, tmpNum).SetEase(Ease.Linear);

            return tmpTween;
        }

        
        /// <summary>
        /// 再生するビデオ判断用
        /// </summary>
        public void changeClip()
        {
            // 参照オブジェクトがnullの場合関数終了
            if(InGameSceneController.TreasureBox == null)
                return;
            // プレイヤーが安置にいるときのみ処理
            if(InGameSceneController.Stages.StageState == Const.STATE_START)
            {
                // TipButtonを表示
                if(tmpVideo.ButtonFadeoutTween == null)
                {   
                    tmpVideo.ButtonFadeinTween = null;
                    InGameSceneController.TreasureBox.TipButton.gameObject.SetActive(true);
                    tmpVideo.ButtonFadeoutTween = buttonMove(Const.FADE_MAX_ALPHA, 0);
                }

                // playerのposが一定以内の場合
                if(InGameSceneController.Player.transform.position.x <= StageConst.TipMoviePos[0].x &&
                    InGameSceneController.Player.transform.position.z >= StageConst.TipMoviePos[0].z)
                    {
                        if(tmpVideo.TipVideo.clip != tmpVideo.TipVideoClip[1])
                            tmpVideo.TipVideo.clip = tmpVideo.TipVideoClip[1];
                        return;
                    }

                // playerのposが一定以内の場合
                if(InGameSceneController.Player.transform.position.x <= StageConst.TipMoviePos[1].x &&
                    InGameSceneController.Player.transform.position.z <= StageConst.TipMoviePos[1].z)
                    {
                        // クリップが変更されていないとクリップ変更
                        if(tmpVideo.TipVideo.clip != tmpVideo.TipVideoClip[2])
                            tmpVideo.TipVideo.clip = tmpVideo.TipVideoClip[2];
                        return;
                    }

                // playerのposが一定以内の場合
                if(InGameSceneController.Player.transform.position.x >= StageConst.TipMoviePos[2].x &&
                    InGameSceneController.Player.transform.position.z >= StageConst.TipMoviePos[2].z)
                    {
                        // クリップが変更されていないとクリップ変更
                        if(tmpVideo.TipVideo.clip != tmpVideo.TipVideoClip[3])
                            tmpVideo.TipVideo.clip = tmpVideo.TipVideoClip[3];
                        return;
                    }

                // playerのposが一定以内の場合
                if(InGameSceneController.Player.transform.position.x >= StageConst.TipMoviePos[3].x &&
                    InGameSceneController.Player.transform.position.z <= StageConst.TipMoviePos[3].z)
                    {
                        // クリップが変更されていないとクリップ変更
                        if(tmpVideo.TipVideo.clip != tmpVideo.TipVideoClip[4])
                            tmpVideo.TipVideo.clip = tmpVideo.TipVideoClip[4];
                        return;
                    }
                
                
                // クリップが変更されていないとクリップ変更
                if(tmpVideo.TipVideo.clip != tmpVideo.TipVideoClip[0])
                    tmpVideo.TipVideo.clip = tmpVideo.TipVideoClip[0];
            
                }
            // 安置の中にいなかったら
            else
            {
                // TipButtonを非表示
                if(tmpVideo.ButtonFadeinTween == null)
                {
                    tmpVideo.ButtonFadeoutTween = null;
                    tmpVideo.ButtonFadeinTween = buttonMove(0, Const.FADE_MAX_ALPHA);
                }
                // ボタンが表示されていてアルファ値が0の場合
                if(InGameSceneController.TreasureBox.TipButton.gameObject.activeSelf 
                && InGameSceneController.TreasureBox.TipButton.GetComponent<Image>().color.a == 0)
                {
                    InGameSceneController.TreasureBox.TipButton.gameObject.SetActive(false);
                }
            }
                
                
        }

        /// <summary>
        /// 動画終了挙動
        /// </summary>
        /// <returns></returns>
        public async void EndVideo()
        {
            tmpVideo.OnPlay = false;
            // 初期化
            tmpVideo.TipVideo.Stop();
            // アルファ値を1秒かけて0にする
            while(true)
            {
                // アルファ値が0になったら抜ける
                if(tmpVideo.TipVideo.targetCameraAlpha <= 0 || tmpVideo.OnPlay)
                    break;

                // １０ミリ秒に0.1アルファ値を減らす
                tmpVideo.TipVideo.targetCameraAlpha -= Const.FADE_VIDEO_NUM;
                await UniTask.Delay(Const.FADE_VIDEO_TIME);
            }
            
        }
    }
}

