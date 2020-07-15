using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Data
{
    public string Name;
    public int Money;
    public int Chapter; //1, 2, 3, ...
    public int Progress; //0, 25, 50, 75, 100 for each chapter
}
