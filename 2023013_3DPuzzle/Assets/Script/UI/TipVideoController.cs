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
        void Start()
        {
            
            tipVideoMove = new TipVideoMove(this);
            TipVideo = GetComponent<VideoPlayer>();
        }

        // Update is called once per frame
        void Update()
        {
            tipVideoMove.changeClip();
        }
    }
}

