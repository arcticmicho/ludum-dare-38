using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinComparer : Comparer<float>
{
    public override int Compare(float x, float y)
    {
        return (int) (x - y);
    }
    
}
