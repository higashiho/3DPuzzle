using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{

[CreateAssetMenu(menuName = "MyScriptable/Create EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("移動待機ターン数"), SerializeField]
    private int waitMoveCount;
    public int WaitMoveCount{get{return waitMoveCount;}}

    [Header("エネミー回転速度"), SerializeField]
    private float enemyMoveSpeed;
    public float EnemyMoveSpeed{get{return enemyMoveSpeed;}}

    [Header("エネミー移動インターバル"), SerializeField]
    private int moveInterval;
    public int MoveInterval{get{return moveInterval;}}
}
}
