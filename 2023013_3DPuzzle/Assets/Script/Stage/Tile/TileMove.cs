using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tile
{
    /// <summary>
    /// タイル挙動関数管理クラス
    /// </summary>
    public class TileMove
    {
        private BaseTile tmpTile;
        public TileMove(BaseTile tmp)
        {
            tmpTile = tmp;
        }

        /// <summary>
        /// クリア回数が１回じゃないステージ用関数
        /// </summary>
        public void ChangeSwitchTile()
        {
            InGameSceneController.Stages.ClearCount--;

            GameObject tmpObj = null;
            float nearDis = default;
            
            // プレイヤーから遠いオブジェクトを探索
            foreach(var tmp in InGameSceneController.FallTile.FallTiles)
            {
                if(!tmp)
                    break;
                
                if(tmp.activeSelf)
                {
                    if(!tmpObj)
                        tmpObj = tmp;
                    else
                    {
                        var tmpDis = Vector3.Distance(tmp.transform.position, InGameSceneController.Player.transform.position);
                        if(tmpDis > nearDis)
                        {
                            nearDis = tmpDis;
                            tmpObj = tmp;
                        }
                    }
                }
            }

            // オブジェクト変更
            tmpTile.gameObject.tag = "Tiles";
            var tmpMaterialRenderer = tmpTile.gameObject.GetComponent<Renderer>().material;
            tmpMaterialRenderer = InGameSceneController.Stages.TileMaterial;
            tmpMaterialRenderer.color = Color.white;
            tmpTile.gameObject.GetComponent<Renderer>().material = tmpMaterialRenderer;
            tmpObj.tag = "SwitchTile";
            tmpMaterialRenderer = tmpObj.GetComponent<Renderer>().material;
            tmpMaterialRenderer = InGameSceneController.Stages.SwitchTileMaterial;
            tmpMaterialRenderer.color = Color.magenta;
            tmpObj.GetComponent<Renderer>().material = tmpMaterialRenderer;
            InGameSceneController.Stages.TileChangeFlag = false;

        }
    }
}