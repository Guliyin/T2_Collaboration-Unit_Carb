using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    [Header("调试选项")]
    [SerializeField] int healthMax = 1000;
    [Space]
    [Header("绑定物体")]
    [SerializeField] HealthBar_Boss healthBar;

    NumericalSystem healthSystem;
    NavMeshAgent navMeshAgent;
    BossStateMachine stateMachine;

    [HideInInspector] public BossParameters parameters;
    [HideInInspector] public Transform player;

    public float agentCurSpeed => navMeshAgent.velocity.magnitude;
    public float agentMaxSpeed => navMeshAgent.speed;

    enum BossAtaacks
    {
        Tread,
        Grab,
        Charge,
        Summon
    }

    private void Awake()
    {
        healthSystem = new NumericalSystem(healthMax);

        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = GetComponent<BossStateMachine>();
        parameters = GetComponent<BossParameters>();

        stateMachine.Init();
    }
    private void Start()
    {
        healthBar.Init(healthSystem.NormalizedAmount, healthMax);
        healthSystem.OnDamaged += OnDamaged;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void Move(Vector3 pos)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(pos);
    }
    public void Move(bool isMoving)
    {
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.isStopped = !isMoving;
    }
    void OnDamaged(object sender, EventArgs e)
    {
        healthBar.HealthSystem_OnDamaged(healthSystem.Amount);
        if (healthSystem.Amount == 0) print("Die");
    }
    public void Damage(int damage)
    {
        healthSystem.Damage(damage);
    }
    public Type NextMove()
    {
        return typeof(BossState_Tread);
    }
} 
