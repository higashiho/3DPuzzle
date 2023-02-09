using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace button
{
    public class EndButtonController : BaseButton
    {
        // Start is called before the first frame update
        void Start()
        {
            TitleBackButton = transform.GetChild(0).GetComponent<Button>();
            TitleBackButton.onClick.AddListener(CallOnSceneMoveFlag);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}