using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tile;
using DG.Tweening;


namespace Stage
{
    /// <summary>
    /// 落下タイル挙動関数管理クラス
    /// </summary>
    public class FallTileMove
    {
        /// <summary>
        /// タイルステージから離れるときの初期化関数
        /// </summary>
        /// <param name="tmpFallTile"></param>
        public void FallTileReset(BaseFallTile tmpFallTile)
        {
            foreach(var tmpObj in tmpFallTile.FallTiles)
            {
                var tmpTile = tmpObj.GetComponent<BaseTile>();
                // 落下するカウントを初期化して消えている場合は出現し直し
                if(tmpTile.FallCount != Const.FALL_COUNT_MAX)
                {
                    tmpObj.GetComponent<Renderer>().material = tmpTile.StartMaterial;
                    tmpTile.FallCount = Const.FALL_COUNT_MAX;
                }

                if(!tmpObj.transform.parent.gameObject.activeSelf)
                    tmpObj.transform.parent.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// エリアに入った時のカウントダウン処理
        /// </summary>
        /// <param name="tmpFallTile">落下タイルの実体</param>
        public async void TimeMove(BaseFallTile tmpFallTile, CancellationToken token)
        {
            if(InGameSceneController.Stages.StageState == Const.STATE_FALLING_STAGE && 
                tmpFallTile.TimeCountTask == null)
            {
                // TimeCountTaskにtimeCountを代入
                tmpFallTile.TimeCountTask = timeCount(tmpFallTile, token);

                Debug.Log("in");
                // timeCountを実行
                await (UniTask)tmpFallTile.TimeCountTask;
            }
        }

        /// <summary>
        /// エリアに入った時のカウントダウン処理のタスク
        /// </summary>
        /// <param name="tmpFallTile">落下タイルの実体</param>
        /// <param name="cts">キャンセル処理用Token</param>
        /// <returns>無し</returns>
        private async UniTask timeCount(BaseFallTile tmpFallTile, CancellationToken token)
        {
            // 制限時間半分になったら警告Panel出現
            var tmpTime = Const.FALL_COUNTDOWN_TIME / 2;
            await UniTask.Delay(tmpTime * Const.CHANGE_SECOND);

            // キャンセルが要求されている場合
            if ( token.IsCancellationRequested )
            {
                return;
            }
            // キャンセルが要求されていない場合
            else
            {
                // 半分になったら
                tmpFallTile.WarningPanel.enabled = true;
                // 1/4の明るさまで表示
                var tmpValue = Const.FADE_END_VALUE / 4;
                var tmpTweem = tmpFallTile.WarningPanel.DOFade(tmpValue,Const.FADE_TIMER).
                SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);

                await UniTask.Delay(tmpTime * Const.CHANGE_SECOND);

                // キャンセルが要求されている場合
                if ( token.IsCancellationRequested )
                {
                    return;
                }

                // スタート位置帰還
                InGameSceneController.Stages.MoveStage.StageFailure();

                // 初期化
                tmpFallTile.WarningPanel.enabled = false;
                FallTileReset(tmpFallTile);
                tmpTweem.Kill();
            }
        }
    }
}