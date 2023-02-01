using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tile
{
    /// <summary>
    /// ニードルの当たり判定管理クラス
    /// </summary>
    public class ColNeedleTile : MonoBehaviour
    {
        
        // インスタンス化
        [SerializeField]
        private BaseTile tmpTile;
        
        private void OnCollisionStay(Collision col)
        {
            tmpTile.MoveTile.KeyTileCollsionMove(col, this.gameObject);
        }
    }
}
