using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StageConst
{
    

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
    /// ステージのエリア
    /// </summary>
    public static readonly Vector3[] AreaPos = new Vector3[4]
    {
        new Vector3(54.0f, 5.0f, 77.0f),        // 左上
        new Vector3(54.0f, 5.0f, 54.0f),        // 左下
        new Vector3(77.0f, 5.0f, 77.0f),        // 右上
        new Vector3(77.0f, 5.0f, 54.0f),        // 右下
    };
    /// <summary>
    /// 動画が流れる種類変換座標
    /// </summary>
    /// <value></value>
    public static readonly Vector3[] TipMoviePos = new Vector3[4]
    {
        new Vector3(59.0f, 5.0f, 72.0f),        // 左上
        new Vector3(59.0f, 5.0f, 59.0f),        // 左下
        new Vector3(72.0f, 5.0f, 72.0f),        // 右上
        new Vector3(72.0f, 5.0f, 59.0f),        // 右下
    };
    /// <summary>
    /// 倒れるスピード
    /// </summary>
    public const float ROTATE_TIME = 3.0f;
    /// <summary>
    /// 落下タイルが落下するまでのカウント最大値
    /// </summary>
    public const float FALL_COUNT_MAX = 2.0f;
    /// <summary>
    /// 落下ステージ制限時間
    /// </summary>
    public const int FALL_COUNTDOWN_TIME = 30;
    /// <summary>
    /// マグマが昇るY座標
    /// </summary>
    public const float MAGMA_MOVE_POS_Y = 2.3f;
    /// <summary>
    /// キータイル変換ゴール回数
    /// </summary>
    public const int GOAL_TILE_NUM = 1;
    /// <summary>
    /// ゴール回数が複数あるステージのマックス値
    /// </summary>
    public const int MAX_GOAL_NUM = 2;
    /// <summary>
    /// 壁オブジェクトをタイルに変更する対象オブジェクトの位置
    /// </summary>
    public static readonly Vector3 CHANGE_WALL_POS = new Vector3(130, 0, 20);
    /// <summary>
    /// ステージクリアの時に得られる数値最小値
    /// </summary>
    public const int MIN_NUM = 1;
    /// <summary>
    /// ステージクリアの時に得られる数値最大値
    /// </summary>
    public const int MAX_NUM = 9;
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
    // ステージのステート定数=====================================
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
}
