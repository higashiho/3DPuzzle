using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage
{
    /// <summary>
    /// 落下タイルの挙動管理クラス
    /// </summary>
    public class FallTileController : BaseFallTile
    {
        // Start is called before the first frame update
        void Start()
        {
            FallTiles = GameObject.FindGameObjectsWithTag("Fall");
            FallTileMoves = new FallTileMove(this);
        }

        // Update is called once per frame
        void Update()
        {
            FallTileMoves.TimeMoveAsync();
        }
    }
}