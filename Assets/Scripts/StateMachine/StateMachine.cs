using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    IState currentState;
    protected Dictionary<System.Type, IState> stateTable;

    private void Update()
    {
        currentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        currentState.PhysicUpdate();
    }
    protected void SwitchOn(IState newState)
    {
        currentState = newState;
        currentState.Enter();
    }
    public void SwitchState(IState newState)
    {
        currentState.Exit();
        SwitchOn(newState);
    }
    public void SwitchState(System.Type newStateType)
    {
        if (stateTable.ContainsKey(newStateType))
            SwitchState(stateTable[newStateType]);
        else
            throw new UnityException(string.Format("想切换的状态{0}不在状态列表中", newStateType));
    }
}
