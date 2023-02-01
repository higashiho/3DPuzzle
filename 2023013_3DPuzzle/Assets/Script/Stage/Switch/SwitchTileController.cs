using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tile
{
    /// <summary>
    /// スイッチタイルの挙動管理クラス
    /// </summary>
    public class SwitchTileController : BaseSwitchTile
    {
        void Start() 
        {
            SwutchTiles = GameObject.FindGameObjectsWithTag("SwitchTile");
        }

        void Update()
        {

        }
    }
}

