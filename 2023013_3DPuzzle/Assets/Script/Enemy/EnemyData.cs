using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("移動待機ターン数"), SerializeField]
    private int waitMoveCount;
    public int WaitMoveCount{get{return waitMoveCount;}}
}
