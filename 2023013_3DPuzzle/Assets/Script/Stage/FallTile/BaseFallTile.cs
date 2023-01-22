using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage
{
    /// <summary>
    /// 落下タイルのベースクラス
    /// </summary>
    public class BaseFallTile : MonoBehaviour
    {
        [SerializeField, Header("落下タイル取得用")]
        protected GameObject[] fallTiles;
        public GameObject[] FallTiles{get{return fallTiles;} protected set{fallTiles = value;}}
    }
}