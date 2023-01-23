using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using Stage;
using DG.Tweening;

public class BasePlayer : MonoBehaviour
{
    [SerializeField]
    protected PlayerData playerData;
    public PlayerData PlayersData{get{return playerData;}private set{playerData = value;}}

    // 選択しているタイル
    protected GameObject chooseObj;
    public GameObject ChooseObj{get{return chooseObj;}set{chooseObj = value;}}

    // 初期座標
    protected Vector3 startPos;
    public Vector3 StartPos{get{return startPos;}set{startPos = value;}}
    
    
    [SerializeField, Header("針管理クラス")]
    protected BaseNeedle needle;
    public BaseNeedle Needle{get{return needle;} set{needle = value;}}

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

    
    // 失敗時のTween
    protected Tween playerFailureTween;
    public Tween PlayerFailureTween{get{return playerFailureTween;}set{playerFailureTween = value;}}
    // 成功時のTween
    protected Tween playerClearTween;
    public Tween PlayerClearTween{get{return playerClearTween;}set{playerClearTween = value;}}

    // インスタンス化
    protected PlayerMove playerMove = new PlayerMove();

}
