using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar_Boss : MonoBehaviour
{
    const float BAR_WIDTH = 1000f;

    Image healthBar;
    Transform damagedBarTemplate;
    HealthSystem healthSystem;

    private void Awake()
    {
        healthBar = transform.Find("Fill").GetComponent<Image>();
        damagedBarTemplate = transform.Find("damagedBarTemplate");
    }

    void Start()
    {
        healthSystem = new HealthSystem(1000);
        SetHealth(healthSystem.GetHealthNormalized());

        healthSystem.OnDamaged += HealthSystem_OnDamaged;
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            healthSystem.Damage(10);
        }
    }

    void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        float beforeDamaged = healthBar.fillAmount;
        SetHealth(healthSystem.GetHealthNormalized());
        Transform go = Instantiate(damagedBarTemplate, transform);
        go.gameObject.SetActive(true);
        RectTransform damagedBar = go.GetComponent<RectTransform>();
        damagedBar.anchoredPosition = new Vector2(healthBar.fillAmount * BAR_WIDTH, damagedBar.anchoredPosition.y);
        damagedBar.GetComponent<Image>().fillAmount = beforeDamaged - healthBar.fillAmount;

        damagedBar.DOAnchorPosY(-20, 0.3f);
        damagedBar.GetComponent<Image>().DOFade(0, 0.3f);
        Destroy(damagedBar.gameObject, 0.4f);
    }

    void SetHealth(float healthNormalized)
    {
        healthBar.fillAmount = healthNormalized;
    }
}
