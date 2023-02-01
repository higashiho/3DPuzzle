using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scene
{
    public class TitleSceneMove
    {
        // シーン遷移
        public void Move(BaseScene tmpScene)
        {
            //タイトルシーンからメインシーンへ
            tmpScene.MoveFade.FadeIn(tmpScene, "MainScene");
            //tmpScene.StateScene = BaseScene.SceneState.Main;
        }

        /// <summary>
        /// マウスがボタンの上に乗ったら
        /// 押せるようにする
        /// </summary>
        /// <param name="tmpTitle">BaseTitle</param>
        public void OnMouseOver(BaseScene tmpTitle)
        {
            if(!tmpTitle.OnToButtonFlag)
            {
                tmpTitle.OnToButtonFlag = true;
            }
        }
        /// <summary>
        /// マウスがボタンの上から離れたら
        /// 押せなくする
        /// </summary>
        /// <param name="tmpTitle">BaseTitle</param>
        public void OnMouseExit(BaseScene tmpTitle)
        {
            if(tmpTitle.OnToButtonFlag)
            {
                tmpTitle.OnToButtonFlag = false;
            }
        }
    }
}