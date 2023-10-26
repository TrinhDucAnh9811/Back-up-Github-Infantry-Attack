using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EffectPool : Singleton<EffectPool>
{
    private List<GameObject> effectPool = new List<GameObject>();

    private int effectAmount = 100;

    public GameObject gunMuzzleEffect;
    void Start()
    {
      for(int i=0; i < effectAmount; i++)
        {
            GameObject obj = Instantiate(gunMuzzleEffect);
            obj.SetActive(false);
            effectPool.Add(obj);
        }
            
    }

    public GameObject GetEffectPool()
    {
        for(int i =0; i < effectPool.Count; i++)
        {
            if (!effectPool[i].activeInHierarchy)
            {
                return effectPool[i];
            }
        }
        return null;
    }
  
}
