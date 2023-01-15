using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage
{
    public class TileController : BaseTile
    {
        // Start is called before the first frame update
        void Start()
        {
            startColor = this.GetComponent<Renderer>().material.color;

            
            if(this.gameObject.tag == "SwitchTile")
            {
                startColor = Color.cyan;
                this.GetComponent<Renderer>().material.color = startColor;
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}