using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create PlayerData")]
public class PlayerData : ScriptableObject
{
    
    // 挙動スピード
    [SerializeField, Header("移動所要時間")]
    private float playerMoveTime;
    public float PlayerMoveTime{get{return playerMoveTime;}private set{playerMoveTime = value;}}
}
