using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseScene : MonoBehaviour
{
    [Header("タイトル暗転パネル")]
[SerializeField]
public Image fadePanel;

public TitleScene SecenTitle = new TitleScene();
}
