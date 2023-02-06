using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Move()
    {
        if(waitMove())
        {

        }
    }

    /// <summary>
    /// エネミーの行動待機時間だけ待ってフラグを立てる
    /// </summary>
    /// <returns>行動待機時間待った : true</returns>
    private bool waitMove()
    {
        int turn;
        // turn = (エネミーの移動待機ターン) - (プレイヤーの移動カウンター)
        turn = enemy.EnemyDatas.WaitMoveCount - InGameSceneController.Player.MoveCount;
        if(turn <= 0)
            return true;
        
        return false;
    }

    
}
