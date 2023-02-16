using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Scene
{
    public class EndSceneBoxController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            EndBoxMove();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        /// <summary>
        /// エンドシーンでのボックスの挙動
        /// </summary>
        private void EndBoxMove()
        {
            this.gameObject.transform.DORotate(Vector3.up * Const.ONE_ROUND, Const.END_BOX_ROTATE_TIME, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear).SetLoops(Const.LOADING_ANIMATION_INFINITY, LoopType.Restart).SetLink(this.gameObject);

            this.gameObject.transform.DOMove(InGameSceneController.Scene.EndBoxPos, Const.END_BOX_ROTATE_TIME)
            .SetEase(Ease.OutQuint).SetLoops(Const.LOADING_ANIMATION_INFINITY, LoopType.Yoyo).SetLink(this.gameObject);
        }
    }
}