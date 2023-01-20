using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tile
{
    /// <summary>
    /// 落下タイル当たり判定管理クラス
    /// </summary>
    public class ColFallTile : MonoBehaviour
    {
        [SerializeField]
        private BaseTile tile;

        private void  OnCollisionExit(Collision col) 
        {
            if(col.gameObject.tag == "Player")
            {
                // カウントを減らす
                tile.FallCount--;

                // カウントが０以下になったら落下削除
                if(tile.FallCount <= 0)
                    this.transform.parent.gameObject.SetActive(false);

                // カウントが残っている場合は描画変更
                else
                    this.GetComponent<Renderer>().material.color = Color.blue;

            }
        }
    }
}