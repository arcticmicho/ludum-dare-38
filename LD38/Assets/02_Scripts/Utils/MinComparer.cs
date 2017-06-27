using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinComparer : Comparer<long>
{
    public override int Compare(long x, long y)
    {
        if(x>y)
        {
            return 1;
        }else if(x<y)
        {
            return -1;
        }else
        {
            return 0;
        }
    }
    
}
