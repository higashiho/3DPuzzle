using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tile;

namespace Stage
{
    public class BaseNeedle : MonoBehaviour
    {
        [SerializeField,Header("ニードルタイルオブジェクト")]
        protected GameObject[] needleTiles;
        public GameObject[] NeedleTiles{get{return needleTiles;}set{needleTiles = value;}}
    }
}