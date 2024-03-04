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
    [SerializeField] public Transform treadArea;

    HealthBar_Boss healthBar;
    NumericalSystem healthSystem;
    NavMeshAgent navMeshAgent;
    BossStateMachine stateMachine;

    [HideInInspector] public BossParameters parameters;
    [HideInInspector] public Transform player;

    public float agentCurSpeed => navMeshAgent.velocity.magnitude;
    public float agentMaxSpeed => navMeshAgent.speed;

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
        GameObject.Find("GameUI").transform.GetChild(3).gameObject.SetActive(true);
        healthBar = FindObjectOfType<HealthBar_Boss>();
        healthBar.Init(healthSystem.NormalizedAmount, healthMax);
        healthSystem.OnDamaged += OnDamaged;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null) print("null");
        else print("bu null");
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
        int rand = UnityEngine.Random.Range(1, parameters.AbilityTotal);
        int temp = 0;
        int output = 0;

        for (int i = 0; i < parameters.AbilityWeight.Length; i++)
        {
            temp += parameters.AbilityWeight[i];
            if (rand < temp)
            {
                output = i;
            }
        }

        switch (output)
        {
            case 0:
                return typeof(BossState_Tread);
            case 1:
                return typeof(BossState_Summon);
            case 2:
                return typeof(BossState_Tread);
            case 3:
                return typeof(BossState_Summon);
            case 4:
                return typeof(BossState_Tread);
            default:
                return typeof(BossState_Tread);
        }

        //return typeof(BossState_TreadPre);
    }
}
