using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TestState
{
    Patrol,
    Chasing,
    Attacking,
    Dead
}
public class TestFSM : MonoBehaviour
{
    private ITestState currentState;
    private Dictionary< TestState, ITestState> states = new Dictionary<TestState, ITestState>();

    public void AddState(TestState stringState, ITestState state)
    {
        states.Add(stringState, state);
    }

    public void SwitchState(TestState stringState)
    {
        if(currentState != null)
            currentState.OnExitState();
        
        currentState = states[stringState];
        currentState.OnEnterState();
    }

    public void OnUpdateState()
    {
        currentState.UpdateState();
    }
}
