using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace button
{
    public class ActSaveButton
    {
        /// <summary>
        /// 画面内に移動
        /// </summary>
        /// <param name="tmpButton"></param>
        public void ActiveSaveButton(BaseButton tmpButton)
        {
            tmpButton.SaveButton.transform.DOLocalMoveX(default, Const.SAVEBUTTON_MOVE_TIME)
            .SetEase(Ease.Linear);
        }

        /// <summary>
        /// 画面外に移動
        /// </summary>
        /// <param name="tmpButton"></param>
        public void OffSaveButton(BaseButton tmpButton)
        {
            tmpButton.SaveButton.transform.DOLocalMoveX(Const.SAVEBUTTON_MOVE_POS, Const.SAVEBUTTON_MOVE_TIME)
            .SetEase(Ease.Linear);
        }
    }
}