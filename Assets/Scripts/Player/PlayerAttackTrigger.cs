using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerAttackTrigger : MonoBehaviour
{
    [SerializeField] int damage;

    public Action hitEnemy;
    [SerializeField] GameObject bloodParticle;
    [SerializeField] GameObject numberText;

    List<GameObject> hitObjects = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (hitObjects.Contains(other.gameObject)) return;
            hitObjects.Add(other.gameObject);
            int damageFinal;
            if (GameManager.Instance.EnableGrowth)
            {
                damageFinal = damage + GameManager.Instance.DamageBounus;
            }
            else
            {
                damageFinal = damage;
            }

            Vector3 point = other.ClosestPoint(transform.position);

            GameObject blood = GameObjectPool.Instance.RequestCacheGameObejct(bloodParticle, 1f);
            blood.transform.position = point;

            GameObject number = GameObjectPool.Instance.RequestCacheGameObejct(numberText, 1f);
            number.transform.position = point;
            number.GetComponentInChildren<DamagePopup>().Init(damageFinal);

            other.SendMessageUpwards("Damage", damageFinal, SendMessageOptions.DontRequireReceiver);

            hitEnemy();

            AudioManager.Instance.PlayAudio(new string[] {"Enemy_Get_Hit_1", "Enemy_Get_Hit_2" });
        }
    }
    public void ClearColliderCache()
    {
        hitObjects.Clear();
    }
}
