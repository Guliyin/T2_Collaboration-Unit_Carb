using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOne : MonoBehaviour
{
    bool activated;

    [SerializeField] MinionController levelOneMinion;
    [SerializeField] GameObject ui;

    private void Start()
    {
        levelOneMinion.Die += LevelOneFinish;
    }
    void LevelOneFinish()
    {
        activated = true;
        Time.timeScale = 0;
        ui.SetActive(true);
    }
    private void Update()
    {
        if (activated && Input.anyKeyDown)
        {
            GameManager.Instance.LoadLevelTwo();
            Time.timeScale = 1;
            activated = false;
            ui.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
