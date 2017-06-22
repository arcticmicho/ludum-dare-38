using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleView : UIView
{

    protected override void OnViewOpened()
    {
        base.OnViewOpened();
        Debug.Log("Idle View Opened");
    }
}
