using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            
        }

        // Update is called once per frame
        void Update()
        {
            //各シーンごとの処理(フェードとシーン切り替え)
            switch(StateScene)
            {
                case BaseScene.SceneState.Title:
                    titleSceneMove.Move(this);
                break;

                case BaseScene.SceneState.MainFinish:
                    mainSceneMove.Move(this);
                break;

                case BaseScene.SceneState.End:
                    endSceneMove.Move(this);
                break;

                default:
                break;
            }
        }
    }
}
