using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Button[] startButtons;
    [SerializeField] GameObject settingStartPoint;
    [SerializeField] GameObject controllStartPoint;

    void OnEnable()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(startButtons[0].gameObject);

        startButtons[0].onClick.AddListener(GameManager.Instance.ResumeGame);
        startButtons[3].onClick.AddListener(GameManager.Instance.LoadMenu);
    }
    public void OpenSettings()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(settingStartPoint);
    }
    public void OpenControll()
    {
        var eventSystem = EventSystem.current;ss
        eventSystem.SetSelectedGameObject(controllStartPoint);
    }
    public void BackToMenu()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(startButtons[0].gameObject);
    }
    private void OnDisable()
    {
        startButtons[0].onClick.RemoveListener(GameManager.Instance.ResumeGame);
        startButtons[3].onClick.RemoveListener(GameManager.Instance.LoadMenu);
    }
}
