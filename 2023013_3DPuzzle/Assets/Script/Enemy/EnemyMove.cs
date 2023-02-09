using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using Stage;

namespace Enemy
{

    /// <summary>
    /// エネミーの移動処理クラス
    /// </summary>
    public class EnemyMove 
    {
        private BaseEnemy enemy;

        // コンストラクタ
        public EnemyMove(BaseEnemy tmpEnemy)
        {
            enemy = tmpEnemy;
        }

        /// <summary>
        /// 移動処理(プレイヤーの通ったタイルをたどっていく)
        /// </summary>
        /// <param name="cts">キャンセレーショントークンソース</param>
        public  void Move(CancellationTokenSource cts)
        {   
            // エネミーが回転中またはステージの移動中は動かない
            if(enemy.IsRotate || InGameSceneController.Stages.Moving)
            {
                // 変数リセット
                resetMoveValue();
                return;
            }

            // プレイヤーの通った座標を保管するQueueの要素数が0より大きかったら
            if(InGameSceneController.EnemyManager.PlayerTrace.Count > 0)
            {
                // 座標調整
                enemy.transform.position = Functions.CalcRoundingHalfUp(enemy.transform.position);
                // 目的地を決める
                Vector3 destination = InGameSceneController.EnemyManager.PlayerTrace.Dequeue();
                // 目的地の座標調整
                destination = Functions.CalcRoundingHalfUp(destination);
                Debug.Log("ElementNum in Queue = " + InGameSceneController.EnemyManager.PlayerTrace.Count);
                // 移動方向フラグを立てる
                enemy.MoveFlag = Functions.SetDirection(enemy.transform.position, destination);

                // 回転移動(平行)
                shiftMove(enemy.MoveFlag, Const.RotatePointArr, cts);
                // 座標調整
                enemy.transform.position = Functions.CalcRoundingHalfUp(enemy.transform.position);
                

                // リセット
                resetMoveValue();
            }
            else
            {
                // エネミーがプレイヤーに追いついちゃう処理
                Debug.Log("GameOver");
                // ゲームオーバー

            }
        }


        /// <summary>
        /// 移動に使った変数を初期化する関数
        /// </summary>
        private void resetMoveValue()
        {
            // 移動方向フラグ初期化
            enemy.MoveFlag = -1;
        }

        /// <summary>
        /// 回転処理
        /// </summary>
        private async UniTask rotateMove()
        {
            // 回転中のフラグを立てる
            enemy.IsRotate = true;
            // 回転角を設定
            if(enemy.CubeAngle == default)
                enemy.CubeAngle = enemy.EnemyDatas.EnemyMoveSpeed;
            
            float sumAngle = 0f;    // 回転角の合計値保存用
            while(sumAngle < 90f)
            {
                if(enemy.EnemyMoveCancel)
                    break;
                
                sumAngle += enemy.CubeAngle;

                // 90°以上回転しないように値を制限
                if(sumAngle > 90f)
                {
                    enemy.CubeAngle -= sumAngle - 90f;
                }

                // 回転と移動
                enemy.transform.RotateAround(enemy.RotatePoint , enemy.RotateAxis , enemy.CubeAngle);

                await UniTask.Yield(PlayerLoopTiming.Update);
            
            }
            // 回転中のフラグを倒す
            enemy.IsRotate = false;
        }

        /// <summary>
        /// 平行移動処理
        /// </summary>
        /// <param name="flag">移動フラグ</param>
        /// <param name="rotatePoint">回転中心</param>
        /// <param name="cts">キャンセレーショントークンソース</param>
        private async void shiftMove(int flag, Vector3[] rotatePoint, CancellationTokenSource cts)
        {
            // 移動方向フラグ向きに平行移動
            // 回転軸と回転中心を設定
            // 移動フラグを確認して回転軸と回転中心を設定
            enemy.RotateAxis = Functions.SetRotateAxis(flag) * Const.CUBE_SIZE_HALF;
            enemy.RotatePoint = enemy.transform.position + Functions.SetRotatePoint(rotatePoint, flag) * Const.CUBE_SIZE_HALF;
            
            
            if(!enemy.IsRotate)
            {
                await UniTask.WhenAny(rotateMove());
            }
        }

        /// <summary>
        /// プレイヤーがステージにいるかどうか
        /// </summary>
        /// <returns>ステージ内にいる : true</returns>
        public bool EnterStage()
        {
            if(InGameSceneController.Stages.StageState != Const.STATE_START)
                return true;
            
            return false;
        }

        
    }
}