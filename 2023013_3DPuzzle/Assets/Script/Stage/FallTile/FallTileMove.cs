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

        private BaseFallTile tmpFallTile;
        public FallTileMove(BaseFallTile tmp)
        {
            tmpFallTile = tmp;
        }
        /// <summary>
        /// タイルステージから離れるときの初期化関数
        /// </summary>
        public void FallTileReset()
        {
            // 落下タイルリセット
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

                if(tmpObj.tag == "SwitchTile")
                {
                    tmpObj.GetComponent<Renderer>().material.color = tmpObj.GetComponent<BaseTile>().StartColor;
                    tmpObj.GetComponent<Renderer>().material = tmpTile.StartMaterial;
                    tmpObj.tag = "Fall";
                }
            }

            // スイッチタイルリセット
            foreach(var tmpObj in InGameSceneController.Stages.SwitchTiles)
            {
                if(tmpObj == null)
                    break;

                if(tmpObj.tag != "SwitchTile")
                {
                    tmpObj.tag = "SwitchTile";
                    tmpObj.GetComponent<Renderer>().material.color = tmpObj.GetComponent<BaseTile>().StartColor;
                }
            }

            // 初期化
            tmpFallTile.TimeCountTask = null;
            InGameSceneController.Stages.TileChangeFlag = true;
            InGameSceneController.Stages.ClearCount = Const.MAX_GOAL_NUM;

        }

        /// <summary>
        /// エリアに入った時のカウントダウン処理
        /// </summary>
        public async void TimeMoveAsync()
        {
            if(InGameSceneController.Stages.StageState == Const.STATE_FALLING_STAGE)
            {
                if(tmpFallTile.TimeCountTask == null)
                {
                    // TimeCountTaskにtimeCountを代入
                    tmpFallTile.TimeCountTask = timeCountAsyck();

                    Debug.Log("in");
                    // timeCountを実行
                    await (UniTask)tmpFallTile.TimeCountTask;
                }
                return;
            }

            // ステートが落下ステージではないとき
            FallTileReset();
            BaseFallTile.Cts.Cancel();
            DOTween.Kill(tmpFallTile.WarningPanel);


        }

        /// <summary>
        /// エリアに入った時のカウントダウン処理のタスク
        /// </summary>
        /// <returns>無し</returns>
        private async UniTask timeCountAsyck()
        {
            // 制限時間半分になったら警告Panel出現
            var tmpTime = Const.FALL_COUNTDOWN_TIME / 2;
            await UniTask.Delay(tmpTime * Const.CHANGE_SECOND);

            // Cancel処理
            if(BaseFallTile.Cts.Token.IsCancellationRequested)
            {
                Debug.Log("Cancel");
                return;
            }

            // 半分になったら画面を赤くするパネル表示
            tmpFallTile.WarningPanel.enabled = true;
            // 1/4の明るさまで表示
            var tmpValue = Const.FADE_END_VALUE / 4;
            var tmpTweem = tmpFallTile.WarningPanel.DOFade(tmpValue,tmpTime).
            SetEase(Ease.Linear).OnKill(() => tmpFallTile.WarningPanel.enabled = false);

            await UniTask.Delay(tmpTime * Const.CHANGE_SECOND);
            var tmpColor = tmpFallTile.WarningPanel.color;

            // Cancel処理
            if(BaseFallTile.Cts.Token.IsCancellationRequested)
            {
                Debug.Log("Cancel");
                tmpTweem.Kill();
                // 初期化
                tmpColor.a = 0;
                tmpFallTile.WarningPanel.color = tmpColor;

                return;
            }
            // スタート位置帰還
            InGameSceneController.Stages.MoveStage.StageFailure();

            // 初期化
            tmpColor.a = 0;
            tmpFallTile.WarningPanel.color = tmpColor;
            tmpFallTile.WarningPanel.enabled = false;            
            FallTileReset();
            tmpTweem.Kill();
        }
    }
}