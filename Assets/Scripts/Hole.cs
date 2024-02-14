using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] Hole exit;
    SphereCollider sphereCollider;
    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Vector3[] fromToPos = { transform.position, exit.transform.position };
        StartCoroutine(Disable());
        other.SendMessage("HoleEnter", fromToPos, SendMessageOptions.DontRequireReceiver);
    }
    private void OnTriggerExit(Collider other)
    {
        other.SendMessage("HoleExit",SendMessageOptions.DontRequireReceiver);
    }
    IEnumerator Disable()
    {
        sphereCollider.enabled = false;
        yield return new WaitForSeconds(3);
        sphereCollider.enabled = true;
    }
}
