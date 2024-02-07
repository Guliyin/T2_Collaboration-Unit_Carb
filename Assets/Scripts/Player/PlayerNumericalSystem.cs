using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNumericalSystem : MonoBehaviour
{
    [Header("µ÷ÊÔÑ¡Ïî")]
    [SerializeField] int MaxHealth;
    [SerializeField] int MaxStamina;
    [SerializeField] float autoRestoreStaminaDelayTimer;
    [SerializeField] float autoRestoreStaminaPunishTimer;
    [SerializeField] float autoRestoreStaminaSpeed;
    [Space]

    [SerializeField] HealthBar healthBar;
    [SerializeField] HealthBar staminaBar;

    NumericalSystem playerHealth;
    NumericalSystem playerStamina;
    public bool HasStamina => playerStamina.Amount > 0;

    float restoreTimer;

    void Start()
    {

        playerHealth = new NumericalSystem(MaxHealth);
        playerStamina = new NumericalSystem(MaxStamina);
    }
    private void Update()
    {
        restoreTimer -= Time.deltaTime;
        if (restoreTimer <= 0 && playerStamina.NormalizedAmount < 1)
        {
            RestoreStamina(Time.deltaTime * autoRestoreStaminaSpeed);
        }
    }
    public void Damage(int amount)
    {
        playerHealth.Damage(amount);
    }
    public void Heal(int amount)
    {
        playerHealth.Heal(amount);
    }
    public void DeductStamina(float amount)
    {
        
        playerStamina.Damage(amount);

        if (HasStamina) restoreTimer = autoRestoreStaminaDelayTimer;
        else restoreTimer = autoRestoreStaminaDelayTimer + autoRestoreStaminaPunishTimer;

        staminaBar.HealthSystem_OnDamaged(playerStamina.NormalizedAmount);
    }
    public void RestoreStamina(float amount)
    {
        playerStamina.Heal(amount);
        staminaBar.HealthSystem_OnHealed(playerStamina.NormalizedAmount);
    }
}
