using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionController : MonoBehaviour
{
    [Header("调试选项")]
    [SerializeField] int healthMax = 30;
    [Space]
    [Header("绑定物体")]
    [SerializeField] HealthBar healthBar;

    NumericalSystem healthSystem;
    MinionStateMachine stateMachine;
    Rigidbody rb;

    [HideInInspector] public MinionParameters parameters;
    [HideInInspector] public Transform player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        parameters = GetComponent<MinionParameters>();
        stateMachine = GetComponent<MinionStateMachine>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        healthSystem = new NumericalSystem(healthMax);
        stateMachine.Init();
    }
    private void Start()
    {
        healthSystem.OnDamaged += OnDamaged;
    }
    public void Move(Vector3 horizontalVelocity)
    {
        rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.z);
    }
    void OnDamaged(object sender, System.EventArgs e)
    {
        healthBar.HealthSystem_OnDamaged(healthSystem.NormalizedAmount);
    }
    public void Damage(int amount)
    {
        healthSystem.Damage(amount);
        if (healthSystem.Amount <= 0)
        {
            stateMachine.SwitchState(typeof(MinionState_Dead));
        }
        else
        {
            stateMachine.SwitchState(typeof(MinionState_Hit));
        }
    }
}
