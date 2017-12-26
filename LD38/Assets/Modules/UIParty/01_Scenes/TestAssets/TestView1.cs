using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestView1 : UIView
{
    protected override void OnViewOpened()
    {
        base.OnViewOpened();
        Debug.LogWarning("View 1 - Opened");
    }

    protected override void OnViewOpen()
    {
        base.OnViewOpen();
        Debug.LogWarning("View 1 - Open");
    }

    protected override void OnViewClose()
    {
        base.OnViewClose();
        Debug.LogWarning("View 1 - Closing");
    }

    protected override void OnViewClosed()
    {
        base.OnViewClosed();
        Debug.LogWarning("View 1 - Closed");
    }

}
