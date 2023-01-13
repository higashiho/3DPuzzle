using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : BaseBox
{
    // Start is called before the first frame update
    void Start()
    {
        instanceBox.CreateBox(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
