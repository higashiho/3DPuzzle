using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 定数保存用クラス
/// </summary>
public class Const
{
    // 以下ステート用定数
    /// <summary>
    /// ステートの初期値
    /// </summary>
    public const int STATE_START = 0x0000;
    // 以上ステート用定数

    // 以下プレイヤー定数
    public const float PLAYER_POSY = 5f;
    public const int PLAYER_ROTATE_MAX = 2;     // プレイヤーの回転可能回数上限
    public const float PLAYER_MOVABLE_POSY = 5f; // プレイヤーが移動可能なY座標最小値
    public const int RIGHT = 0;
    public const int LEFT = 1;
    public const int FORWARD = 2;
    public const int BACK = 3;
    public const int UP = 4;
    public const int DOWN = 5;

    public const int UPPER_ROTATE = 0;
    public const int NORMAL_ROTATE = 1;
    public const int DOWN_ROTATE = 2;


    /// <summary>
    /// 初期座標に戻る時間
    /// </summary>
    public const float START_BACK_TIME = 3.0f;
    // 以上プレイヤー定数
    
    // 以下Box用定数
    /// <summary>
    /// 接地判定用x座標
    /// </summary>
    public const int CHECK_POS_X = 5;
    /// <summary>
    /// 接地判定用y座標
    /// </summary>
    public const int CHECK_POS_Z = 5;
    /// <summary>
    /// Boxな半分のサイズ
    /// </summary>
    public const float CUBE_SIZE_HALF = 2.5f;
    // 以上Box用定数

    // 以下ビデオ用定数
    /// <summary>
    /// ビデオのアルファ値が変化する量
    /// </summary>
    public const float FADE_VIDEO_NUM = 0.1f;  
    /// <summary>
    /// ビデオのアルファ値が変化する時間（ミリ秒）
    /// </summary>
    public const int FADE_VIDEO_TIME = 100;  
    /// <summary>
    /// 透明度最大値
    /// </summary>
    public const float FADE_MAX_ALPHA = 1.0f;   
    // 以上ビデオ用定数

    // 以下タイトル定数--------------------------
    public const float FADE_END_VALUE = 1.0f;   //透明度の最大値
    public const float FADE_TIMER = 2.0f;       //フェードアウト終わるまでの時間
    // 以上タイトル定数--------------------------


    // 以下カメラ用定数--------------------------
    /// <summary>
    /// カメラの回転範囲の制限値
    /// </summary>
    public const float LIMIT_CAMERA_ANGLE_Y = 30.0f;
    /// <summary>
    /// カメラの回転範囲の制限値
    /// </summary>
    public const float LIMIT_CAMERA_ANGLE_X = 20.0f;
    /// <summary>
    /// ズームカメラのデフォルト視野狭窄
    /// </summary>
    public const int FIELD_OF_VIEW_DEFAULT = 50;
    /// <summary>
    /// カメラのズームスピード
    /// </summary>
    public const int ZOME_POWER = 10;
    /// <summary>
    /// カメラが各エリアに移るときのスピード
    /// </summary>
    public const float CAMERA_MOVE_SPEED = 0.4f;
    /// <summary>
    /// 円運動の回転速度
    /// </summary>
    public const float MOVE_AROUND_SPEED = 200.0f;
    /// <summary>
    /// 回転上限
    /// </summary>
    public const int UNDER_ROTATE_LIMIT = 20;
    public const int OVER_ROTATE_LIMIT = 70;
    // 以上カメラ用定数--------------------------


    // 以下回転用定数
    /// <summary>
    /// 一周 (2PI)
    /// </summary>
    public const int ONE_ROUND = 360;
    // 以上回転用定数

    // 以下タスク用定数
    /// <summary>
    /// ミリ秒を秒に変換用定数
    /// </summary>
    public const int CHANGE_SECOND = 1000;
    // 以上タスク用定数

    // 以下UI用定数
    /// <summary>
    /// 左のボタン
    /// </summary>
    public const int BUTOON_LEFT = 0;
    /// <summary>
    /// 真ん中左のボタン
    /// </summary>
    public const int BUTOON_CENTER_LEFT = 1;
    /// <summary>
    /// 真ん中右のボタン
    /// </summary>
    public const int BUTOON_CENTER_RIGHIT = 2;
    /// <summary>
    /// 右のボタン
    /// </summary>
    public const int BUTOON_RIGHIT = 3;
    /// <summary>
    /// 南京錠の値最大値
    /// </summary>
    public const int NUM_MAX = 10;
    /// <summary>
    /// ループ回数
    /// </summary>
    public const int LOOP_NUM = 2;
    /// <summary>
    /// ポップアップ目標X座標
    /// </summary>
    public const float POPUP_TARGET_POS_X = 710f;
    /// <summary>
    /// Popupの目標ｘ座標まで行く時間
    /// </summary>
    public const float POPUP_MOVE_TIMER_X = 2.0f;
    /// <summary>
    /// ポップアップ目標Y座標
    /// </summary>
    public const float POPUP_TARGET_POS_Y = 20f;
    /// <summary>
    /// Popupの待ち時間
    /// </summary>
    public const float POPUP_DELAY_TIME = 3.0f; 
    // 以上UI用定数

    // 以下回転用定数**********************

    /// <summary>
    /// 回転軸の座標配列
    /// </summary>
    public static readonly Vector3[] RoteteAxisArr = new Vector3[7]
    {
        new Vector3(0f, 0f, -1f),   // X(正)
        new Vector3(0f, 0f, 1f),    // X(負)
        new Vector3(1f, 0f, 0f),    // Z(正)
        new Vector3(-1f, 0f, 0f),   // Z(負)
        new Vector3(0f, 1f, 0f),    // Y(正)
        new Vector3(0f, -1f, 0f),   // Y(負)
        Vector3.zero                // 初期化用
    };

    /// <summary>
    /// 平面を転がるときの回転中心の座標配列
    /// </summary>
    public static readonly Vector3[] RotatePointArr = new Vector3[4]
    {
        new Vector3(1f, -1f, 0f),
        new Vector3(-1f, -1f, 0f),
        new Vector3(0f, -1f, 1f),
        new Vector3(0f, -1f, -1f)
    };

    /// <summary>
    /// 上段のタイルに上るときの回転中心座標の配列
    /// </summary>
    public static readonly Vector3[] GoUpRotatePointArr = new Vector3[4]
    {
        new Vector3(1f, 1f, 0f),
        new Vector3(-1f, 1f, 0f),
        new Vector3(0f, 1f, 1f),
        new Vector3(0f, 1f, -1f)
    };

    // 以上回転用定数********************************
}
