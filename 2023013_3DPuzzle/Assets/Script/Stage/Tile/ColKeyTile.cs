using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tile
{
    /// <summary>
    /// キータイルの当たり判定管理クラス
    /// </summary>
    public class ColKeyTile : MonoBehaviour
    {
        
        // インスタンス化
        [SerializeField]
        private BaseTile tmpTile;
        
        private void OnCollisionStay(Collision col)
        {
            if(col.gameObject.tag == "Player")
            {    
                tmpTile.MoveTile.KeyTileCollsionMove(col, this.gameObject);
            }
        }
    }
}

