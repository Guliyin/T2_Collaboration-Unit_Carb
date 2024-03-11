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
    [SerializeField] public Transform SpawnPos;
    [SerializeField] public GameObject trash;

    HealthBar_Boss healthBar;
    NumericalSystem healthSystem;
    NavMeshAgent navMeshAgent;
    BossStateMachine stateMachine;

    [HideInInspector] public BossParameters parameters;
    [HideInInspector] public Transform player;

    public float agentCurSpeed => navMeshAgent.velocity.magnitude;
    public float agentMaxSpeed => navMeshAgent.speed;
    public bool isDead { get; set; }

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
        GameObject.Find("GameUI").transform.Find("BossHP").gameObject.SetActive(true);
        healthBar = FindObjectOfType<HealthBar_Boss>();
        healthBar.Init(healthSystem.NormalizedAmount, healthMax);
        healthSystem.OnDamaged += OnDamaged;
        healthSystem.OnHealed += OnHealed;
        Heal(healthMax);

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        //print(navMeshAgent.velocity.magnitude);
    }
    public bool Move(Vector3 pos)
    {
        navMeshAgent.isStopped = false;
        return navMeshAgent.SetDestination(pos);
    }
    public void Move(bool isMoving)
    {
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.isStopped = !isMoving;
    }
    public bool Move(float speed, Vector3 pos)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
        return navMeshAgent.SetDestination(pos);
    }
    void OnDamaged(object sender, EventArgs e)
    {
        healthBar.HealthSystem_OnDamaged(healthSystem.Amount);
    }
    public void Damage(int damage)
    {
        healthSystem.Damage(damage);
        GameManager.Instance.CollectDamage(damage);
        if (healthSystem.Amount <= 0 && !isDead)
        {
            stateMachine.SwitchState(typeof(BossState_Dead));
        }
    }
    public void Heal(int amount)
    {
        healthSystem.Heal(amount);
    }
    void OnHealed(object sender, EventArgs e)
    {
        healthBar.HealthSystem_OnHealed(healthSystem.Amount);
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
                break;
            }
        }

        switch (output)
        {
            case 0:
                return typeof(BossState_TreadPre);
            case 1:
                return typeof(BossState_GrabPre);
            case 2:
                return typeof(BossState_GrabTwice);
            case 3:
                return typeof(BossState_Summon);
            case 4:
                return typeof(BossState_TreadPre);
            default:
                return typeof(BossState_TreadPre);
        }
    }
    public void SpawnTrash()
    {
        Instantiate(trash, SpawnPos.GetChild(0).transform.position, Quaternion.identity, transform.parent);
        Instantiate(trash, SpawnPos.GetChild(1).transform.position, Quaternion.identity, transform.parent);
    }
}
