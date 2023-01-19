using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage
{
    public class NeedleController : BaseNeedle
    {
        // Start is called before the first frame update
        void Start()
        {
            // ニードル取得
            NeedleTiles = GameObject.FindGameObjectsWithTag("Needle");
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}