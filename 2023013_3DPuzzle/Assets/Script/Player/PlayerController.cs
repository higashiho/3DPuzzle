using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stage;

public class PlayerController : BasePlayer
{
    
    // Start is called before the first frame update
    void Start()
    {
        Needle = GameObject.Find("Needle").GetComponent<BaseNeedle>();
        StartPos = this.transform.position;
        ChooseObj = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        playerMove.Move(this, this.cts);
    }
    
}
