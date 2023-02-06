using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyKeyNumber/Create KeyNumberData", fileName = "KeyNanberData")]
public class KeyNumberData : ScriptableObject
{
    [SerializeField]
    public int one = 1;
    public int twe = 2;
    public int three = 3;
    public int four = 4;
    public int five = 5;
    public int six = 6;
    public int seven = 7;
    public int eight = 8;
    public int nine = 9;
    public int zero = 0;
}
