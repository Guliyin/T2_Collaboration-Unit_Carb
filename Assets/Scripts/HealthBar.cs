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

    private void Awake()
    {
        healthBar = transform.Find("Fill").GetComponent<Image>();
        damageBar = transform.Find("DamageBar").GetComponent<Image>();
    }

    void Start()
    {
        damagedHealthShrinkTimer = DAMAGED_HEALTH_SHRINK_TIMER_MAX;
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
    }

    public void HealthSystem_OnDamaged(float amountNormalized)
    {
        damagedHealthShrinkTimer = DAMAGED_HEALTH_SHRINK_TIMER_MAX;
        SetHealth(amountNormalized);
    }

    public void HealthSystem_OnHealed(float amountNormalized)
    {
        if (healthBar.fillAmount >= damageBar.fillAmount)
        {
            damageBar.fillAmount = amountNormalized;
        }
        SetHealth(amountNormalized);
    }

    void SetHealth(float amountNormalized)
    {
        healthBar.fillAmount = amountNormalized;
    }

}
