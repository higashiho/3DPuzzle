using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

/// <summary>
/// セーブデータクラス
/// </summary>
[Serializable]
public class SaveData
{
    // プレイヤーの獲得した数字
    public List<uint> KeyNumber;
    // プレイヤーがステージをクリアしたか
    public bool[] ClearFlag;

    /// <summary>
    /// オブジェクトからJSONに変換
    /// </summary>
    /// <returns></returns>
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    /// <summary>
    /// JSONからオブジェクトに変換
    /// </summary>
    /// <param name="jsonStr"></param>
    /// <returns></returns>
    public SaveData FromJson(string jsonStr)
    {
        return JsonUtility.FromJson<SaveData>(jsonStr);
    }
}


public static class SaveDataManager
{
    /// <summary>
    /// セーブ処理
    /// </summary>
    /// <param name="saveData"></param>
    /// <param name="filePath"></param>
    public static void SaveJson(SaveData saveData,string filePath)
    {
        if(FileController.WriteFile(filePath, saveData.ToJson()))
        {
            Debug.Log("セーブ完了");
        }
    }

    /// <summary>
    /// ロード処理
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static SaveData LoadJson(string filePath)
    {
        string json = FileController.ReadFile(filePath);
        if(!"".Equals(json))
        {
            SaveData saveData = new SaveData();
            return saveData.FromJson(json);
        }
        return null;
    }
}

public class FileController
{
    public static bool WriteFile(string filePath, string json)
    {
        try
        {
            File.WriteAllText(filePath, json);
            return true;
        }
        catch(Exception e)
        {
            Debug.LogError("書き込み失敗しました");
            return false;
        }
    }

    public static string ReadFile(string filePath)
    {
        try
        {
            return File.ReadAllText(filePath);
        }
        catch(Exception e)
        {
            Debug.LogError("読み込み失敗");
            return "";
        }
    }
}
