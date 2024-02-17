using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] Hole exit;
    [SerializeField] float coolDown = 3;
    SphereCollider sphereCollider;
    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        print("Why");
        Vector3[] fromToPos = { transform.position, exit.transform.position };
        CoolDown();
        exit.SendMessage(nameof(CoolDown));
        other.SendMessage("HoleEnter", fromToPos, SendMessageOptions.DontRequireReceiver);
    }

    void CoolDown()
    {
        StartCoroutine(DisableCollision());
    }

    IEnumerator DisableCollision()
    {
        sphereCollider.enabled = false;
        yield return new WaitForSeconds(coolDown);
        sphereCollider.enabled = true;
    }
}
