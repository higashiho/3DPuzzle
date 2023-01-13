using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : BaseStage
{
    private InstanceStage instance = new InstanceStage();
    // Start is called before the first frame update
    void Start()
    {
        // 内部データ初期化
        tiles = new GameObject[stageData.TileX * (int)PrefabTile[0].transform.localScale.x, stageData.TileY * (int)PrefabTile[0].transform.localScale.y];
        instance.StageMaking(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
