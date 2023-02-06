using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Save", menuName = "SaveAndLoad/SaveData")]
public class SaveData : ScriptableObject
{
    public List<KeyNumberData> KeyNumberDatas;

    public int GetKeyNumberID(KeyNumberData keyNumberData)
    {
        for(int i = 0; i < KeyNumberDatas.Count; i++)
        {
            if(KeyNumberDatas[i] == keyNumberData)
            {
                return i;
            }
        }
        Debug.Log("データベースにない数字が検出されました");
        return -1;
    }
}
