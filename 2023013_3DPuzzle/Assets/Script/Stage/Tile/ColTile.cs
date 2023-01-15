using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Box;

namespace Tile
{
    public class ColTile : MonoBehaviour
    {
        // 当たり判定
        private void OnCollisionEnter(Collision col)
        {
            // Boxと当たった時に自分がスイッチの場合
            if(col.gameObject.tag == "Box" && this.gameObject.tag == "SwitchTile")
            {
                Debug.Log("Enter");
                col.gameObject.GetComponent<BaseBox>().TileActiveFlag = true;
            }
        }
        private void OnCollisionExit(Collision col)
        {
            
            // Boxと離れた時に自分がスイッチの場合
            if(col.gameObject.tag == "Box" && this.gameObject.tag == "SwitchTile")
            {
                Debug.Log("Exit");
                col.gameObject.GetComponent<BaseBox>().TileActiveFlag = false;

                // 消えていたオブジェクトを消す
                foreach(GameObject tmpObj in InGameSceneController.Stages.GoneTile)
                {
                    tmpObj.SetActive(false);
                }
            }
        }
    }
}