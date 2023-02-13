using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SystemIO;

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
            StartButton.onClick.AddListener(CallOnSceneMoveFlag);
            RestartButton.onClick.AddListener(() =>
            {
                CallOnSceneMoveFlag();
                SaveFile.SaveLoad.DoLoad();
            });
        }
        
        // Update is called once per frame
        void Update()
        {
            
        }
    }
}