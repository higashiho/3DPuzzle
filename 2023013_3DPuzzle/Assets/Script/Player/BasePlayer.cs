using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    [SerializeField]
    protected PlayerData playerData;
    protected PlayerMove playerMove = new PlayerMove();
    public PlayerData PlayersData{get{return playerData;}private set{playerData = value;}}

    // 選択しているタイル
    protected GameObject chooseObj;
    public GameObject ChooseObj{get{return chooseObj;}set{chooseObj = value;}}
    

    // 移動中か
    protected bool onMove = false;
    public bool OnMove{get{return onMove;}set{onMove = value;}}
}
