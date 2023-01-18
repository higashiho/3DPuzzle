using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStairs : MonoBehaviour
{
    // 初期マテリアルカラー取得
    [SerializeField, Header("初期色")]
    protected Color startColor;
    public Color StartColor{get{return startColor;}set{startColor = value;}}

}
