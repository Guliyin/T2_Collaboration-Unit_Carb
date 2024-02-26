using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    [Header("����ѡ��")]
    [SerializeField] int healthMax = 1000;
    [Space]
    [Header("������")]
    [SerializeField] HealthBar_Boss healthBar;

    NumericalSystem healthSystem;
    NavMeshAgent navMeshAgent;
    BossStateMachine stateMachine;

    [HideInInspector] public BossParameters parameters;
    [HideInInspector] public Transform player;

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
        navMeshAgent.SetDestination(pos);
    }
    void OnDamaged(object sender, System.EventArgs e)
    {
        healthBar.HealthSystem_OnDamaged(healthSystem.Amount);
        if (healthSystem.Amount == 0) print("Die");
    }
    public void Damage(int damage)
    {
        healthSystem.Damage(damage);
    }
    public void NextMove()
    {

    }
} 
