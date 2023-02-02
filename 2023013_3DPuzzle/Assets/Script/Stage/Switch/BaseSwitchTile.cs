using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tile;

namespace Stage
{
    /// <summary>
    /// スイッチタイルのベースクラス
    /// </summary>
    public class BaseSwitchTile : MonoBehaviour
    {
        [SerializeField, Header("スイッチタイル")]
        protected GameObject[] switchTiles;
        public GameObject[] SwutchTiles{get{return switchTiles;} protected set{switchTiles = value;}}
        // 初期化フラグ
        protected bool resetFlag = false;
        public bool ResetFlag{get{return resetFlag;} set{resetFlag = value;}}

        // インスタンス化
        protected SwitchTileMove switchTileMove;
    }
}
