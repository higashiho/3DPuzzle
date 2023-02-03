using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Box
{
    /// <summary>
    /// ボックスの挙動管理クラス
    /// </summary>
    public class BoxController : BaseBox
    {
        // Start is called before the first frame update
        void Start()
        {
            TipButton = GameObject.FindWithTag("TipButton").GetComponent<Button>();
            OpenBoxUI = GameObject.FindWithTag("TreasureBoxPanel").GetComponent<Image>();
            OpenBoxUI.gameObject.SetActive(false);
            boxMove = new BoxMove(this);
        }

        // Update is called once per frame
        void Update()
        {
            boxMove.Move();
        }
    }
}