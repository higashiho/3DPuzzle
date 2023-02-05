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
            if(tmpScene != null)
            {
                Destroy(this.gameObject);
                return;
            }
            tmpScene = this;
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
                    BaseLoadingImage.tmpImage.TitleButton.OffTitleButton(this);
                    titleSceneMove.Move(this, loadingImage);
                break;

                case BaseScene.SceneState.Main:
                    if(Input.GetKeyDown("space"))//デバッグ用
                    {
                        SceneMoveOnFlag = true;
                        BaseLoadingImage.tmpImage.OnLoadingImages();
                        BaseLoadingImage.tmpImage.LoadingImageAnimation(BaseLoadingImage.tmpImage);
                        mainSceneMove.Move(this, loadingImage);
                    }
                    break;

                case BaseScene.SceneState.End:
                    BaseLoadingImage.tmpImage.TitleButton.OnTitleButton(this);
                    BaseLoadingImage.tmpImage.ImageFill.OffLoadingImages(BaseLoadingImage.tmpImage, this);
                    BaseLoadingImage.tmpImage.TitleButton.OnClickButton(BaseLoadingImage.tmpImage, tmpScene);
                break;

                default:
                    break;
            }
        }
    }
}
