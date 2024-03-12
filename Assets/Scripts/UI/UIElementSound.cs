using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIElementSound : MonoBehaviour
{
    public bool audioPlayed;
    EventSystem eventSystem;
    Button button;

    private void Start()
    {
        TryGetComponent(out button);
        if (button != null)
        {
            button.onClick.AddListener(ButtonClicked);
        }
        eventSystem = EventSystem.current;
    }
    public void ButtonClicked()
    {
        AudioManager.Instance.PlayAudio("UI_Sound_Confirm");
    }
    private void Update()
    {
        if (eventSystem.currentSelectedGameObject == gameObject)
        {
            if (!audioPlayed)
            {
                AudioManager.Instance.PlayAudio("UI_Sound_Select");
                var uis = FindObjectsOfType<UIElementSound>();
                foreach (var ui in uis)
                {
                    ui.audioPlayed = false;
                }
                audioPlayed = true;
            }
        }
    }
}
