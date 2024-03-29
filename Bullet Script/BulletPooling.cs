using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : Singleton<BulletPooling>
{


    private List<GameObject> bulletPool = new List<GameObject>();

    private int poolAmount = 100;

    public GameObject bulletFrefab;

    public Transform firePoint;
    
    void Start()
    {
        //Initialize bullet:
        for(int i = 0; i< poolAmount; i++)
        {
            GameObject obj = Instantiate(bulletFrefab);
            obj.SetActive(false);
            bulletPool.Add(obj);
        }  
    }

    public GameObject GetPooledBullet()
    {
        for(int i =0; i < bulletPool.Count; i++)
        {
            if(!bulletPool[i].activeInHierarchy)
            {
                return bulletPool[i];
            }
        }
        return null;
    }
}
