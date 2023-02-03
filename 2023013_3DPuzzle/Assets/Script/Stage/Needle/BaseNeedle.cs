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

        [SerializeField, Header("表示する針判断用")]
        protected int needleChangeCount;
        public int NeedleChangeCount{get{return needleChangeCount;}set{needleChangeCount = value;}}
        
        // 針変換フラグ(変換中か)
        protected bool onNeedleTrans = false;
        public bool OnNeedleTrans{get{return onNeedleTrans;}set{onNeedleTrans = value;}}

        // インスタンス化
        protected NeedleMove needleMove = new NeedleMove();
    }
}