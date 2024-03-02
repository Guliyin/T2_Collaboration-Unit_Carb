using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Button[] startButtons;

    void OnEnable()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(startButtons[0].gameObject);

        startButtons[0].onClick.AddListener(GameManager.Instance.ResumeGame);
        startButtons[3].onClick.AddListener(GameManager.Instance.LoadMenu);
    }
    public void OpenSettings()
    {

    }
    public void CloseSetting()
    {

    }
    public void OpenControll()
    {
        
    }
    public void CloseControll()
    {

    }
    private void OnDisable()
    {
        startButtons[0].onClick.RemoveListener(GameManager.Instance.ResumeGame);
        startButtons[3].onClick.RemoveListener(GameManager.Instance.LoadMenu);
    }
}
