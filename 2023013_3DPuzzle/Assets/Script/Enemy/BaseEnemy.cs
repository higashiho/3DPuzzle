using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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




        
        // タスクキャンセル用変数***********
        
        protected CancellationTokenSource cts = new CancellationTokenSource();
        [Header("エネミー移動キャンセル"), SerializeField]
        protected bool enemyMoveCancel = false;
        public bool EnemyMoveCancel{get{return enemyMoveCancel;}set{enemyMoveCancel = value;}}


        public void OnDestroy()
        {
            this.transform.position = Vector3.zero;
            EnemyMoveCancel = true;
            cts.Cancel();
        }

        void OnEnable()
        {
            this.transform.position = (Vector3)InGameSceneController.EnemyManager.EnemyStartPos;
            EnemyState = EnemyPhase.Move;
        }
        
        protected EnemyMove enemyMove;
    }
}
