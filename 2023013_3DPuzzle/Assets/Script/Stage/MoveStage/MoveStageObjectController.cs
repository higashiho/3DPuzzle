using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tile
{
    public class MoveStageObjectController : BaseMoveStageObject
    {
        // Start is called before the first frame update
        void Start()
        {
            StartAngle = this.transform.localEulerAngles;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}

