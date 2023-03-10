using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Enemy
{
    public class BaseEnemy : MonoBehaviour
    {
        [Header("エネミーデータ"), SerializeField]
        private EnemyData enemyData;
        public EnemyData EnemyDatas{get{return enemyData;}}
        
        [Header("移動中フラグ"), SerializeField]
        protected bool isRotate = false;
        public bool IsRotate{get{return isRotate;}set{isRotate = value;}}

        [Header("エネミー移動禁止フラグ"), SerializeField]
        private bool isStop;
        public bool IsStop{get{return isStop;}set{isStop = value;}}

        [Header("エネミーデフォルト角度"), SerializeField]
        private Vector3 defaultAngle;
        public Vector3 DefaultAngle{get{return defaultAngle;}set{defaultAngle = value;}}

        /// <summary>
        /// エネミーの状態管理用列挙体
        /// </summary>
        public enum EnemyPhase
        {
            Move,
            Reset
        }

        /// <summary>
        /// エネミー状態管理変数
        /// </summary>
        public EnemyPhase EnemyState;
        
        /// <summary>
        /// 回転中心の座標(ワールド座標)
        /// </summary>
        private Vector3 rotatePoint;
        public Vector3 RotatePoint{get{return rotatePoint;} set{rotatePoint = value;}}

        /// <summary>
        /// 回転軸(ワールド座標)
        /// </summary>
        private Vector3 rotateAxis;
        public Vector3 RotateAxis{get{return rotateAxis;}set{rotateAxis = value;}}

        /// <summary>
        /// エネミーの回転角
        /// </summary>
        private float cubeAngle = 0;
        public float CubeAngle{get{return cubeAngle;}set{cubeAngle = value;}}
        
        /// <summary>
        /// エネミー移動フラグ
        /// </summary>
        private int moveFlag = -1;
        public int MoveFlag{get{return moveFlag;}set{moveFlag = value;}}

        /// <summary>
        /// エネミー移動待機タスク
        /// </summary>
        private UniTask? waitMove;
        public UniTask? WaitMove{get{return waitMove;}set{waitMove = value;}}

        // タスクキャンセレーショントークン
        protected CancellationTokenSource cts = new CancellationTokenSource();

        [Header("エネミー移動キャンセル"), SerializeField]
        protected bool enemyMoveCancel = false;
        public bool EnemyMoveCancel{get{return enemyMoveCancel;}set{enemyMoveCancel = value;}}

        /// <summary>
        /// プレイヤーリセットフラグ
        /// </summary>
        public bool B_ResetPlayer;


        public void OnDestroy()
        {
            EnemyMoveCancel = true;
            cts.Cancel();
        }

        void OnEnable()
        {
            // 生成座標設定
            this.transform.position = (Vector3)InGameSceneController.EnemyManager.EnemyStartPos;
            // エネミーステートmoveに更新
            EnemyState = EnemyPhase.Move;

            IsStop = false;
            this.transform.position = (Vector3)Functions.CalcRoundingHalfUp(this.transform.position);
            this.transform.rotation = Quaternion.identity;
            WaitMove = null;
        }
        
        public EnemyMove EnemyMove;
    }
}
