using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tile
{
    /// <summary>
    /// スイッチタイルのベースクラス
    /// </summary>
    public class BaseSwitchTile : MonoBehaviour
    {
        [SerializeField, Header("スイッチタイル")]
        protected GameObject[] switchTiles;
        public GameObject[] SwutchTiles{get{return switchTiles;} protected set{switchTiles = value;}}
    }
}
