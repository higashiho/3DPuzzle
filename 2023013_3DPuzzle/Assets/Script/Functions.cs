using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 汎用関数まとめクラス
/// </summary>
public class Functions 
{
    /// <summary>
    /// 対象の座標を偶数丸目する処理
    /// </summary>
    /// <param name="pos">対象座標</param>
    /// <returns>偶数まるめ後の座標</returns>
    public static Vector3? CalcRoundingHalfUp(Vector3? pos)
    {
        if(pos == null)
            return null;
        var tmpPos = (Vector3)pos;
        tmpPos.x = (int)Mathf.RoundToInt(tmpPos.x);
        tmpPos.y = (int)Mathf.RoundToInt(tmpPos.y);
        tmpPos.z = (int)Mathf.RoundToInt(tmpPos.z);

        return tmpPos;
    }

    /// <summary>
    /// 移動方向のフラグを立てる
    /// </summary>
    /// <param name="mySelf">自身の座標</param>
    /// <param name="other">移動先の座標</param>
    /// <returns>移動方向フラグ</returns>
    public static int SetDirection(Vector3 mySelf, Vector3 other)
    {
        if((mySelf.x < other.x) && (mySelf.z == other.z))
        {   
            return Const.RIGHT;
        }
        else if((mySelf.x > other.x) && (mySelf.z == other.z))
        {
            return Const.LEFT;
        }
        if((mySelf.x == other.x) && (mySelf.z < other.z))
        {
            return Const.FORWARD;
        }
        else if((mySelf.x == other.x) && (mySelf.z > other.z))
        {
            return Const.BACK;
        }
        return 0;
    }

    /// <summary>
    /// 回転中心を設定する処理
    /// </summary>
    /// <param name="Arr">回転中心配列</param>
    /// <param name="flag">移動フラグ</param>
    /// <returns>回転中心</returns>
    public static Vector3 SetRotatePoint(Vector3[] Arr, int flag)
    {
        if(flag == Const.RIGHT)
            return Arr[0];
        if(flag == Const.LEFT)
            return Arr[1];
        if(flag == Const.FORWARD)
            return Arr[2];
        if(flag == Const.BACK)
            return Arr[3];
        if(flag == Const.UP)
            return Arr[4];
        if(flag == Const.DOWN)
            return Arr[5];

        return Vector3.zero;
    }

    /// <summary>
    /// 回転軸の設定処理
    /// </summary>
    /// <param name="flag">移動フラグ</param>
    /// <returns>回転軸</returns>
    public static Vector3 SetRotateAxis(int flag)
    {
        if(flag == Const.RIGHT)
            return Const.RoteteAxisArr[0];
        if(flag == Const.LEFT)
            return Const.RoteteAxisArr[1];
        if(flag == Const.FORWARD)
            return Const.RoteteAxisArr[2];
        if(flag == Const.BACK)
            return Const.RoteteAxisArr[3];
        if(flag == Const.UP)
            return Const.RoteteAxisArr[4];
        if(flag == Const.DOWN)
            return Const.RoteteAxisArr[5];

        return Vector3.zero;
    }

}
