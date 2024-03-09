using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar_Boss : MonoBehaviour
{
    const float BAR_WIDTH = 1300f;

    Image healthBar;
    TMP_Text healthText;
    
    Transform damagedBarTemplate;

    int maxHealth;

    private void Awake()
    {
        healthBar = transform.Find("Fill").GetComponent<Image>();
        healthText = transform.Find("HealthNumText").GetComponent<TMP_Text>();
        damagedBarTemplate = transform.Find("damagedBarTemplate");
    }

    public void Init(float initHealthNormalized,int maxHealth)
    {
        this.maxHealth = maxHealth;
        SetHealth(initHealthNormalized);
    }

    public void HealthSystem_OnDamaged(float health)
    {
        float beforeDamaged = healthBar.fillAmount;
        SetHealth((float)health / maxHealth);
        Transform go = Instantiate(damagedBarTemplate, transform);
        go.gameObject.SetActive(true);
        RectTransform damagedBar = go.GetComponent<RectTransform>();
        damagedBar.anchoredPosition = new Vector2(healthBar.fillAmount * BAR_WIDTH, damagedBar.anchoredPosition.y);
        damagedBar.GetComponent<Image>().fillAmount = beforeDamaged - healthBar.fillAmount;
        damagedBar.DOAnchorPosY(-20, 0.3f);
        damagedBar.GetComponent<Image>().DOFade(0, 0.3f);
        Destroy(damagedBar.gameObject, 0.4f);

        healthText.text = health + "/" + maxHealth;
    }
    public void HealthSystem_OnHealed(float health)
    {
        SetHealth((float)health / maxHealth);
        healthText.text = health + "/" + maxHealth;
    }

    void SetHealth(float healthNormalized)
    {
        healthBar.fillAmount = healthNormalized;
    }
}
