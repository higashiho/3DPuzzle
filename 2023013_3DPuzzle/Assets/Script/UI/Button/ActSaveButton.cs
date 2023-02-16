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
            if(tmpButton.SaveButtonTween[0] == null)
            tmpButton.SaveButtonTween[0] = tmpButton.SaveButton.transform.DOLocalMoveX(default, Const.SAVEBUTTON_MOVE_TIME)
            .SetEase(Ease.Linear).OnStart(() => tmpButton.SaveButtonTween[1] = null);
        }

        /// <summary>
        /// 画面外に移動
        /// </summary>
        /// <param name="tmpButton"></param>
        public void OffSaveButton(BaseButton tmpButton)
        {
            if(tmpButton.SaveButtonTween[1] == null)
            tmpButton.SaveButtonTween[1] = tmpButton.SaveButton.transform.DOLocalMoveX(Const.SAVEBUTTON_MOVE_POS, Const.SAVEBUTTON_MOVE_TIME)
            .SetEase(Ease.Linear).OnStart(() => tmpButton.SaveButtonTween[0] = null);
        }
    }
}