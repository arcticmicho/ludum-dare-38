using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
    DateTime ActionTime { get; }
    void StartAction();
    void EndAction();
    bool ProcessAction(float deltaTime);
}
