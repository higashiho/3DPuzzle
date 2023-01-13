using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTile : MonoBehaviour
{
    // 初期マテリアルカラー取得
    [SerializeField, Header("初期色")]
    protected Color startColor;
    // プレイヤーオブジェクト参照
    [SerializeField]
    protected BasePlayer player;

    // 座標取得
    public Vector3 TilePos{get{return this.transform.position;}private set{this.transform.position = value;}}

    
    //マウスカーソルがSphereに乗った時の処理
    private void OnMouseOver()
    {
        if(!player.OnMove)
        {
            //Sphereの色を赤色に変化
            this.GetComponent<Renderer>().material.color = Color.red;
            player.ChooseObj = this.gameObject;
        }
        
    }

    //マウスカーソルがSphereの上から離れた時の処理
    private void OnMouseExit()
    {
        //Sphereの色が元の色に戻す
        this.GetComponent<Renderer>().material.color = startColor;

    }
}
