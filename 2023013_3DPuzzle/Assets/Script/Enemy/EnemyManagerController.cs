using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Enemy
{
    // プレイヤーがステージにいるか判定
    // ステージに入ったらエネミー待機状態にする
    // 待機状態が完了したらエネミーActive
    // プレイヤーがステージからいなくなったらエネミー非Active => エネミーコントローラーに記述

    /// <summary>
    /// エネミー生成・待機管理クラス
    /// </summary>
    public class EnemyManagerController : MonoBehaviour
    {
        private BasePlayer player;


        [Header("エネミーデータ"), SerializeField]
        private EnemyData enemyDatas;

        [Header("ステージ内のMoveCount"), SerializeField]
        private int? moveCountInStage;
        

        /// <summary>
        /// エネミーの生成座標
        /// </summary>
        public Vector3? EnemyStartPos = null;
 
        /// <summary>
        /// プレイヤーの通った座標を保管するQueue
        /// </summary>
        public Queue<Vector3> PlayerTrace = new Queue<Vector3>(16);

        /// <summary>
        /// エネミー生成フェーズ
        /// </summary>
        public enum EnemyState
        {
            Init, Wait, Reset
        }
        public EnemyState EnemyPhase;

        void Start()
        {
            EnemyPhase = EnemyState.Init;
            player = InGameSceneController.Player;
        }

        void Update()
        {
            switch(EnemyPhase)
            {
                case EnemyState.Init:

                    moveCountInStage = null;
                    EnemyStartPos = null;
                    // プレイヤーがステージ内にいる場合
                    if(PlayerInStage())
                    {
                        
                        // プレイヤーがステージに入った瞬間のMoveCount取得
                        moveCountInStage = InGameSceneController.Player.MoveCount;
                        
                        if(moveCountInStage != null)
                        {
                            // エネミー待機状態
                            EnemyPhase = EnemyState.Wait;
                            
                        }
                    }

                    break;
                case EnemyState.Wait:

                    if(player.MoveCount - moveCountInStage > enemyDatas.WaitMoveCount)
                    {
                        // エネミー生成座標設定
                        if(EnemyStartPos == null)
                            EnemyStartPos = PlayerTrace.Dequeue();
                        
                        // エネミーアクティブ化
                        this.transform.GetChild(0).gameObject.SetActive(true);
                        // エネミー待機状態
                        EnemyPhase = EnemyState.Reset;
                    }
                    if(!PlayerInStage())
                    {
                        // エネミー待機状態
                        EnemyPhase = EnemyState.Reset;
                    }
                    

                    break;

                case EnemyState.Reset:
                    // エネミーが非アクティブになっていたら
                    if(!this.transform.GetChild(0).gameObject.activeSelf)
                    {
                        EnemyPhase = EnemyState.Init;
                        // プレイヤーの通った座標を保管するQueue初期化
                        PlayerTrace.Clear();
                    }
                    break;
            }
            
        }
        
        /// <summary>
        /// プレイヤーがステージ内にいるか判定する関数
        /// </summary>
        /// <returns>プレイヤーがステージ内にいる場合 : true</returns>
        public bool PlayerInStage()
        {
            // スタートタイルまたはMoveStageにいるとき
            if(InGameSceneController.Stages.StageState == Const.STATE_START
             || InGameSceneController.Stages.StageState == StageConst.STATE_MOVE_STAGE)
                return false;

            // それ以外のステージ内にいる場合
            return true;
        }

        

        
    }
}

