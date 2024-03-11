using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialOne : MonoBehaviour
{
    bool activated;

    [SerializeField] MinionController levelOneMinion;
    [SerializeField] GameObject ui;

    private void Start()
    {
        levelOneMinion.Die += LevelOneFinish;
    }
    void LevelOneFinish(MinionController minion)
    {
        Time.timeScale = 0;
        ui.SetActive(true);
        ui.GetComponent<CanvasGroup>().DOFade(1, 0.3f).SetUpdate(true).OnComplete(() => AnimOver());
    }
    void AnimOver()
    {
        activated = true;
    }
    private void Update()
    {
        if (activated && Input.anyKeyDown)
        {
            GameManager.Instance.LoadLevelTwo();
            Time.timeScale = 1;
            activated = false;
            ui.GetComponent<CanvasGroup>().DOFade(0, 0.5f).SetUpdate(true).OnComplete(() => Close());
        }
    }
    void Close()
    {
        Destroy(GameObject.Find("Tutorial"));
        Destroy(gameObject, 0.5f);
    }
    private void OnDisable()
    {
        levelOneMinion.Die -= LevelOneFinish;
    }
}
