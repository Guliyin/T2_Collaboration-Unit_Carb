using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTreadMark : MonoBehaviour
{
    [SerializeField] GameObject go;
    private void OnEnable()
    {
        GameObject mark = GameObjectPool.Instance.RequestCacheGameObejct(go, 2);
        mark.transform.position = transform.position;
    }
}
