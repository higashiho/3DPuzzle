using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : BaseStage
{
    // Start is called before the first frame update
    void Awake()
    {
        instance.StageMaking(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
