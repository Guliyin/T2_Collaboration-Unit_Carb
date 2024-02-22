using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionController : MonoBehaviour
{
    [Header("调试选项")]
    [SerializeField] int healthMax = 30;
    [Space]
    [Header("绑定物体")]
    [SerializeField] Texture textutre;
    [SerializeField] public GameObject attackArea;

    HealthBar healthBar;
    NumericalSystem healthSystem;
    MinionStateMachine stateMachine;
    Rigidbody rb;
    Material material;

    public Vector3 MoveSpeed => new Vector3(rb.velocity.x, 0, rb.velocity.z);

    [HideInInspector] public MinionParameters parameters;
    [HideInInspector] public Transform player;
    [HideInInspector] public bool isDead;

    Transform focusPoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        parameters = GetComponent<MinionParameters>();
        stateMachine = GetComponent<MinionStateMachine>();
        focusPoint = transform.Find("FoucsPoint");

        player = GameObject.FindGameObjectWithTag("Player").transform;

        healthSystem = new NumericalSystem(healthMax);
        stateMachine.Init();
    }
    private void Start()
    {
        healthBar = transform.GetChild(1).Find("HP").GetComponent<HealthBar>();

        healthSystem.OnDamaged += OnDamaged;

        var rend = GetComponentInChildren<Renderer>();
        rend.material = new Material(Shader.Find("Shader Graphs/Minion_Dissolve"));
        rend.material.SetTexture("_Main", textutre);
        material = rend.material;
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
        if (isDead) return;
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
    public void Dead()
    {
        StartCoroutine(PlayDissolveAnim());
    }
    IEnumerator PlayDissolveAnim()
    {
        focusPoint.gameObject.SetActive(false);
        if (player.GetComponent<PlayerController>().isLocking) player.GetComponent<PlayerController>().NewTarget();
        yield return new WaitForSeconds(0.15f);
        float amount = 0;
        while (amount <= 1)
        {
            amount += Time.deltaTime;
            material.SetFloat("_DissolveAmount", amount);
            yield return null;
        }
        Destroy(gameObject, 0.2f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerController>().Damage(parameters.damage);
        }
    }
}
