using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BasePlayer
{
    
    // Start is called before the first frame update
    void Start()
    {
        ChooseObj = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        playerMove.Move(this);
    }
    
}
