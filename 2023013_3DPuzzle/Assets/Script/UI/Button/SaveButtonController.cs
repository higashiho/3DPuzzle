using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scene;
using SystemIO;

namespace button
{
    public class SaveButtonController : BaseButton
    {
        // Start is called before the first frame update
        void Start()
        {   
            SaveButton = GetComponent<Button>();
            // ボタンに処理を追加
            SaveButton.onClick.AddListener(() =>
            {
                if(BaseScene.TmpScene.SceneTween == null)
                {
                    // 記録をセーブする
                    SaveFile.SaveLoad.DoSave();
                    // タイトルに戻るため、今はエンドにいるということにする
                    BaseScene.TmpScene.StateScene = BaseScene.SceneState.End;
                    // フラグオン
                    CallOnSceneMoveFlag();
                    // タイトルシーンに戻る
                    BaseScene.TmpScene.EndMove.Move(BaseScene.TmpScene, BaseScene.TmpScene.LoadingImage);
                }
            });
        }

        // Update is called once per frame
        void Update()
        {
            //中央にいるときだけセーブボタン出現
            if(InGameSceneController.Stages.StageState == Const.STATE_START)
            {
                SaveButtonAct.ActiveSaveButton(this);
            }
            else
            {
                SaveButtonAct.OffSaveButton(this);
            }
        }
    }
}