using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : StateMachine
{
    [SerializeField] BossState[] states;

    Animator animator;
    BossController boss;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        stateTable = new Dictionary<System.Type, IState>(states.Length);

        foreach (var state in states)
        {
            state.Initialize(animator, boss, this);
            stateTable.Add(state.GetType(), state);
        }
    }
    private void Start()
    {
        //SwitchOn(stateTable[typeof(BossState_Idle)]);
    }
}
