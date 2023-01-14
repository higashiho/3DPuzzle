using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Box
{
    public class BoxController : BaseBox
    {
        // Start is called before the first frame update
        void Start()
        {
            // 初期代入
            startColor = this.GetComponent<Renderer>().material.color;
            Parent = this.transform.parent.gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            // PosYに値が入っている場合は固定する
            if(PosY != null)
            {
                Debug.Log("座標固定中");
                boxMove.FixationPosY(this);
            }
        }
    }
}