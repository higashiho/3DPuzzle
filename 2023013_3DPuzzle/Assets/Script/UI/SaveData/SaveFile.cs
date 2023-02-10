using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SystemIO
{
    public class SaveFile : MonoBehaviour
    {
        string filePath;
        void Awake()
        {
            filePath = Application.persistentDataPath + "/" + ".savedata.json";
        }

        /// <summary>
        /// データを上書きする
        /// </summary>
        public void DoSave()
        {
            SaveData saveData = new SaveData();
            saveData.KeyNumber = InGameSceneController.Player.HaveNum;
            saveData.ClearFlag = InGameSceneController.Stages.StageClearFlags;
            // 形式を変更
            SaveDataManager.SaveJson(saveData, filePath);
        }

        /// <summary>
        /// データを読み込む
        /// </summary>
        public void DoLoad()
        {
            SaveData saveData = SaveDataManager.LoadJson(filePath);
            if(saveData != null)
            {
                Debug.Log("HasNumber : " + saveData.KeyNumber);
                InGameSceneController.Player.HaveNum = saveData.KeyNumber;
                InGameSceneController.Stages.StageClearFlags = saveData.ClearFlag;
            }
        }

        /// <summary>
        /// 初めからを押したらデータが消える
        /// </summary>
        public void ResetSaveData()
        {
            SaveData saveData = new SaveData();
            saveData.KeyNumber = null;
            saveData.ClearFlag = null;
        }

        public void ResetKeyNumber()
        {
            SaveData saveData = new SaveData();
            saveData.KeyNumber = null;
        }
        public void ResetClearFlag()
        {
            SaveData saveData = new SaveData();
            saveData.ClearFlag = null;
        }

        public static SaveFile SaveLoad{get; protected set;}
    }
}