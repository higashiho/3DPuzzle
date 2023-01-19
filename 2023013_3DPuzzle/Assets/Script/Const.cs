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
    public const float PLAYER_POSY = 3f;
    public const uint RIGHT = 0x0000;
    public const uint LEFT = 0x0001;
    public const uint FORWARD = 0x0002;
    public const uint BACK = 0x0004;
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
    /// プレイヤーに押された際に移動するまでの時間
    /// </summary>
    public const float BOX_MOVE_SPEED = 3f;
    /// <summary>
    /// BoxのY座標
    /// </summary>
    public const float BOX_POS_Y = 4.5f;
    /// <summary>
    /// Boxな半分のサイズ
    /// </summary>
    public const float CUBE_SIZE_HALF = 2.5f;
    /// <summary>
    /// 動かせないボックスの比率最大数（1/3で出現）
    /// </summary>
    public const int BOX_DONT_MOVE_NUM = 3;
    // 以上Box用定数

    // 以下ステージ用定数
    /// <summary>
    /// ステージの左上エリア
    /// </summary>
    public static readonly Vector3 Area1Pos = new Vector3(45.0f, 5.0f, 75.0f);
    /// <summary>
    /// ステージの左下エリア
    /// </summary>
    public static readonly Vector3 Area2Pos = new Vector3(45.0f, 5.0f, 45.0f);
    /// <summary>
    /// ステージの右上エリア
    /// </summary>
    public static readonly Vector3 Area3Pos = new Vector3(75.0f, 5.0f, 75.0f);
    /// <summary>
    /// ステージの右下エリア
    /// </summary>
    public static readonly Vector3 Area4Pos = new Vector3(75.0f, 5.0f, 45.0f);
    /// <summary>
    /// 倒れるスピード
    /// </summary>
    public const float ROTATE_TIME = 3.0f;
    // 動かせるステージのステート定数==============================
    /// <summary>
    /// 立っている状態
    /// </summary>
    public const int STATE_STAND_UP = 0x0001;
    /// <summary>
    /// 倒れている状態
    /// </summary>
    public const int STATE_FALL = 0x0002;
    // =========================================================
    // ステージのステート定数
    /// <summary>
    /// 針出現ステージ
    /// </summary>
    public const int STATE_NEEDLE_STAGE = 0x0001;
    /// <summary>
    /// 動かせるステージブロック出現ステージ
    /// </summary>
    public const int STATE_MOVE_STAGE = 0x0002;
    /// <summary>
    /// 落ちる床出現ステージ
    /// </summary>
    public const int STATE_FALLING_STAGE = 0x0004;
    /// <summary>
    ///  スイッチ出現ステージ
    /// </summary>
    public const int STATE_SWITCH_STAGE = 0x0008;
    // =========================================================

    // 以上ステージ用定数


    // 以下タイトル定数
    public const float FADE_END_VALUE = 1.0f;   //透明度の最大値
    public const float FADE_TIMER = 2.0f;       //フェードアウト終わるまでの時間
    // 以上タイトル定数

    // 以下カメラ用定数
    // 以上カメラ用定数

    // 以下回転用定数
    /// <summary>
    /// 一周 (2PI)
    /// </summary>
    public const int ONE_ROUND = 360;
    // 以上回転用定数

    // 以下アウトゲーム関係
    //public const float MaxPosY = 180;
    // 以上アウトゲーム関係
}
