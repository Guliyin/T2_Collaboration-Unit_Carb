using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerAttackTrigger : MonoBehaviour
{
    public Action hit;
    [SerializeField] GameObject bloodParticle;
    [SerializeField] GameObject numberText;
    private void OnTriggerEnter(Collider other)
    {
        GameObject blood = GameObjectPool.Instance.RequestCacheGameObejct(bloodParticle, 1f);
        blood.transform.position = other.ClosestPoint(transform.position);

        GameObject number = GameObjectPool.Instance.RequestCacheGameObejct(numberText, 1f);
        number.transform.position = other.ClosestPoint(transform.position);

        hit();
    }
}
