using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scene
{
    public class LoadingUI
    {

        public static LoadingUI loadingUI{get; set;}
        /// <summary>
        /// ロード進捗率をスライダーに反映
        /// </summary>
        /// <param name="tmpScene"></param>
        /// <returns></returns>
        public IEnumerator LoadData(BaseScene tmpScene)
        {
            while(!tmpScene.Async.isDone)
            {
                // ロードの進捗を0~1にして返す値を、0.9で割る
                var progressVal = Mathf.Clamp01(tmpScene.Async.progress / 0.9f);
                // スライダーに反映
                tmpScene.LoadSlider.value = progressVal;
                // なにこれ
                yield return null;
            }
        }
        
    }
}