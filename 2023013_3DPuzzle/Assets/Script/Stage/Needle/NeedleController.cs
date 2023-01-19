using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage
{
    /// <summary>
    /// 針ステージ挙動管理クラス
    /// </summary>
    public class NeedleController : BaseNeedle
    {
        // Start is called before the first frame update
        void Start()
        {
            // ニードル取得
            NeedleTiles = GameObject.FindGameObjectsWithTag("Needle");
        }

        // Update is called once per frame
        void Update()
        {
            needleMove.Move(this);
        }
    }
}