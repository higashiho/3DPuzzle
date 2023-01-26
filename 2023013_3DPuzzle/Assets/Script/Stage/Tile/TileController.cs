using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tile
{
    /// <summary>
    /// タイルの挙動管理クラス
    /// </summary>
    public class TileController : BaseTile
    {
        // Start is called before the first frame update
        void Start()
        {  
            startMaterial = this.GetComponent<Renderer>().material;
            startColor = this.GetComponent<Renderer>().material.color;

        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}