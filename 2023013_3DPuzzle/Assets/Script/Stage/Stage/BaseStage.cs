using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using TMPro;

namespace Stage
{
    /// <summary>
    /// ステージのベースクラス
    /// </summary>
    public class BaseStage : MonoBehaviour
    {
        //CSVファイルのパス
        [SerializeField, Header("CSVファイルのアドレス")]
        protected AssetReference csvDataAssetRef; 
        public AssetReference CsvDataAssetRef{get{return csvDataAssetRef;} protected set{csvDataAssetRef = value;}}

        [SerializeField, Header("生成するステージのブロックのアドレス")]
        protected AssetReference[] stageBlockDataAssetRef = new AssetReference[13]; 
        public AssetReference[] StageBlockDataAssetRef{get{return stageBlockDataAssetRef;} protected set{stageBlockDataAssetRef = value;}}
        // ステージアドレスのハンドル
        protected AsyncOperationHandle handle;
        public AsyncOperationHandle Handle{get{return handle;} protected set{handle = value;}}
        // ステージのブロックの読み込みが終わったかフラグ
        public bool StageBlockLoadFlag{get; protected set;} = false;

        // ステージブロックのハンドル
        protected AsyncOperationHandle[] stageBlockHandle = new AsyncOperationHandle[13];
        public AsyncOperationHandle[] StageBlockHandle{get{return stageBlockHandle;} protected set{stageBlockHandle = value;}}

        
        [SerializeField, Header("タイルの親")]
        protected GameObject tileParent;
        public GameObject TileParemt{get{return tileParent;}}
        [SerializeField, Header("スイッチタイル")]
        protected GameObject[] keyTiles;
        public GameObject[] KeyTiles{get{return keyTiles;} protected set{keyTiles = value;}}
        [SerializeField, Header("パズルステージの壁")]
        protected AssetReference puzzleStageWallDataAssetRef; 
        protected AsyncOperationHandle puzzleStageWallHandle;
        [SerializeField, Header("白色タイル")]
        protected GameObject whiteTile;
        public GameObject WhiteTile{get{return whiteTile;}}
        
        // スイッチステージをクリアしたか
        protected bool clearSwitchStage;
        public bool ClearSwitchStage{get{return clearSwitchStage;} set{clearSwitchStage = value;}}
        
        // ボックスが移動中かフラグ
        private bool moving = false;
        public bool Moving{get{return moving;}set{moving = value;}}

        // どのステージをクリアしたかフラグ配列
        [SerializeField]
        protected bool[] stageClearFlags = new bool[4]{false, false, false, false};
        public bool[] StageClearFlags{get{return stageClearFlags;}set{Debug.Log("InClearFlag");stageClearFlags = value;}}
        
        [SerializeField,Header("生成するBox")]
        protected GameObject prefabBox;
        public GameObject PerfabBox{get{return prefabBox;}}

        [SerializeField, Header("ステージのステート")]
        protected uint stageState;
        public uint StageState{get{return stageState;}set{stageState = value;}}
        [SerializeField, Header("スイッチタイルのmaterial")]
        protected Material keyTileMaterial;
        public Material KeyTileMaterial{get{return keyTileMaterial;}}
        [SerializeField, Header("初期タイルのmaterial")]
        protected Material tileMaterial;
        public Material TileMaterial{get{return tileMaterial;}}

        // FallStageクリア回数
        [SerializeField]
        protected int clearCount = StageConst.MAX_GOAL_NUM;
        public int ClearCount{get{return clearCount;} set{clearCount = value;}}
        // FallStage用クリアタイルが変更されたかフラグ
        [SerializeField]
        protected bool tileChangeFlag = true;
        public bool TileChangeFlag{get{return tileChangeFlag;}set{tileChangeFlag = value;}}

        [SerializeField, Header("壁オブジェクト")]
        protected GameObject[] wallTiles;
        public GameObject[] WallTiles{get{return wallTiles;} protected set{wallTiles = value;}}
        [SerializeField, Header("差し替えタイル")]
        protected List<GameObject> changeTile = new List<GameObject>(1);
        public List<GameObject> ChangeTile{get{return changeTile;} set{changeTile = value;}}

        [SerializeField, Header("取得数字ポップアップテキスト")]
        protected TextMeshProUGUI getNumPopupText;
        public TextMeshProUGUI GetNumPopupText{get{return getNumPopupText;}}
        // Popupの初期座標
        public Vector3 PopupStartPos{get; protected set;}

        // インスタンス化
        protected InstanceStage instance;
        protected StageMove stageMove = new StageMove();
        public StageMove MoveStage{get{return stageMove;}}

    }

}
