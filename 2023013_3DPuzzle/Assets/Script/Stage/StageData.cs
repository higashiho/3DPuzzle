using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create StageData")]
public class StageData : ScriptableObject
{
    [SerializeField, Header("生成するｘ座標個数")]
    private int tileX;
    public int TileX{get{return tileX;}private set{tileX = value;}}
    [SerializeField, Header("生成するy座標個数")]
    private int tileY;
    public int TileY{get{return tileY;}private set{tileY = value;}}

    

}
