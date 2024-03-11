using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    Vector3 StartPos;

    MeshRenderer m_Renderer;
    Collider m_Collider;
    ParticleSystem particle;

    [SerializeField] float amount;
    [SerializeField] float cd;
    private void Start()
    {
        m_Renderer = GetComponent<MeshRenderer>();
        m_Collider = GetComponent<Collider>();
        particle = GetComponentInChildren<ParticleSystem>();
        StartPos = transform.position;
    }
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, StartPos.y + Mathf.Sin(Time.time * 2f) * 0.25f, transform.position.z);
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * 75));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().Heal(amount);
            AudioManager.Instance.PlayAudio("Get_Heal");
            StartCoroutine(CoolDown());
        }
    }
    IEnumerator CoolDown()
    {
        particle.Stop();
        m_Renderer.enabled = false;
        m_Collider.enabled = false;
        yield return new WaitForSeconds(cd);
        particle.Play();
        m_Renderer.enabled = true;
        m_Collider.enabled = true;
    }
}
