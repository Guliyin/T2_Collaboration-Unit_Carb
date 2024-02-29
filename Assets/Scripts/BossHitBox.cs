using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BossHitBox : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Tuple<int, Vector3> damageInfo = new Tuple<int, Vector3>(damage, transform.position);
            other.GetComponent<PlayerController>().Damage(damageInfo);
        }
    }
}
