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

        public void DoSave()
        {
            SaveData saveData = new SaveData();
            saveData.KeyNumber = InGameSceneController.Player.HaveNum;
            saveData.ClearFlag = InGameSceneController.Stages.StageClearFlags;
            SaveDataManager.SaveJson(saveData, filePath);
        }

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

        public static SaveFile SaveLoad{get; set;}
    }
}