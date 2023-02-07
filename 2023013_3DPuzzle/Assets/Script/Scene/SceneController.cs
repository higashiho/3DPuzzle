using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LoadingImage;
using UnityEngine.UI;

namespace Scene
{
    public class SceneController : BaseScene
    {

        void Awake()
        {
            //複数生成を避ける
            if(TmpScene != null)
            {
                Destroy(this.gameObject);
                return;
            }
            TmpScene = this;
            DontDestroyOnLoad(this);
        }

        // Start is called before the first frame update
        void Start()
        {
            fadePanel = GetComponentInChildren<Image>();
        }

        // Update is called once per frame
        void Update()
        {
                //各シーンごとの処理(フェードとシーン切り替え)
                switch(StateScene)
                {
                    case BaseScene.SceneState.Title:
                        if(!TmpScene.StartButton.enabled && !TmpScene.FinishButton.enabled && !TmpScene.RestartButton.enabled)
                        {
                            // タイトルボタン表示
                            BaseLoadingImage.tmpImage.SceneButton.OnTitleButton(TmpScene);
                        }
                        titleSceneMove.Move(this, loadingImage);
                    break;

                    case BaseScene.SceneState.Main:
                        if(Input.GetKeyDown("space"))//デバッグ用
                        {
                            SceneMoveFlagOn();
                            mainSceneMove.Move(this, loadingImage);
                        }
                        break;

                    case BaseScene.SceneState.End:
                        endSceneMove.Move(this, loadingImage);
                    break;

                    default:
                        break;
                }
        }
    }
}
