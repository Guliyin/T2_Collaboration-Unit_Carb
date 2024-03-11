using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    Slider volume;
    Slider sensitivity;
    Toggle growth;
    
    void Awake()
    {
        volume = transform.GetChild(0).Find("Volume").GetComponent<Slider>();
        sensitivity = transform.GetChild(0).Find("Sensitivity").GetComponent <Slider>();
        growth = transform.GetChild(0).Find("Toggle").GetComponent<Toggle>();

        volume.onValueChanged.AddListener(delegate { GameManager.Instance.ChangeVolume(volume.value); });
        sensitivity.onValueChanged.AddListener(delegate { GameManager.Instance.MouseSensitivity(sensitivity.value); });
        growth.onValueChanged.AddListener(delegate { GameManager.Instance.SetPlayerGrowth(growth.isOn); });
    }
    void OnEnable()
    {
        volume.value = GameManager.Instance.Volume;
        sensitivity.value = GameManager.Instance.Sensitivity;
        growth.isOn = GameManager.Instance.EnableGrowth;
    }
    private void OnDestroy()
    {
        volume.onValueChanged.RemoveListener(delegate { GameManager.Instance.ChangeVolume(volume.value); });
        sensitivity.onValueChanged.RemoveListener(delegate { GameManager.Instance.MouseSensitivity(sensitivity.value); });
        growth.onValueChanged.RemoveListener(delegate { GameManager.Instance.SetPlayerGrowth(growth.isOn); });
    }
}
