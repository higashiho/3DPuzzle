using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scene;
using LoadingImage;

namespace button
{
    public class EndSceneButtonCon
    {
        /// <summary>
        /// タイトルシーンに戻る
        /// </summary>
        public void Restart()
        {
            BaseScene.TmpScene.EndMove.Move(BaseScene.TmpScene, BaseLoadingImage.tmpImage);
        }
    }
}