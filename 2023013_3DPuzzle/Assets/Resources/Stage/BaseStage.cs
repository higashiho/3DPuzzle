using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Box;

namespace Stage
{
    /// <summary>
    /// ステージのベースクラス
    /// </summary>
    public class BaseStage : MonoBehaviour
    {
        //CSVファイルのパス
        protected string filePath = "Stage/StagesData";     

        [SerializeField,Header("生成するステージ")]
        protected GameObject[] stages = new GameObject[6];
        public GameObject[] Stages{get{return stages;}}
        
        [SerializeField, Header("タイルの親")]
        protected GameObject tileParent;
        public GameObject TileParemt{get{return tileParent;}}
        [SerializeField, Header("スイッチタイル")]
        protected GameObject[] switchTiles;
        public GameObject[] SwitchTiles{get{return switchTiles;} protected set{switchTiles = value;}}
        
        // ボックスが移動中かフラグ
        private bool moving = false;
        public bool Moving{get{return moving;}set{moving = value;}}

        
        [SerializeField,Header("生成するBox")]
        protected GameObject prefabBox;
        public GameObject PerfabBox{get{return prefabBox;}}

        [SerializeField, Header("ステージのステート")]
        protected uint stageState;
        public uint StageState{get{return stageState;}set{stageState = value;}}

        [SerializeField, Header("落下ステージのコントローラークラス")]
        protected BaseFallTile fallTiles;
        public BaseFallTile FallTiles{get{return fallTiles;}}

        // FallStageクリア回数
        [SerializeField]
        protected int clearCount = Const.MAX_GOAL_NUM;
        public int ClearCount{get{return clearCount;} set{clearCount = value;}}
        // FallStage用クリアタイルが変更されたかフラグ
        [SerializeField]
        protected bool tileChangeFlag = true;
        public bool TileChangeFlag{get{return tileChangeFlag;}set{tileChangeFlag = value;}}
        // インスタンス化
        protected InstanceStage instance = new InstanceStage();
        protected StageMove stageMove = new StageMove();
        public StageMove MoveStage{get{return stageMove;}}
    }

}
