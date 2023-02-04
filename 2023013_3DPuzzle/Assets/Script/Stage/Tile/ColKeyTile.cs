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
                var tmpAngleX = Mathf.RoundToInt(col.transform.localEulerAngles.x);
                var tmpAngleZ = Mathf.RoundToInt(col.transform.localEulerAngles.z);
                if(tmpAngleX == 0 && tmpAngleZ == 0)
                {
                    tmpTile.MoveTile.KeyTileCollsionMove(col, this.gameObject);
                }
            }
        }
    }
}

