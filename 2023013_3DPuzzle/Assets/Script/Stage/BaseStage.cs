using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStage : MonoBehaviour
{
    [SerializeField]
    protected StageData stageData;
    public StageData StagesData{get{return stageData;}private set{stageData = value;}}

    
    //タイルのプレハブ
    [SerializeField]
    protected GameObject[] prefabTile = new GameObject[2];
    public GameObject[] PrefabTile{get{return prefabTile;}private set{prefabTile = value;}}
    
    //内部データ
    [SerializeField]
    protected GameObject[,] tiles;
    public GameObject[,] Tiles{get {return tiles;}private set{tiles = value;}}

    // インスタンス化
    protected InstanceStage instance = new InstanceStage();
}
