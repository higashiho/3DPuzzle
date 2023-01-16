using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stage;

namespace Box
{
    public class ColBox : MonoBehaviour
    {
        [SerializeField]
        private BaseBox box;

        // 以下当たり判定
        private void OnCollisionEnter(Collision col)
        {
            // タイルに当たった時の処理
            if(col.gameObject.tag == "Tiles" || col.gameObject.tag == "SwitchTile")
            {
                // フリーズポジションがオンになっていない場合
                var tmpRb = this.GetComponent<Rigidbody>();
                if(tmpRb.constraints != RigidbodyConstraints.FreezePositionY)
                {   
                    tmpRb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
                }
                box.Tille = col.gameObject;
            }
            // プレイヤーに当たった時の処理
            if(col.gameObject.tag == "Player" && box.StartColor != Color.yellow && !col.gameObject.GetComponent<BasePlayer>().OnMove)
            {
                
                // 座標固定用変数代入
                box.PosY = this.transform.position.y;
                // 押し出し挙動の時一旦子になる
                this.transform.SetParent(col.transform);
            }
        }
    }
}