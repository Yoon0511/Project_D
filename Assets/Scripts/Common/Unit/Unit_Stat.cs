using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MyStat
{
    public string key;
    public float value;
}


public partial class MyUnit : MonoBehaviour
{
    public List<MyStat> Stats = new List<MyStat>();

}
