using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlayerDiedUI : MonoBehaviour
{
    CanvasGroup group;
    TMP_Text statusText;

    void Start()
    {
        group = GetComponent<CanvasGroup>();
        statusText = transform.Find("StatusText").GetComponent<TMP_Text>();
        EventCenter.AddListener(FunctionType.PlayerDead, ShowUI);
        EventCenter.AddListener(FunctionType.RestartLevel, HideUI);
    }
    void ShowUI()
    {
        statusText.text =   "Amount of damage done: " + GameManager.Instance.DamageDealtOnce + "\n" +
                            "Total amount of damage done: " + GameManager.Instance.DamageDealtAll + "\n" +
                            "Attack bonus: +" + (int)(GameManager.Instance.DamageDealtAll / 100f);
        group.DOFade(1, 0.7f);
    }
    void HideUI()
    {
        group.alpha = 0;
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(FunctionType.PlayerDead, ShowUI);
        EventCenter.RemoveListener(FunctionType.RestartLevel, HideUI);
    }
}
