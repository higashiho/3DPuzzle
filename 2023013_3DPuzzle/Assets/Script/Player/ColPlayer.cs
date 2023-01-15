using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColPlayer : MonoBehaviour
{
    
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Tiles")
        {
            
        }
    }
   
}
