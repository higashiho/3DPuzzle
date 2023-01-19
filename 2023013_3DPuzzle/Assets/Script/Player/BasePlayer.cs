using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stage;

public class BasePlayer : MonoBehaviour
{
    [SerializeField]
    protected PlayerData playerData;
    protected PlayerMove playerMove = new PlayerMove();
    public PlayerData PlayersData{get{return playerData;}private set{playerData = value;}}

    // 選択しているタイル
    protected GameObject chooseObj;
    public GameObject ChooseObj{get{return chooseObj;}set{chooseObj = value;}}
    
    
    [SerializeField, Header("針管理クラス")]
    protected BaseNeedle needle;
    public BaseNeedle Needle{get{return needle;} set{needle = value;}}

    // 移動中か
    protected bool onMove = false;
    public bool OnMove{get{return onMove;}set{onMove = value;}}
}
