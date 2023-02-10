using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stage;

namespace Tile
{
    /// <summary>
    /// スイッチタイルの挙動関数管理クラス
    /// </summary>
    public class SwitchTileMove
    {
        // インスタンス化
        private BaseSwitchTile tmpSwitchTile;

        public SwitchTileMove(BaseSwitchTile tmp)
        {
            tmpSwitchTile = tmp;
        }

        /// <summary>
        /// スイッチタイルステージ挙動関数
        /// </summary>
        public void Move()
        {
            // ステートがスイッチステージの場合
            if(InGameSceneController.Stages.StageState == StageConst.STATE_SWITCH_STAGE)
            {
                tmpSwitchTile.ResetFlag = true;

                // 全てのスイッチが踏まれたら
                if(checkSwitchTile())
                {
                    // 指定の座標のオブジェクトを取得してオブジェクトを差し変え
                    foreach(var tmpWall in InGameSceneController.Stages.WallTiles)
                    {
                        if(tmpWall.transform.position == StageConst.CHANGE_WALL_POS && tmpWall.activeSelf)
                        {
                            // 対象のオブジェクトの表示を消してタイル作成
                            tmpWall.SetActive(false);
                            // まだオブジェクトが生成されていない場合
                            if(InGameSceneController.Stages.ChangeTile.Count == 0)
                            {
                                // 生成してリストに格納
                                var tmpTileObj = MonoBehaviour.Instantiate(
                                    InGameSceneController.Stages.WhiteTile, 
                                    StageConst.CHANGE_WALL_POS,
                                    Quaternion.identity,
                                    InGameSceneController.Stages.TileParemt.transform);
                                InGameSceneController.Stages.ChangeTile.Add(tmpTileObj);
                            }
                            // 生成されていたら使いまわす
                            else
                            {
                                InGameSceneController.Stages.ChangeTile[0].SetActive(true);
                            }
                        }
                    }
                    InGameSceneController.Stages.ClearSwitchStage = true;
                }
            }
                
        }

        /// <summary>
        /// 全てのスイッチが踏まれたか確認用フラグ
        /// </summary>
        private bool checkSwitchTile()
        {
            foreach(var tmpObj in tmpSwitchTile.SwutchTiles)
            {
                // オブジェクトがnullの場合処理終了
                if(tmpObj == null)
                    break;
                else
                {
                    var tmpTile = tmpObj.GetComponent<BaseTile>();
                    // Switchが押されてないオブジェクトがあれば関数終了
                    if(!tmpTile.OnSwitch)
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Switchステージ初期化関数
        /// </summary>
        public void SwitchTileReset()
        {
            // リセットフラグ初期化
            tmpSwitchTile.ResetFlag = false;
            // タイル初期化
            foreach(var tmpObj in tmpSwitchTile.SwutchTiles)
            {
                var tmpTile = tmpObj.GetComponent<BaseTile>();
                if(tmpTile.OnSwitch)
                    tmpTile.OnSwitch = false;
                
                tmpObj.GetComponent<Renderer>().material.color = Color.green;
                tmpTile.StartColor = tmpObj.GetComponent<Renderer>().material.color;
            }  

            // 壁初期化
            foreach(var tmpWall in InGameSceneController.Stages.WallTiles)
            {
                // 表示されていたら飛ばす
                if(tmpWall.activeSelf)
                    continue;
                
                // されていなかったら表示し直し
                tmpWall.SetActive(true);
            }

            // ゴールにつながるタイルが表示されていたら削除
            if(InGameSceneController.Stages.ChangeTile.Count != 0 && InGameSceneController.Stages.ChangeTile[0].activeSelf)
                InGameSceneController.Stages.ChangeTile[0].SetActive(false);

            // フラグ初期化
            InGameSceneController.Stages.ClearSwitchStage = false;


        }
    }
}

