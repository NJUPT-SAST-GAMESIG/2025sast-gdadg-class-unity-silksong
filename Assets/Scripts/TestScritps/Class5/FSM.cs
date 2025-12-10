using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    Patrol,
    Chasing,
    Attacking,
    Die,
}
public class FSM
{
    private IState currentState;
    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();

    public void AddState(StateType stateType, IState state)
    {
        states.TryAdd(stateType, state);
    }
    
    public void SwitchState(StateType stateType)
    {
        currentState?.OnExitState();
        
        if (states.TryGetValue(stateType, out var state))
        {
            currentState = state;
        }

        currentState?.OnEnterState();
    }
    
    public void OnUpdateState()
    {
        currentState?.OnUpdateState();
    }
}
