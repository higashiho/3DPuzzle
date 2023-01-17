using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage
{
    public class StageController : BaseStage
    {
        // Start is called before the first frame update
        void Start()
        {
            // ステージ生成
            for (int i = 0; i < 4; i++)
                instance.StageMaking(this, i);
            tiles = GameObject.FindGameObjectsWithTag("Tiles");

            instanceBox.CreateBox(this);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}