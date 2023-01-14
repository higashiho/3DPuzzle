using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Box;

namespace Stage
{
    public class BaseStage : MonoBehaviour
    {
        [SerializeField]
        protected StageData stageData;
        public StageData StagesData{get{return stageData;}private set{stageData = value;}}

        
        //タイルのプレハブ
        [SerializeField]
        protected GameObject[] prefabTile = new GameObject[2];
        public GameObject[] PrefabTile{get{return prefabTile;}private set{prefabTile = value;}}
        
        //生成オブジェクト格納用
        [SerializeField]
        protected GameObject[] tiles = new GameObject[20];
        public GameObject[] Tiles{get {return tiles;}private set{tiles = value;}}

        
        [SerializeField,Header("生成するBox")]
        protected GameObject prefabBox;
        public GameObject PerfabBox{get{return prefabBox;}private set{prefabBox = value;}}
        [SerializeField, Header("Boxの格納親オブジェクト")]
        protected GameObject boxs;
        public GameObject Boxs{get{return boxs;}private set{boxs = value;}}

        // インスタンス化
        protected InstanceStage instance = new InstanceStage();
        protected InstanceBox instanceBox = new InstanceBox();
    }

}
