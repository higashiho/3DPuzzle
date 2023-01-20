using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

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
    // Taskキャンセル処理用
    public CancellationTokenSource cts{get;private set;} = new CancellationTokenSource();
    public CancellationToken ct{get;private set;} = new CancellationToken();

    public void OnDestroy()
    {
        cts.Cancel();
    }
}
