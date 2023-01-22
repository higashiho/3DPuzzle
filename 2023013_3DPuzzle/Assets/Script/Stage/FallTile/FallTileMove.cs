using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stage;

namespace Tile
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
                    tmpObj.GetComponent<Renderer>().material.color = tmpTile.StartColor;
                    tmpTile.FallCount = Const.FALL_COUNT_MAX;
                }

                if(!tmpObj.transform.parent.gameObject.activeSelf)
                    tmpObj.transform.parent.gameObject.SetActive(true);
            }
        }
    }
}