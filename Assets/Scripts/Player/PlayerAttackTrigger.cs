using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerAttackTrigger : MonoBehaviour
{
    [SerializeField] int Damage;

    public Action hitEnemy;
    [SerializeField] GameObject bloodParticle;
    [SerializeField] GameObject numberText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject blood = GameObjectPool.Instance.RequestCacheGameObejct(bloodParticle, 1f);
            blood.transform.position = other.ClosestPoint(transform.position);

            GameObject number = GameObjectPool.Instance.RequestCacheGameObejct(numberText, 1f);
            number.transform.position = other.ClosestPoint(transform.position);

            if (other.GetComponentInParent<BossController>())
            {
                other.GetComponentInParent<BossController>().Damage(Damage);
            }

            hitEnemy();
        }
    }
}
