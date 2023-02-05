using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stage;
using Cysharp.Threading.Tasks;
using System.Threading;

public class PlayerController : BasePlayer
{
    
    // Start is called before the first frame update
    void Start()
    {
        Needle = GameObject.Find("Needles").GetComponent<BaseNeedle>();
        StartPos = this.transform.position;
        ChooseObj = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
        playerMove.Move(cts, Needle, this);

        
        
    }
    
}
