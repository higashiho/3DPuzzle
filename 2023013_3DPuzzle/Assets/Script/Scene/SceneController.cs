using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : BaseScene
{
    public static BaseScene  tmpScene{get; private set;}

    void Awake()
    {
        if(tmpScene != null)
        {
            Destroy(this.gameObject);
            return;
        }
        tmpScene = this;
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SecenTitle.titleMove(this);
    }
}
