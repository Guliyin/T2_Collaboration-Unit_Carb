using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlayerWinUI : MonoBehaviour
{
    CanvasGroup group;
    TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        group = GetComponent<CanvasGroup>();
        text = transform.Find("Text").GetComponent<TMP_Text>();
        EventCenter.AddListener(FunctionType.PlayerWin, ShowUI);
    }

    void ShowUI()
    {
        text.text = "You died " + GameManager.Instance.DeathCount + " times\n" +
                    "spent " + (int)(GameManager.Instance.GameTime / 60) + "m " + (int)(GameManager.Instance.GameTime % 60) + "s";
        group.DOFade(1, 0.7f);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(FunctionType.PlayerWin, ShowUI);
    }
}
