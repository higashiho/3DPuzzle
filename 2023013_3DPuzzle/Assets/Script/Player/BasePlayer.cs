using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using Stage;
using DG.Tweening;

public class BasePlayer : MonoBehaviour
{
    [Header("プレイヤーのデータ"), SerializeField]
    protected PlayerData playerData;
    public PlayerData PlayersData{get{return playerData;}private set{playerData = value;}}

    [Header("選択しているタイル"), SerializeField]
    protected GameObject chooseObj;
    public GameObject ChooseObj{get{return chooseObj;}set{chooseObj = value;}}
    [SerializeField, Header("取得した値")]
    protected List<uint> haveNum = new List<uint>(4){0, 0, 0, 0};
    public List<uint> HaveNum{get{return haveNum;}set{haveNum = value;}}
    // 初期座標
    protected Vector3 startPos;
    public Vector3 StartPos{get{return startPos;}protected set{startPos = value;}}
    // プレイヤーの移動回数
    [Header("プレイヤー行動回数カウント"), SerializeField]
    protected int moveCount = 0;     
    public int MoveCount{get{return moveCount;}set{moveCount = value;}}
    
    
    [SerializeField, Header("針管理クラス")]
    protected BaseNeedle needle;
    public BaseNeedle Needle{get{return needle;} set{needle = value;}}

    // 移動中か
    [Header("移動中フラグ"), SerializeField]
    protected bool onMove = false;
    public bool OnMove{get{return onMove;}set{onMove = value;}}

    [Header("回転中フラグ"), SerializeField]
    protected bool isRotate;
    public bool IsRotate{get{return isRotate;}set{isRotate = value;}}
    

    
    
    // 回転中心の座標(ワールド座標) 
    private Vector3 rotatePoint; 
    public Vector3 RotatePoint{get{return rotatePoint;} set{rotatePoint = value;}}
    
    //回転軸(ワールド座標)
    private Vector3 rotateAxis;  
    public Vector3 RotateAxis{get{return rotateAxis;} set{rotateAxis = value;}}
    
    // プレイヤーの回転角
    private float cubeAngle = 0;    
    public float CubeAngle{get{return cubeAngle;} set{cubeAngle = value;}}
    
    // プレイヤーの移動フラグ
    private int moveFlag = -1;      
    public int MoveFlag{get{return moveFlag;}set{moveFlag = value;}} 

    /// <summary>
    /// 回転軸の座標配列
    /// </summary>
    protected Vector3[] roteteAxisArr = 
    {
        new Vector3(0f, 0f, -1f),   // X(正)
        new Vector3(0f, 0f, 1f),    // X(負)
        new Vector3(1f, 0f, 0f),    // Z(正)
        new Vector3(-1f, 0f, 0f),   // Z(負)
        new Vector3(0f, 1f, 0f),    // Y(正)
        new Vector3(0f, -1f, 0f),   // Y(負)
        Vector3.zero                // 初期化用
    };
    public Vector3[] RotateAxisArr{get{return roteteAxisArr;}}

    /// <summary>
    /// 平面を転がるときの回転中心の座標配列
    /// </summary>
    protected Vector3[] rotatePointArr = 
    {
        new Vector3(1f, -1f, 0f),
        new Vector3(-1f, -1f, 0f),
        new Vector3(0f, -1f, 1f),
        new Vector3(0f, -1f, -1f)
    };
    public Vector3[] RotatePointArr{get{return rotatePointArr;}}

    /// <summary>
    /// 上段のタイルに上るときの回転中心座標の配列
    /// </summary>
    protected Vector3[] goUpRotatePointArr = 
    {
        new Vector3(1f, 1f, 0f),
        new Vector3(-1f, 1f, 0f),
        new Vector3(0f, 1f, 1f),
        new Vector3(0f, 1f, -1f)
    };
    public Vector3[] GoUpRotatePointArr{get{return goUpRotatePointArr;}}

    // Taskキャンセル処理用
    public CancellationTokenSource cts{get;private set;} = new CancellationTokenSource();
    public CancellationToken token{get; protected set;}
    [SerializeField]
    protected bool playerMoveCancel = false;
    public bool PlayerMoveCancel{get{return playerMoveCancel;}set{Debug.Log("reset");playerMoveCancel = value;}}

    public void OnDestroy()
    {
        PlayerMoveCancel = true;
        cts.Cancel();
    }

    
    // 失敗時のTween
    protected Tween playerFailureTween;
    public Tween PlayerFailureTween{get{return playerFailureTween;}set{playerFailureTween = value;}}
    // 成功時のTween
    protected Tween playerClearTween;
    public Tween PlayerClearTween{get{return playerClearTween;}set{playerClearTween = value;}}

    // UniTask
    public UniTask? WaitMove = null;
    
    protected PlayerMove playerMove = new PlayerMove();
}
