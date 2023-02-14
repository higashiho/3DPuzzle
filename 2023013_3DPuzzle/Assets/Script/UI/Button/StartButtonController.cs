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
                // シーン遷移フラグオン
                CallOnSceneMoveFlag();
                    // ボタンを押してフェードアウトが始まってから、シーンのステートが変わるまで待つ
                    DOVirtual.DelayedCall(Const.WAIT_TIME + Const.FADE_TIMER, () =>
                    {
                        // 全てのセーブデータを消す
                        SaveFile.SaveLoad.ResetSaveData();
                    });
            });
            // 続きからのボタンを押したら
            RestartButton.onClick.AddListener(() =>
            {
                // シーン遷移フラグオン
                CallOnSceneMoveFlag();

                // ボタンを押してフェードアウトが始まってからシーンのステートが変わるまで待つ
                DOVirtual.DelayedCall(Const.FADE_TIMER, () =>
                {
                    // セーブデータを読み込む
                    SaveFile.SaveLoad.DoLoad();
                });
                
            });
            FinishButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }
        
        // Update is called once per frame
        void Update()
        {
            
        }
    }
}