using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create StageData")]
public class StageData : ScriptableObject
{
    [SerializeField, Header("生成するｘ座標個数")]
    private int tileX;
    public int TileX{get{return tileX;}private set{tileX = value;}}
    [SerializeField, Header("生成するz座標個数")]
    private int tileZ;
    public int TileZ{get{return tileZ;}private set{tileZ = value;}}

    [SerializeField, Header("何段生成するか")]
    private int tileY;
    public int TileY{get{return tileY;}private set{tileY = value;}}

    [SerializeField, Header("生成するBoxオブジェクト数")]
    private int boxsNum;
    public int BoxsNum{get{return boxsNum;}private set{boxsNum = value;}}
}
