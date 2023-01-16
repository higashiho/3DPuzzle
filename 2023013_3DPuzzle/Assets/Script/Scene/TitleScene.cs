using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScene
{
    //フェードアウト、シーン遷移
    public void titleMove(BaseScene tmpScene)
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            tmpScene.fadePanel.enabled = true;
            tmpScene.fadePanel.DOFade(endValue: Const.FADE_END_VALUE,duration: Const.FADE_TIMER).SetEase(Ease.Linear).OnComplete(() =>
            {
                SceneManager.LoadScene("MainScene");
            });
        }
    }
}
