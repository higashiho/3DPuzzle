using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScene
{
[Header("タイトル暗転パネル")]
[SerializeField]
private Image fadePanel;

private const float fadeEndValue = 1.0f;

private const float fadeTimer = 3.0f;

private void titleMove()
{
    if(Input.GetKeyDown(KeyCode.Return) && !fadePanel.enabled)
    {
        fadePanel.enabled = true;
        fadePanel.DOFade(endValue: fadeEndValue,duration: fadeTimer).SetEase(Ease.Linear).OnComplete(() =>
        {
            SceneManager.LoadScene("MainScene");
        });
    }
}


}
