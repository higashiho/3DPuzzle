using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Stage
{
    /// <summary>
    /// 針ステージ挙動関数管理クラス
    /// </summary>
    public class NeedleMove
    {
        // ムーブカウントが増えたか確認用変数
        private int tmpMoveCount;

        // コンストラクタ
        public NeedleMove()
        {
            tmpMoveCount = 0;
        }

        /// <summary>
        /// 挙動関数
        /// </summary>
        /// <param name="tmpNeedle">ニードルの実体</param>
        public void Move(BaseNeedle tmpNeedle)
        {
            // ステートが針ステージの場合はプレイヤーへのムーブカウントで表示するニードルを変更
            if(InGameSceneController.Stages.StageState == Const.STATE_NEEDLE_STAGE)
            {
                // 1/2で表示するニードルタイルを変更
                if((tmpNeedle.NeedleChangeCount & 1) == 0)
                {
                    changeNeedle(tmpNeedle, "WhiteTile", Color.black);
                }
                else
                {
                    changeNeedle(tmpNeedle, "BlackTile", Color.white);
                }

                // タイル更新
                tilesUpdate(tmpNeedle);
               
                // カウント格納変数を更新
                tmpMoveCount = tmpNeedle.PlyaerMoveCount;
                return;
            }

            // ステートが針ステージではない場合は針を表示しない
            foreach(var tmpObj in tmpNeedle.NeedleTiles)
            {
                tmpObj.transform.GetChild(0).gameObject.SetActive(false);
                tmpObj.transform.GetChild(1).gameObject.SetActive(false);
            }           
        }

        /// <summary>
        /// ニードルタイルの表示挙動関数
        /// </summary>
        /// <param name="tmpNeedle">ニードルの実体</param>
        /// <param name="tmpName">表示する種類</param>
        /// <param name="tmpColor">変換する色</param>
        private void changeNeedle(BaseNeedle tmpNeedle, string tmpName, Color tmpColor)
        {
            foreach(var tmpObj in tmpNeedle.NeedleTiles)
            {
                // 初回のみ処理実装
                if((tmpNeedle.PlyaerMoveCount % Const.CHANGE_NEEDLE_NUM) != 0)
                    break;
                // ブラックニードルタイルの時は表示以外は非表示
                if(tmpObj.transform.parent.tag == tmpName)
                {
                    // タイルの色を初期化して表示
                    tmpObj.transform.parent.GetComponent<Renderer>().material.color = tmpColor;
                    tmpObj.transform.GetChild(0).gameObject.SetActive(true);
                    tmpObj.transform.GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    tmpObj.transform.GetChild(0).gameObject.SetActive(false);
                    tmpObj.transform.GetChild(1).gameObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// タイルの状態を更新する関数
        /// </summary>
        /// <param name="tmpNeedle">ニードルの実体</param>
        private void tilesUpdate(BaseNeedle tmpNeedle)
        {
            // プレイヤーの手数が２の時次出現するニードルタイルの色変更
            if((tmpNeedle.PlyaerMoveCount % Const.CHANGE_NEEDLE_NUM) == Const.CHANGE_NEEDLE_TILE_COLOR_NUM)
            {
                // プレイヤーが失敗していないときのみ更新
                if(InGameSceneController.Player.PlayerFailureTween == null && InGameSceneController.Player.PlayerClearTween == null)
                {
                    foreach(var tmpObj in tmpNeedle.NeedleTiles)
                    {
                        // 非表示のオブジェクトかつ色がまだ変わっていない場合タイルの色を変える
                        if(!tmpObj.transform.GetChild(0).gameObject.activeSelf && 
                        tmpObj.transform.parent.GetComponent<Renderer>().material.color != Color.green
                            )
                            tmpObj.transform.parent.GetComponent<Renderer>().material.color = Color.green;
                    }
                }
            }
            // 3手動いたら出現するニードルタイルを変更
            if((tmpNeedle.PlyaerMoveCount % Const.CHANGE_NEEDLE_NUM) == 0 && tmpMoveCount != tmpNeedle.PlyaerMoveCount)
                tmpNeedle.NeedleChangeCount++;
        }
        
        /// <summary>
        /// タイルの変化状態を戻す関数
        /// </summary>
        public void ResetTile()
        {
            // タイルの色が変わっている際色を戻す
            foreach(var tmpObj in InGameSceneController.Stages.transform.GetChild(1).GetComponent<BaseNeedle>().NeedleTiles)
            {
                // ニードルタイルで外枠の色が緑色がある場合
                if(tmpObj.transform.parent.GetComponent<Renderer>().material.color == Color.green)
                {
                    // タイルがWhiteの場合色を黒に変える
                    if(tmpObj.transform.parent.tag == "WhiteTile")
                    {
                        tmpObj.transform.parent.GetComponent<Renderer>().material.color = Color.black;
                    }
                    // タイルの色が黒の場合色を白に変える
                    else
                    {
                        tmpObj.transform.parent.GetComponent<Renderer>().material.color = Color.white;
                    }
                }
            }
        }

    }
}