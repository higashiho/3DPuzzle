using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Cysharp.Threading.Tasks;

namespace Video
{
    /// <summary>
    /// ビデオ管理クラス
    /// </summary>
    public class TipVideoController : BaseTipVideo
    {
        // Start is called before the first frame update
        async void Start()
        {
            tipVideoMove = new TipVideoMove(this);
            TipVideo = GetComponent<VideoPlayer>();

            // ステージ生成が終わるまで処理を待機
            await InGameSceneController.Stages.Handle.Task;
        }

        // Update is called once per frame
        void Update()
        {
            tipVideoMove.changeClip();
        }
    }
}

