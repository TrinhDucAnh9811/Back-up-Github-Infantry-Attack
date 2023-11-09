using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : Singleton<EnemyPool>
{
    [System.Serializable]
    public struct EnemyInfo //Tạo Struct chứa các biến value (ko phải reference) để có thể copy giá trị mỗi khi được gọi, ko ảnh hưởng đến biến. Ngoài ra, biến value này được lưu ở bộ nhớ Stack. 
    {
        public EnumForEnemyType.ObjectList enemyType;
        public GameObject enemyPrefab;
        public int enemyAmount;
    }

    public EnemyInfo[] enemyInfos; //Tạo mảng của struct cho từng Enemy
    public Dictionary<EnumForEnemyType.ObjectList, List<GameObject>> enemyPools = new Dictionary<EnumForEnemyType.ObjectList, List<GameObject>>();

    void Start()
    {
        foreach (EnemyInfo enemyInfo in enemyInfos)
        {
            enemyPools[enemyInfo.enemyType] = new List<GameObject>();
            InitializeEnemyPool(enemyInfo);
        }
    }

    private void InitializeEnemyPool(EnemyInfo enemyInfo)
    {
        for (int i = 0; i < enemyInfo.enemyAmount; i++)
        {
            GameObject obj = Instantiate(enemyInfo.enemyPrefab);
            obj.transform.parent = transform;
            obj.SetActive(false);
            enemyPools[enemyInfo.enemyType].Add(obj);
        }
    }

    public GameObject GetEnemyPool(EnumForEnemyType.ObjectList enemyType)
    {
        if (enemyPools.ContainsKey(enemyType))
        {
            List<GameObject> pool = enemyPools[enemyType];
            foreach (GameObject obj in pool)
            {
                if (!obj.activeSelf)
                {
                    return obj;
                }
            }
        }

        // Tạo một đối tượng mới nếu không có trong Pool
        if (enemyPools.ContainsKey(enemyType))
        {
            EnemyInfo effectInfo = System.Array.Find(enemyInfos, x => x.enemyType == enemyType);
            GameObject obj = Instantiate(effectInfo.enemyPrefab);
            enemyPools[enemyType].Add(obj);
            obj.SetActive(false);
            return obj;
        }

        return null;
    }
}
