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

        // インスタンス化
        private BaseNeedle tmpNeedle;

        // コンストラクタ
        public NeedleMove(BaseNeedle tmp)
        {
            tmpMoveCount = 0;
            tmpNeedle = tmp;
        }

        /// <summary>
        /// 挙動関数
        /// </summary>
        public void Move()
        {
            // ステートが針ステージの場合はプレイヤーへのムーブカウントで表示するニードルを変更
            if(InGameSceneController.Stages.StageState == StageConst.STATE_NEEDLE_STAGE)
            {
                // 初期化フラグを立てる
                tmpNeedle.ResetFlag = true;
                // 1/2で表示するニードルタイルを変更
                if((tmpNeedle.NeedleChangeCount & 1) == 0)
                {
                    changeNeedle("WhiteTile", Color.white);
                }
                else
                {
                    changeNeedle("BlackTile", Color.black);
                }

                // タイル更新
                tilesUpdate();
               
                // カウント格納変数を更新
                tmpMoveCount = InGameSceneController.Player.MoveCount;
                // ニードル変換中フラグOFF
                tmpNeedle.OnNeedleTrans = false;

                return;
            }

            // リセットフラグがたっていたら初期化
            if(tmpNeedle.ResetFlag)
            {
                // ステートが針ステージではない場合で針が出現している場合は針を表示しない
                if(tmpNeedle.NeedleTiles[0].activeSelf || tmpNeedle.NeedleTiles[1].activeSelf)
                {    
                    ResetTile();
                    foreach(var tmpObj in tmpNeedle.NeedleTiles)
                    {
                        tmpObj.transform.GetChild(0).gameObject.SetActive(false);
                    }     
                } 
            }
                
        }

        /// <summary>
        /// ニードルタイルの表示挙動関数
        /// </summary>
        /// <param name="tmpName">表示する種類</param>
        /// <param name="tmpColor">変換する色</param>
        private void changeNeedle(string tmpName, Color tmpColor)
        {
            foreach(var tmpObj in tmpNeedle.NeedleTiles)
            {
                // ブラックニードルタイルの時は表示以外は非表示
                if(tmpObj.transform.parent.tag == tmpName)
                {
                    // タイルの色を初期化して表示
                    tmpObj.transform.GetComponent<Renderer>().material.color = tmpColor;
                    if(!tmpObj.transform.GetChild(0).gameObject.activeSelf)
                        tmpObj.transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    if(tmpObj.transform.GetChild(0).gameObject.activeSelf)
                        tmpObj.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// タイルの状態を更新する関数
        /// </summary>
        private void tilesUpdate()
        {
            // プレイヤーの手数が２の時次出現するニードルタイルの色変更
            if((InGameSceneController.Player.MoveCount % StageConst.CHANGE_NEEDLE_NUM) == StageConst.CHANGE_NEEDLE_TILE_COLOR_NUM)
            {
                // プレイヤーが失敗していないときのみ更新
                if(InGameSceneController.Player.PlayerFailureTween == null && InGameSceneController.Player.PlayerClearTween == null)
                {
                    foreach(var tmpObj in tmpNeedle.NeedleTiles)
                    {
                        // 非表示のオブジェクトかつ色がまだ変わっていない場合タイルの色を変える
                        if(!tmpObj.transform.GetChild(0).gameObject.activeSelf && 
                        tmpObj.transform.GetComponent<Renderer>().material.color != Color.green
                            )
                            tmpObj.transform.GetComponent<Renderer>().material.color = Color.green;
                    }
                }
            }
            // 3手動いたら出現するニードルタイルを変更
            if((InGameSceneController.Player.MoveCount % StageConst.CHANGE_NEEDLE_NUM) == 0 &&
             tmpMoveCount != InGameSceneController.Player.MoveCount
             )
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
                if(tmpObj.transform.GetComponent<Renderer>().material.color == Color.green)
                {
                    // タイルがWhiteの場合色を黒に変える
                    if(tmpObj.transform.parent.tag == "WhiteTile")
                    {
                        tmpObj.transform.GetComponent<Renderer>().material.color = Color.white;
                    }
                    // タイルの色が黒の場合色を白に変える
                    else
                    {
                        tmpObj.transform.GetComponent<Renderer>().material.color = Color.black;
                    }
                }
            }
            // 初期化フラグ初期化
            tmpNeedle.ResetFlag = false;
        }

    }
}