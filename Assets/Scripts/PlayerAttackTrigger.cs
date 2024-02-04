using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerAttackTrigger : MonoBehaviour
{
    public Action hit;
    [SerializeField] GameObject blood;
    private void OnTriggerEnter(Collider other)
    {
        GameObject go = GameObjectPool.Instance.RequestCacheGameObejct(blood, 1f);
        go.transform.position = other.ClosestPoint(transform.position);
        hit();
    }
}
