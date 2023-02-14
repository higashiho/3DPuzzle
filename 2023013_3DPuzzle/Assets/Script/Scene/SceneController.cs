using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
            DOVirtual.DelayedCall(Const.FADE_TIMER, () =>
            {
                fadePanel.DOFade(endValue: Const.FADE_IN_ALPHA, duration: Const.FIRST_FADE_TIMER).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    fadePanel.enabled = false;
                    SceneTween = null;
                });
            }, false);
        }

        // Update is called once per frame
        void Update()
        {
                //各シーンごとの処理(フェードとシーン切り替え)
                switch(StateScene)
                {
                    case BaseScene.SceneState.Title:
                        titleSceneMove.Move(this, LoadingImage);
                    break;

                    case BaseScene.SceneState.Main:
                        #if DEBUG
                            if(Input.GetKeyDown("space"))//デバッグ用
                            {
                                SceneMoveFlagOn();
                                mainSceneMove.Move(this, LoadingImage);
                            }
                        #else
                        
                        #endif
                        break;

                    case BaseScene.SceneState.End:
                        EndMove.Move(this, LoadingImage);
                    break;

                    default:
                        break;
                }
        }
    }
}
