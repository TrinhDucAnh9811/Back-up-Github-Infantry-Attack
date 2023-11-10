using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool
{
    public int spawnCount;

    public GameObject prefab;

    private List<GameObject> gameObjects = new List<GameObject>();
}

public class BulletPooling : Singleton<BulletPooling>
{
    //Machine Gun:
    private List<GameObject> bulletPool = new List<GameObject>();

    private int bulletPoolAmount = 100;

    public Transform firePointBullet;

    public GameObject bulletFrefab;

    //Rocket Launcher:
    private List<GameObject> rocketPool = new List<GameObject>();

    private int rocketPoolAmount = 50;

    public Transform firePointRocket;

    public GameObject rocketPrefab;


    void Start()
    {
        //Initialize Bullet:
        for(int i = 0; i< bulletPoolAmount; i++)
        {
            GameObject obj = Instantiate(bulletFrefab);
            obj.SetActive(false);
            bulletPool.Add(obj);
        }

        //Initialize Rocket:
        for (int i = 0; i < rocketPoolAmount; i++)
        {
            GameObject obj = Instantiate(rocketPrefab);
            obj.SetActive(false);
            rocketPool.Add(obj);
        }
    }

    public GameObject GetPooledBullet(EnumforPooling.ObjectList index)
    {
        switch(index)
        {
            case EnumforPooling.ObjectList.MachineGunEffect://Bullet
                {
                    for (int i = 0; i < bulletPool.Count; i++)
                    {
                        if (!bulletPool[i].activeInHierarchy)
                        {
                            return bulletPool[i];
                        }
                    }
                    return null;
                  
                }

            case EnumforPooling.ObjectList.RocketExplode: //Rocket
                {
                    for (int i = 0; i < rocketPool.Count; i++)
                    {
                        if (!rocketPool[i].activeInHierarchy)
                        {
                            return rocketPool[i];
                        }
                    }
                    return null;
                   
                }
            default: return null;

        }    
        


       
    }
}
