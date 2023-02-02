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
}
