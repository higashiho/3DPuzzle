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
        public Color StartColor{get{return startColor;}}
        protected Material startMaterial;
        public Material StartMaterial{get{return startMaterial;}}

        // Fallタイル用
        protected float fallCount = 2;
        public float FallCount{get{return fallCount;}set{fallCount = value;}}

        // 自身がキーかどうかフラグ
        [SerializeField, Header("キータイルかどうか")]
        protected bool keyTileFlag = false;

        [SerializeField, Header("自身がスイッチタイルの場合スイッチが押されたかどうかフラグ")]
        protected bool onSwitch = false;
        public bool OnSwitch{get{return onSwitch;} set{onSwitch = value;}}

        // インスタンス化
        public TileMove MoveTile{get; protected set;}
        public SwitchTileMove SwitchTilesMove{get; protected set;}
        
        // マウスカーソルがSphereに乗った時の処理
        private void OnMouseOver()
        {
            if(!InGameSceneController.Player.OnMove)
            {   
                // プレイヤーが２マス以内にいる && BOXが上に乗っていないとき
                if(checkTheDistanceFromPlayer())
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
            // スイッチタイルかどうか
            if(keyTileFlag)
            {
                // スイッチタイルから変更されていないときは初期色を入れる
                if(this.tag == "KeyTile")
                    this.GetComponent<Renderer>().material.color = StartColor;
                else
                    this.GetComponent<Renderer>().material.color = Color.white;

                InGameSceneController.Player.ChooseObj = null;
                return;
            }
            this.GetComponent<Renderer>().material.color = StartColor;
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