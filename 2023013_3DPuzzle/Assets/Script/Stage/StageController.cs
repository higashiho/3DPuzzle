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
            instance.StageMaking(this);
            instanceBox.CreateBox(this);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}