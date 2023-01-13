using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    [SerializeField]
    protected PlayerData playerData;
    public PlayerData PlayersData{get{return playerData;}private set{playerData = value;}}

    // 選択しているタイル
    public GameObject ChooseObj;

    // 移動中か
    public bool OnMove;
}
