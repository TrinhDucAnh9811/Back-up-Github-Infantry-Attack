using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPool : Singleton<EnemyBulletPool>
{
    [System.Serializable]
    public struct EnemyBulletInfo //Tạo Struct chứa các biến value (ko phải reference) để có thể copy giá trị mỗi khi được gọi, ko ảnh hưởng đến biến. Ngoài ra, biến value này được lưu ở bộ nhớ Stack. 
    {
        public EnumForEnemyBullet.ObjectList enemyBulletType;
        public GameObject enemyBulletPrefab;
        public int bulletAmount;
    }

    public EnemyBulletInfo[] bulletInfos; //Tạo mảng của struct cho từng Enemy
    public Dictionary<EnumForEnemyBullet.ObjectList, List<GameObject>> enemyBulletPools = new Dictionary<EnumForEnemyBullet.ObjectList, List<GameObject>>();

    void Start()
    {
        foreach (EnemyBulletInfo bulletInfo in bulletInfos)
        {
            enemyBulletPools[bulletInfo.enemyBulletType] = new List<GameObject>();
            InitializeEnemyBulletPool(bulletInfo);
        }
    }

    private void InitializeEnemyBulletPool(EnemyBulletInfo bulletInfo)
    {
        for (int i = 0; i < bulletInfo.bulletAmount; i++)
        {
            GameObject obj = Instantiate(bulletInfo.enemyBulletPrefab);
            obj.transform.parent = transform;
            obj.SetActive(false);
            enemyBulletPools[bulletInfo.enemyBulletType].Add(obj);
        }
    }

    public GameObject GetEnemyBulletPool(EnumForEnemyBullet.ObjectList bulletInfo)
    {
        if (enemyBulletPools.ContainsKey(bulletInfo))
        {
            List<GameObject> pool = enemyBulletPools[bulletInfo];
            foreach (GameObject obj in pool)
            {
                if (!obj.activeSelf)
                {
                    return obj;
                }
            }
        }

        // Tạo một đối tượng mới nếu không có trong Pool
        if (enemyBulletPools.ContainsKey(bulletInfo))
        {
            EnemyBulletInfo effectInfo = System.Array.Find(bulletInfos, x => x.enemyBulletType == bulletInfo);
            GameObject obj = Instantiate(effectInfo.enemyBulletPrefab);
            enemyBulletPools[bulletInfo].Add(obj);
            obj.SetActive(false);
            return obj;
        }

        return null;
    }
}
