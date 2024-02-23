using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : StateMachine
{
    [SerializeField] string[] stateNames;

    Animator animator;
    BossController minion;
    public void Init()
    {
        animator = GetComponentInChildren<Animator>();
        minion = GetComponent<BossController>();

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
        SwitchOn(stateTable[typeof(BossState_Chase)]);
    }
}
