using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [Header("エネミーデータ"), SerializeField]
    private EnemyData enemyData;
    public EnemyData EnemyDatas{get{return enemyData;}}

    /// <summary>
    /// プレイヤーの通った座標を保管するQueue
    /// </summary>
    public Queue<Vector3> playerTrace = new Queue<Vector3>(16);
    

    protected EnemyMove enemyMove;
}
