using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tile
{
    /// <summary>
    /// タイルのベースクラス
    /// </summary>
    public class BaseTile : MonoBehaviour
    {
        // 初期マテリアルカラー取得
        [SerializeField, Header("初期色")]
        protected Color startColor;

        // プレイヤーが動けるかどうか
        protected bool onMove = true;
        public bool OnMove{get{return onMove;}set{onMove = value;}}

        // Boxが上に乗っているか
        protected bool onBlock = false;
        public bool OnBlock{get{return onBlock;}set{onBlock = value;}}
        
        // マウスカーソルがSphereに乗った時の処理
        private void OnMouseOver()
        {
            if(!InGameSceneController.Player.OnMove && this.gameObject.tag != "SwitchTile" && OnMove)
            {   
                // プレイヤーが２マス以内にいる && BOXが上に乗っていないとき
                if(checkTheDistanceFromPlayer() && !onBlock)
                {
                    //Sphereの色を赤色に変化
                    this.GetComponent<Renderer>().material.color = Color.red;
                    InGameSceneController.Player.ChooseObj = this.gameObject;
                }
                
            }
            
        }

        //マウスカーソルがSphereの上から離れた時の処理
        private void OnMouseExit()
        {
            //Sphereの色が元の色に戻す
            this.GetComponent<Renderer>().material.color = startColor;
            InGameSceneController.Player.ChooseObj = null;

        }

        /// <summary>
        /// プレイヤーとタイルの距離がタイル２枚分の距離以内か確認する関数
        /// </summary>
        /// <returns>距離が２枚分以内:true, 以上:false</returns>
        private bool checkTheDistanceFromPlayer()
        {
            var playerPos = InGameSceneController.Player.transform.position;    // プレイヤーの座標取得
            var tilePos = this.transform.position;                              // 自身の座標取得

            var diffX = Mathf.RoundToInt(Mathf.Abs(playerPos.x - tilePos.x));     // playerとtileのx座標の差を求める(int)
            var diffZ = Mathf.RoundToInt(Mathf.Abs(playerPos.z - tilePos.z));     // playerとtileのz座標の差を求める(int)

            if(diffX <= (Const.CUBE_SIZE_HALF * 4) && diffZ < 1)   // diffXが2マス分以内の距離だったら
                return true;
            if(diffX < 1 && diffZ <= (Const.CUBE_SIZE_HALF * 4))   // diffYが2マス分以内の距離だったら
                return true;

            return false;

        }
    }
    
}