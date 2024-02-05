using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] int healthMax = 1000;

    [SerializeField] HealthBar_Boss healthBar;

    HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = new HealthSystem(healthMax);
    }
    private void Start()
    {
        healthBar.Init(healthSystem.NormalizedHealth, healthMax);
        healthSystem.OnDamaged += OnDamaged;
    }
    void OnDamaged(object sender, System.EventArgs e)
    {
        healthBar.HealthSystem_OnDamaged(healthSystem.Health);
        if (healthSystem.Health == 0) print("Die");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            healthSystem.Damage(11);
            if (healthSystem.Health == 0) print("Die");
        }
    }
    public void Damage(int damage)
    {
        healthSystem.Damage(damage);
    }
}
