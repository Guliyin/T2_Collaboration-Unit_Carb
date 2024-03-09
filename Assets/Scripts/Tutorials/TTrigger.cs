using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] gos;
    [SerializeField] GameObject ui;
    bool activated;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0;
            ui.SetActive(true);
            ui.GetComponent<CanvasGroup>().DOFade(1, 0.3f).SetUpdate(true).OnComplete(() => AnimOver());
            foreach (GameObject go in gos)
            {
                go.SetActive(false);
            }
        }
    }
    void AnimOver()
    {
        activated = true;
    }
    private void Update()
    {
        if (activated && Input.anyKeyDown)
        {
            Time.timeScale = 1;
            activated = false;
            ui.GetComponent<CanvasGroup>().DOFade(0, 0.5f).SetUpdate(true).OnComplete(() => Close());
        }
    }
    void Close()
    {
        ui.SetActive(false);
        gameObject.SetActive(false);
    }
}
