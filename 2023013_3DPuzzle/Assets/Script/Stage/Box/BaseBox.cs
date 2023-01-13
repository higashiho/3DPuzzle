using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBox : MonoBehaviour
{
    [SerializeField,Header("生成するBox")]
    protected GameObject prefabBox;
    public GameObject PerfabBox{get{return prefabBox;}private set{prefabBox = value;}}

    // インスタンス化
    protected InstanceBox instanceBox = new InstanceBox();
}
