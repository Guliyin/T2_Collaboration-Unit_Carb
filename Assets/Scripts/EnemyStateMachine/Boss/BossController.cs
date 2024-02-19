using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] int healthMax = 1000;

    [SerializeField] HealthBar_Boss healthBar;

    NumericalSystem healthSystem;

    private void Awake()
    {
        healthSystem = new NumericalSystem(healthMax);
    }
    private void Start()
    {
        healthBar.Init(healthSystem.NormalizedAmount, healthMax);
        healthSystem.OnDamaged += OnDamaged;
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
} 
