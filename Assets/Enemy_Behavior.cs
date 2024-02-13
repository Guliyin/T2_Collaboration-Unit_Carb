using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behavior : MonoBehaviour
{
    public Transform target; //目标物体
    public float moveSpeed = 5f; //速度
    public float destroyDelay = 0.1f; // 命中后延迟销毁的时间

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
                Destroy(gameObject,destroyDelay);
            }
            
        }
    }

}
