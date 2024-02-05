using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    const float DAMAGED_HEALTH_SHRINK_TIMER_MAX = 0.5f;
    float damagedHealthShrinkTimer;

    Image healthBar;
    Image damageBar;
    HealthSystem healthSystem;

    private void Awake()
    {
        healthBar = transform.Find("Fill").GetComponent<Image>();
        damageBar = transform.Find("DamageBar").GetComponent <Image>();
    }

    void Start()
    {
        healthSystem = new HealthSystem(1000);
        SetHealth(healthSystem.GetHealthNormalized());
        damagedHealthShrinkTimer = DAMAGED_HEALTH_SHRINK_TIMER_MAX;

        healthSystem.OnDamaged += HealthSystem_OnDamaged;
    }
    void Update()
    {
        damagedHealthShrinkTimer -= Time.deltaTime;
        if (damagedHealthShrinkTimer < 0)
        {
            if (healthBar.fillAmount < damageBar.fillAmount)
            {
                damageBar.fillAmount -= 0.5f * Time.deltaTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            healthSystem.Damage(10);
        }
    }

    void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        damagedHealthShrinkTimer = DAMAGED_HEALTH_SHRINK_TIMER_MAX;
        SetHealth(healthSystem.GetHealthNormalized());
    }

    void SetHealth(float healthNormalized)
    {
        healthBar.fillAmount = healthNormalized;
    }

}
