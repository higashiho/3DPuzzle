using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Box
{
    public class StairsMove
    {
        // インスタンス化
        private BoxMove boxMove = new BoxMove();

        /// <summary>
        /// 挙動
        /// </summary>
        /// <param name="tmpBox"></param>
        public void Move(BaseStairs tmpStairs)
        {
            if(boxMove.Chack(tmpStairs.gameObject))  
            {
                // 移動不可オブジェクトじゃなくて挙動中じゃない場合
                if(tmpStairs.GetComponent<Renderer>().material.color != Color.yellow && !InGameSceneController.Stages.Moving)
                {
                    tmpStairs.GetComponent<Renderer>().material.color = Color.green;

                    fall(tmpStairs);
                }
            }
            else   
                tmpStairs.GetComponent<Renderer>().material.color = tmpStairs.StartColor;
                

        }

        private void fall(BaseStairs tmpStairs)
        {
            // 右クリックで右回転
            if(Input.GetMouseButtonDown(1))
                tmpStairs.transform.DORotate()

            // 左クリックで左回転
            else if(Input.GetMouseButtonDown(0))
        }
    }
}