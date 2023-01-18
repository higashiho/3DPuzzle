using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Box
{
    public class BaseStairs : MonoBehaviour
    {
        // 初期マテリアルカラー取得
        [SerializeField, Header("初期色")]
        protected Color startColor;
        public Color StartColor{get{return startColor;}set{startColor = value;}}


        //マウスカーソルが階段に乗った時の処理
        private void OnMouseOver()
        {
            scaleMode.Move(this);
        }

        //マウスカーソルが階段の上から離れた時の処理
        private void OnMouseExit()
        {
            //Sphereの色が元の色に戻す
            this.GetComponent<Renderer>().material.color = startColor;

        }


        // インスタンス化
        protected StairsMove scaleMode = new StairsMove();
    }
}