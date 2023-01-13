using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create PlayerData")]
public class PlayerData : ScriptableObject
{
    // 挙動スピード
    [SerializeField, Header("移動スピード")]
    private float playerSpeed;
    public float PlayerSpeed{get{return playerSpeed;}private set{playerSpeed = value;}}
}
