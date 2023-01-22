using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tile;

namespace Stage
{
    /// <summary>
    /// 針ステージベースクラス
    /// </summary>
    public class BaseNeedle : MonoBehaviour
    {
        [SerializeField, Header("ニードルタイルオブジェクト")]
        protected GameObject[] needleTiles;
        public GameObject[] NeedleTiles{get{return needleTiles;}protected set{needleTiles = value;}}

        [SerializeField, Header("Player行動回数(仮)")]
        protected int playerMoveCount;
        public int PlyaerMoveCount{get{return playerMoveCount;}set{playerMoveCount = value;}}

        [SerializeField, Header("表示する針判断用")]
        protected int needleChangeCount;
        public int NeedleChangeCount{get{return needleChangeCount;}set{needleChangeCount = value;}}

        // インスタンス化
        protected NeedleMove needleMove = new NeedleMove();
    }
}