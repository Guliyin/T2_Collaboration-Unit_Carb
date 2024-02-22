using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    MeshRenderer m_Renderer;
    BoxCollider boxCollider;

    [SerializeField] float amount;
    [SerializeField] float cd;
    private void Start()
    {
        m_Renderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().Heal(amount);
            StartCoroutine(CoolDown());
        }
    }
    IEnumerator CoolDown()
    {
        m_Renderer.enabled = false;
        boxCollider.enabled = false;
        yield return new WaitForSeconds(cd);
        m_Renderer.enabled = true;
        boxCollider.enabled = true;
    }
}
