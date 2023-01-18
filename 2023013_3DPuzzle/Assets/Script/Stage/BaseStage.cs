using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Box;

namespace Stage
{
    public class BaseStage : MonoBehaviour
    {
        [SerializeField, Header("tileのデータ")]
        protected StageData stageData;
        public StageData StagesData{get{return stageData;}private set{stageData = value;}}
        [SerializeField, Header("タイルの親")]
        protected GameObject tileParent;
        public GameObject TileParemt{get{return tileParent;}private set{tileParent = value;}}
        
        //タイルのプレハブ
        [SerializeField, Header("タイルのプレファブ")]
        protected GameObject[] prefabTile = new GameObject[2];
        public GameObject[] PrefabTile{get{return prefabTile;}private set{prefabTile = value;}}
        
        //生成オブジェクト格納用
        [SerializeField, Header("生成したオブジェクト")]
        protected GameObject[] tiles;
        public GameObject[] Tiles{get {return tiles;}private set{tiles = value;}}
        
        // ボックスが移動中かフラグ
        private bool moving = false;
        public bool Moving{get{return moving;}set{moving = value;}}

        // 生成タイル判断用
        public enum InstanceState
        {
            START,
            NOMAL,
            GOAL
        }
        protected InstanceState instanceStatus = InstanceState.START;
        public InstanceState InstanceStatus{get{return instanceStatus;}set{instanceStatus = value;}}
        
        [SerializeField,Header("生成するBox")]
        protected GameObject prefabBox;
        public GameObject PerfabBox{get{return prefabBox;}private set{prefabBox = value;}}

        [SerializeField, Header("Boxの格納親オブジェクト")]
        protected GameObject boxs;
        public GameObject Boxs{get{return boxs;}private set{boxs = value;}}

        [SerializeField, Header("消えているTile")]
        protected GameObject[] goneTile = new GameObject[30];
        public GameObject[] GoneTile{get{return goneTile;}private set{goneTile = value;}}


        // インスタンス化
        protected InstanceStage instance = new InstanceStage();
        protected InstanceBox instanceBox = new InstanceBox();
    }

}
