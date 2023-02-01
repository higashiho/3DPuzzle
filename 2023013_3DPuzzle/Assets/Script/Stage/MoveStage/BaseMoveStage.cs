using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Stage
{
    /// <summary>
    /// 動かせるステージのベースクラス
    /// </summary>
    public class BaseMoveStage : MonoBehaviour
    {
        // 挙動出来るかフラグ
        [SerializeField]
        protected bool moveFlag = false;
        public bool MoveFlag{get{return moveFlag;}set{moveFlag = value;}}
        // リセットフラグ
        protected bool resetFlag = false;
        public bool ResetFlag{get{return resetFlag;}set{resetFlag = value;}}
        // 挙動前の座標
        protected Vector3 lastAngle;
        public Vector3 LastAngle{get{return lastAngle;}set{lastAngle = value;}}

        // 挙動中のTween
        protected Tween nowTween = null;
        public Tween NowTween{get{return nowTween;}set{nowTween = value;}}
        [SerializeField,Header("動かせるステージ達")]
        protected GameObject[] moveStageTiles;
        public GameObject[] MoveStageTiles{get{return moveStageTiles;}protected set{moveStageTiles = value;}}

        [SerializeField, Header("動かすステージ")]
        protected GameObject moveStageObj;
        public GameObject MoveStageObj{get{return moveStageObj;}set{moveStageObj = value;}}

        [SerializeField, Header("動かせるようにするためのスイッチ")]
        protected GameObject[] onMoveSwitchs;
        public GameObject[] OnMoveSwitchs{get{return onMoveSwitchs;} protected set{onMoveSwitchs = value;}}
        // Switch表示変更のフラグ
        [SerializeField]
        protected bool changeSwitchFlag = true;
        public bool ChangeSwitchFlag{get{return changeSwitchFlag;}set{changeSwitchFlag = value;}}




        // インスタンス化
        protected MoveStageMove moveStage;
    }
}