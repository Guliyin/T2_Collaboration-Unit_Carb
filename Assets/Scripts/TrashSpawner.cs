using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    [SerializeField] GameObject trash;
    [SerializeField] float StartDelay;
    [SerializeField] float RepeatTimer;

    void Start()
    {
        InvokeRepeating(nameof(SpawnTrash), StartDelay, RepeatTimer);
    }

    void SpawnTrash()
    {
        GameObject go = GameObjectPool.Instance.RequestCacheGameObejct(trash, 0);
        go.transform.position = transform.position;
    }
}
