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
    /// ニードルタイルの出ている針が変わる間隔
    /// </summary>
    public const int CHANGE_NEEDLE_NUM = 3;
    /// <summary>
    /// ニードルタイルの色が変わる値
    /// </summary>
    public const int CHANGE_NEEDLE_TILE_COLOR_NUM = 2;
    /// <summary>
    /// Clear挙動の移動時間
    /// </summary>
    public const float CLEAR_MOVE_TIME = 5.0f;
    /// <summary>
    /// Clear挙動の一周の回転時間
    /// </summary>
    public const float CLEAR_ROTATE_TIME = 0.5f;
    /// <summary>
    ///  Clear挙動のy座標更新用
    /// </summary>
    public const float CLEAR_MAX_POS_Y = 100.0f;
    /// <summary>
    /// Clear挙動の移動開始待ち時間
    /// </summary>
    public const float CLEAR_STOP_TIME = 2.0f;
    /// <summary>
    /// ステージの左上エリア
    /// </summary>
    public static readonly Vector3 Area1Pos = new Vector3(49.0f, 5.0f, 72.0f);
    /// <summary>
    /// ステージの左下エリア
    /// </summary>
    public static readonly Vector3 Area2Pos = new Vector3(49.0f, 5.0f, 49.0f);
    /// <summary>
    /// ステージの右上エリア
    /// </summary>
    public static readonly Vector3 Area3Pos = new Vector3(72.0f, 5.0f, 72.0f);
    /// <summary>
    /// ステージの右下エリア
    /// </summary>
    public static readonly Vector3 Area4Pos = new Vector3(72.0f, 5.0f, 49.0f);
    /// <summary>
    /// 倒れるスピード
    /// </summary>
    public const float ROTATE_TIME = 3.0f;
    /// <summary>
    /// 落下タイルが落下するまでのカウント最大値
    /// </summary>
    public const float FALL_COUNT_MAX = 2.0f;
    // 動かせるステージのステート定数==============================
    /// <summary>
    /// 立っている状態
    /// </summary>
    public const uint STATE_STAND_UP = 0x0001;
    /// <summary>
    /// 倒れている状態
    /// </summary>
    public const uint STATE_FALL = 0x0002;
    // =========================================================
    // ステージのステート定数
    /// <summary>
    /// 針出現ステージ
    /// </summary>
    public const uint STATE_NEEDLE_STAGE = 0x0001;
    /// <summary>
    /// 動かせるステージブロック出現ステージ
    /// </summary>
    public const uint STATE_MOVE_STAGE = 0x0002;
    /// <summary>
    /// 落ちる床出現ステージ
    /// </summary>
    public const uint STATE_FALLING_STAGE = 0x0004;
    /// <summary>
    ///  スイッチ出現ステージ
    /// </summary>
    public const uint STATE_SWITCH_STAGE = 0x0008;
    // =========================================================

    // 以上ステージ用定数


    // 以下タイトル定数
    public const float FADE_END_VALUE = 1.0f;   //透明度の最大値
    public const float FADE_TIMER = 2.0f;       //フェードアウト終わるまでの時間
    // 以上タイトル定数


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
    /// カメラ回転速度減補正係数
    /// </summary>
    public const float ROTATE_CAMERA_SPEED = 0.1f;
    /// <summary>
    /// ズームカメラのデフォルト視野狭窄
    /// </summary>
    public const int FIELD_OF_VIEW_DEFAULT = 50;
    // 以上カメラ用定数--------------------------


    // 以下回転用定数
    /// <summary>
    /// 一周 (2PI)
    /// </summary>
    public const int ONE_ROUND = 360;
    // 以上回転用定数

    //以下アウトゲーム関係
    /// <summary>
    /// ズーム速度
    /// </summary>
    public const float ZOME_POWER = 20.0f;
    // 以上アウトゲーム関係
}
