using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCounter : MonoBehaviour
{
    public int MoveCount = 0;
    [SerializeField]
    private Text moveCountText;

    void Start()
    {
        moveCountText = this.gameObject.GetComponent<Text>();
    }
    void Update()
    {
        moveCountText.text = "手数:" + MoveCount;
    }
}
