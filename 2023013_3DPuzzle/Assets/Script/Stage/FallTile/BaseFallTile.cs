using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using Cysharp.Threading.Tasks;

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
        [SerializeField, Header("警告パネル")]
        protected Image warningPanel;
        public Image WarningPanel{get{return warningPanel;}}
        
    

        [SerializeField, Header("黒ひび割れタイル")]
        protected Material brokenBlackTileMaterial; 
        public Material BrokenBlackTileMaterial{get{return brokenBlackTileMaterial;}} 

        [SerializeField, Header("黒ひび割れタイル")]
        protected Material brokenWhiteTileMaterial; 
        public Material BrokenWhiteTileMaterial{get{return brokenWhiteTileMaterial;}} 
        // 実行しているタスク
        public UniTask? TimeCountTask = null;
        // Taskキャンセル処理用
        public static CancellationTokenSource Cts{get;private set;} = new CancellationTokenSource();

        // インスタンス化
        public FallTileMove FallTileMoves{get; protected set;}
    }
}