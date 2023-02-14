using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveData
{
    // プレイヤーの獲得した数字
    public List<uint> PlayerHasNumber = new List<uint>(4);
    // プレイヤーがステージをクリアしたか
    public bool[] ClaerFlags = new bool[4];
}
