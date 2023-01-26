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
        public GameObject[] Stages{get{return stages;}private set{stages = value;}}
        
        [SerializeField, Header("タイルの親")]
        protected GameObject tileParent;
        public GameObject TileParemt{get{return tileParent;}private set{tileParent = value;}}
        
        // ボックスが移動中かフラグ
        private bool moving = false;
        public bool Moving{get{return moving;}set{moving = value;}}

        
        [SerializeField,Header("生成するBox")]
        protected GameObject prefabBox;
        public GameObject PerfabBox{get{return prefabBox;}private set{prefabBox = value;}}

        [SerializeField, Header("ステージのステート")]
        protected uint stageState;
        public uint StageState{get{return stageState;}set{stageState = value;}}

        [SerializeField, Header("落下ステージのコントローラークラス")]
        protected BaseFallTile fallTiles;
        public BaseFallTile FallTiles{get{return fallTiles;}}


        // インスタンス化
        protected InstanceStage instance = new InstanceStage();
        protected StageMove stageMove = new StageMove();
        public StageMove MoveStage{get{return stageMove;}}
    }

}
