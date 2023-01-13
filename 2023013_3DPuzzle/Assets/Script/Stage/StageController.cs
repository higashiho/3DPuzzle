using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : BaseStage
{
    // Start is called before the first frame update
    void Awake()
    {
        // 内部データ初期化
        tiles = new GameObject[StagesData.TileX * StagesData.TileY];
        instance.StageMaking(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
