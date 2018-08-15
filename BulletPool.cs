using UnityEngine;
using System.Collections.Generic;

public class BulletPool
{
    private static BulletPool _instance = new BulletPool();

    public static BulletPool Instance
    {
        get {
            if (_instance == null)
            {
                _instance = new BulletPool();
            }
            return _instance;
        }
    }

    Queue<GameObject> pool = new Queue<GameObject>();
    GameObject PoolRoot;

    public GameObject GetBullet()
    {
        GameObject result =null;

        if (pool.Count == 0)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/Bullet");

            if (prefab != null)
            {
                result = GameObject.Instantiate(prefab);
                result.SetActive(false);//false
                pool.Enqueue(result);
            }
            else
            {
                Debug.LogError("load >Prefabs/Bullet< failed!");
            }
        }
        else
        {
            result = pool.Dequeue();
        }

        //激活
        result.SetActive(true);
        return result;
    }

    public void ResetBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        PoolRoot = GameObject.Find("PoolRoot");
        if (PoolRoot == null)
        {
            PoolRoot = new GameObject("PoolRoot");
        }
        bullet.transform.SetParent(PoolRoot.transform);
        pool.Enqueue(bullet);
    }
}
