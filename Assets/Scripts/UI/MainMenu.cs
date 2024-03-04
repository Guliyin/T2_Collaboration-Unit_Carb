using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button[] startButtons;
    [SerializeField] GameObject settingStartPos;
    [SerializeField] GameObject creditStartPos;

    private void Start()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(startButtons[0].gameObject);

        startButtons[0].onClick.AddListener(StartGame);
        startButtons[3].onClick.AddListener(Quit);
    }
    public void SetFocus(GameObject go)
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(go);
    }
    public void StartGame()
    {
        GameManager.Instance.LoadLevelOne();
    }
    public void OpenSettings()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(settingStartPos);
    }
    public void CloseSettings()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(startButtons[1].gameObject);
    }
    public void OpenCredit()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(creditStartPos);
    }
    public void CloseCredit()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(startButtons[2].gameObject);
    }
    public void Quit()
    {
        GameManager.Instance.Quit();
    }
    private void OnDestroy()
    {
        startButtons[0].onClick.RemoveListener(StartGame);
        startButtons[3].onClick.RemoveListener(Quit);
    }
}
