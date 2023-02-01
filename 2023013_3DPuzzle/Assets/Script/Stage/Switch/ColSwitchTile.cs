using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tile
{
    public class ColSwitchTile : MonoBehaviour
    {
        // インスタンス化
        [SerializeField]
        private BaseTile tmpTile;

        // 当たり判定
        private void  OnCollisionExit(Collision col) 
        {
            if(col.gameObject.tag == "Player")
            {
                // スイッチがオンの場合オフにして色を初期化
                if(tmpTile.OnSwitch)
                {
                    tmpTile.OnSwitch = false;
                    tmpTile.GetComponent<Renderer>().material.color = tmpTile.StartColor;
                }
                // スイッチがオフの場合はオンにして色変更
                else
                {
                    tmpTile.OnSwitch = true;
                    tmpTile.GetComponent<Renderer>().material.color = Color.blue;
                }

                // スイッチがすべて押されたか判断
                tmpTile.SwitchTilesMove.Move();
            }

        }
        private void OnCollisionStay(Collision col)
        {
            tmpTile.MoveTile.KeyTileCollsionMove(col, this.gameObject);

        }
    }
}

