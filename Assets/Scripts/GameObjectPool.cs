using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoSingleton<GameObjectPool>
{
    private GameObject CachePanel;

    private Dictionary<string, Queue<GameObject>> m_Pool = new Dictionary<string, Queue<GameObject>>();

    private Dictionary<GameObject, string> m_GoTag = new Dictionary<GameObject, string>();


    /// <summary>
    /// 清空缓存池，释放所有引用
    /// </summary>
    public void ClearCachePool()
    {
        m_Pool.Clear();
        m_GoTag.Clear();
    }

    /// <summary>
    /// 回收GameObject
    /// </summary>
    public void ReturnCacheGameObejct(GameObject go)
    {
        if (CachePanel == null)
        {
            CachePanel = new GameObject();
            CachePanel.name = "CachePanel";
            DontDestroyOnLoad(CachePanel);
        }

        if (go == null)
        {
            return;
        }

        go.transform.parent = CachePanel.transform;
        /////////////////////////////////////////////////
        go.SetActive(false);

        if (m_GoTag.ContainsKey(go))
        {
            string tag = m_GoTag[go];
            RemoveOutMark(go);

            if (!m_Pool.ContainsKey(tag))
            {
                m_Pool[tag] = new Queue<GameObject>();
            }

            m_Pool[tag].Enqueue(go);
        }
    }

    /// <summary>
    /// 请求GameObject
    /// </summary>
    /// <param name="prefab">生成的物体</param>
    /// <param name="time">自动回收的时间，如果时间小于等于0则不会自动回收</param>
    /// <returns></returns>
    public GameObject RequestCacheGameObejct(GameObject prefab, float time)
    {
        string tag = prefab.GetInstanceID().ToString();
        GameObject go = GetFromPool(tag);
        if (go == null)
        {
            go = Instantiate(prefab);
            go.name = prefab.name + Time.time;
        }


        MarkAsOut(go, tag);

        if (time > 0)
        {
            CollectObject(go, time);
        }

        return go;
    }
    private void CollectObject(GameObject go, float delay = 0)
    {
        //延迟调用
        StartCoroutine(CollectObjectDelay(go, delay));
    }

    private IEnumerator CollectObjectDelay(GameObject go, float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnCacheGameObejct(go);
    }


    private GameObject GetFromPool(string tag)
    {
        if (m_Pool.ContainsKey(tag) && m_Pool[tag].Count > 0)
        {
            GameObject obj = m_Pool[tag].Dequeue();
            /////////////////////////////////////////////////
            obj.SetActive(true);
            return obj;
        }
        else
        {
            return null;
        }
    }


    private void MarkAsOut(GameObject go, string tag)
    {
        m_GoTag.Add(go, tag);
    }

    private void RemoveOutMark(GameObject go)
    {
        if (m_GoTag.ContainsKey(go))
        {
            m_GoTag.Remove(go);
        }
        else
        {
            Debug.LogError("remove out mark error, gameObject has not been marked");
        }
    }

}
