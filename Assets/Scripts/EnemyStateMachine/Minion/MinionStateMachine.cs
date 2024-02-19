using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionStateMachine : StateMachine
{
    [SerializeField] string[] stateNames;

    Animator animator;
    MinionController minion;
    public void Init()
    {
        animator = GetComponentInChildren<Animator>();
        minion = GetComponent<MinionController>();

        stateTable = new Dictionary<Type, IState>(stateNames.Length);

        for (int i = 0; i < stateNames.Length; i++)
        {
            Type t = Type.GetType(stateNames[i]);
            IState c = (IState)Activator.CreateInstance(t, stateNames[i], minion, animator, this);
            stateTable.Add(t, c);
        }
    }
    private void Start()
    {
        SwitchOn(stateTable[typeof(MinionState_Chase)]);
    }
}
