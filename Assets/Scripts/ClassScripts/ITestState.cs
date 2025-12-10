using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITestState
{
    public void OnEnterState();
    public void OnExitState();
    public void UpdateState();
}
