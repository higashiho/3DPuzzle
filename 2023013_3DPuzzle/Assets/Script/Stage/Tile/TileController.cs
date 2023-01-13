using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : BaseTile
{
    // Start is called before the first frame update
    void Start()
    {
        startColor = this.GetComponent<Renderer>().material.color;
        player = GameObject.FindWithTag("Player").GetComponent<BasePlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
