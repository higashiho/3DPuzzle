using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SystemIO;
using Scene;
using DG.Tweening;

namespace button
{
    public class StartButtonController : BaseButton
    {
        // Start is called before the first frame update
        void Start()
        {
            // 子を取得・ボタンを取得
            StartButton = transform.GetChild(0).GetComponent<Button>();
            RestartButton = transform.GetChild(1).GetComponent<Button>();
            FinishButton = transform.GetChild(2).GetComponent<Button>();
            // ボタンの処理を追加
            // スタートボタンを押したら
            StartButton.onClick.AddListener(() =>
            {
                // しーん遷移フラグオン
                CallOnSceneMoveFlag();
                if(BaseScene.TmpScene.StateScene != BaseScene.SceneState.Main)
                {
                    // ボタンを押してフェードアウトが始まってからシーンのステートが変わるまで待つ
                    DOVirtual.DelayedCall(Const.WAIT_TIME + Const.FADE_TIMER, () =>
                    {
                        // 何のデータを消すかの場合分け
                        // 全部データあり
                        if(SaveData.DataSave.KeyNumber != null && SaveData.DataSave.ClearFlag != null)
                        {
                            // 全てのセーブデータを消す
                            SaveFile.SaveLoad.ResetSaveData();
                            return;
                        }
                        // クリアフラグだけあり
                        else if(SaveData.DataSave.KeyNumber == null && SaveData.DataSave.ClearFlag != null)
                        {
                            SaveFile.SaveLoad.ResetClearFlag();
                            return;
                        }
                        // 獲得数字のみあり
                        else if(SaveData.DataSave.KeyNumber != null && SaveData.DataSave.ClearFlag == null)
                        {
                            SaveFile.SaveLoad.ResetKeyNumber();
                            return;
                        }
                        else
                        {
                            return;
                        }
                    });
                }
            });
            // 続きからのボタンを押したら
            RestartButton.onClick.AddListener(() =>
            {
                // シーン遷移フラグオン
                CallOnSceneMoveFlag();
                // シーンのステートがMainになるまで待つ
                if(BaseScene.TmpScene.StateScene != BaseScene.SceneState.Main)
                {
                    // ボタンを押してフェードアウトが始まってからシーンのステートが変わるまで待つ
                    DOVirtual.DelayedCall(Const.WAIT_TIME + Const.FADE_TIMER, () =>
                    {
                        // データがないときは
                        if(SaveData.DataSave.KeyNumber == null && SaveData.DataSave.ClearFlag == null)
                        {
                            return;
                        }
                        else
                        {
                        // セーブデータを読み込む
                        SaveFile.SaveLoad.DoLoad();
                        }
                    });
                }
            });
        }
        
        // Update is called once per frame
        void Update()
        {
            
        }
    }
}