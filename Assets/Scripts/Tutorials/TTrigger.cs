using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] gos;
    [SerializeField] GameObject ui;
    bool activated;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            activated = true;
            ui.SetActive(true);
            foreach (GameObject go in gos)
            {
                go.SetActive(false);
            }
        }
    }
    private void Update()
    {
        if (activated && Input.anyKeyDown)
        {
            Time.timeScale = 1;
            activated = false;
            ui.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
