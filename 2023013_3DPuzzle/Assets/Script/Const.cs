using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Const : MonoBehaviour
{
    // 以下プレイヤー定数
    public const float PLAYER_POSY = 3f;
    // 以上プレイヤー定数

    // 以下Box用定数
    public const int CHECK_POS_X = 5;           // 接地判定用
    public const int CHECK_POS_Z = 5;           // 接地判定用
    public const float BOX_MOVE_SPEED = 3f;     // プレイヤーに押された際に移動するまでの時間
    public const float BOX_POS_Y = 4.5f;        // BoxのY座標
    // 以上Box用定数

}
