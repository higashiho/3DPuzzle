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
            // プレイヤーとの当たり判定/スイッチステージをクリアしてない時のみ実施
            if(col.gameObject.tag == "Player" && !InGameSceneController.Stages.ClearSwitchStage)
            {
                // スイッチがオンの場合オフにして色を初期化
                if(tmpTile.OnSwitch)
                {
                    tmpTile.OnSwitch = false;
                    tmpTile.GetComponent<Renderer>().material.color = Color.green;
                    tmpTile.StartColor = tmpTile.GetComponent<Renderer>().material.color;
                }
                // スイッチがオフの場合はオンにして色変更
                else
                {
                    tmpTile.OnSwitch = true;
                    tmpTile.GetComponent<Renderer>().material.color = Color.blue;
                    tmpTile.StartColor = tmpTile.GetComponent<Renderer>().material.color;

                }

                // スイッチがすべて押されたか判断
                tmpTile.SwitchTilesMove.Move();
            }

        }
        void OnCollisionStay(Collision col)
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

