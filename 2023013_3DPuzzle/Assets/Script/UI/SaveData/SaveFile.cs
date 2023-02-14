using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

namespace SystemIO
{
    [Serializable]
    public class SaveFile : MonoBehaviour
    {
        public static SaveFile SaveLoad{get; protected set;}
        private string filePath;

        void Awake()
        {
            if(SaveLoad != null)
            {
                return;
            }
            filePath = Application.persistentDataPath + "/" + ".savedata.txt";
            Debug.Log(filePath);
            SaveLoad = this;
        }

        /// <summary>
        /// データを上書きする
        /// </summary>
        public void DoSave()
        {
            // 保存するデータを作成
            var data = new SaveData()
            {
                PlayerHasNumber = InGameSceneController.Player.HaveNum,

                ClaerFlags = InGameSceneController.Stages.StageClearFlags,
            };

            // オブジェクトを JSON 文字列にシリアライズ
            var json = JsonUtility.ToJson(data);

            // 所定の場所にテキストファイルとして保存
            File.WriteAllText(Path.Combine(Application.persistentDataPath, ".savedata.txt"), json);
        }

        /// <summary>
        /// データを読み込む
        /// </summary>
        public void DoLoad()
        {
            var json = File.ReadAllText(Path.Combine(Application.persistentDataPath + "/" + ".savedata.txt"));
            var data = JsonUtility.FromJson<SaveData>(json);

            InGameSceneController.Player.HaveNum = data.PlayerHasNumber;

            InGameSceneController.Stages.StageClearFlags = data.ClaerFlags;
        }

        /// <summary>
        /// 初めからを押したらデータが消える
        /// </summary>
        public void ResetSaveData()
        {
            // 保存するデータを作成
            var data = new SaveData()
            {
                PlayerHasNumber = {0,0,0,0},
                ClaerFlags = new bool[4],
            };

            // オブジェクトを JSON 文字列にシリアライズ
            var json = JsonUtility.ToJson(data);

            // 所定の場所にテキストファイルとして保存
            File.WriteAllText(Path.Combine(Application.persistentDataPath, "SaveData.txt"), json);
        }

    }
}