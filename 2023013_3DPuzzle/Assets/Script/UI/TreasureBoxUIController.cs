using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class TreasureBoxUIController : BaseTreasureBoxUI
    {
        // Start is called before the first frame update
        void Start()
        {
            treasureBoxUIMove = new TreasureBoxUIMove(this);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}

