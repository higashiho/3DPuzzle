using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Box
{
    /// <summary>
    /// ボックスのベースクラス
    /// </summary>
    public class BaseBox : MonoBehaviour
    {

        // 初期マテリアルカラー取得
        [SerializeField, Header("初期色")]
        protected Color startColor;
        public Color StartColor{get{return startColor;}set{startColor = value;}}

        [SerializeField, Header("プレイヤーに押された時の移動先タイル")]
        private GameObject tile;
        public GameObject Tile{get{return tile;}set{tile = value;}}

        [SerializeField, Header("親オブジェクト")]
        protected GameObject parent;
        public GameObject Parent{get{return parent;}set{parent = value;}}

        // y座標固定用
        protected float? posY = null;
        public float? PosY{get{return posY;}set{posY = value;}}

        // 消えているタイルを表示できるかフラグ
        protected bool tileActiveFlag = false;
        public bool TileActiveFlag{get{return tileActiveFlag;}set{tileActiveFlag = value;}}

        
        //マウスカーソルがBoxに乗った時の処理
        private void OnMouseOver()
        {
            boxMove.Move(this);
        }

        //マウスカーソルがBoxの上から離れた時の処理
        private void OnMouseExit()
        {
            //Sphereの色が元の色に戻す
            this.GetComponent<Renderer>().material.color = startColor;

        }


        // インスタンス化
        protected BoxMove boxMove = new BoxMove();
    }
}