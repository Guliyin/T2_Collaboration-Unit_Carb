using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash_Behavior : MonoBehaviour
{
    Transform target; //目标物体
    public float moveSpeed = 5f; //速度
    public float destroyDelay = 0.1f; // 命中后延迟销毁的时间

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;

            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime * moveSpeed);

            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.CompareTag("Player"))
            {
                //DamagePlayer
                print("DealDamageToPlayer");
                target.GetComponent<PlayerController>().UnlockTarget();
                GameObjectPool.Instance.ReturnCacheGameObejct(gameObject);
            }
            
        }
    }
    public void Damage(int damage)
    {
        print("HIT");
        target.GetComponent<PlayerController>().UnlockTarget();
        GameObjectPool.Instance.ReturnCacheGameObejct(gameObject);
    }

}
