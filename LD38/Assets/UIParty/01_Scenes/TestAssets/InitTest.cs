using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitTest : MonoBehaviour
{
    void Start()
    {
        UIPartyManager.Instance.RequestView<TestView1>();
    }
}
