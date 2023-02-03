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
        //自分を入れる用
        public static BaseScene  tmpScene{get; set;}

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
                    titleSceneMove.Move(this, loadingImage);
                break;

                case BaseScene.SceneState.Main:
                    if(fadePanel.color.a == Const.FADE_OUT_ALPHA)
                    {
                        MoveFade.FadeIn(this);
                    }
                    break;
                case BaseScene.SceneState.MainFinish:
                    mainSceneMove.Move(this, loadingImage);
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
