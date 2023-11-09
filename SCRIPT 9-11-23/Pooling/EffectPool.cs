using System.Collections.Generic;
using UnityEngine;

public class EffectPool : Singleton<EffectPool>
{
    [System.Serializable]
    public struct EffectInfo //Tạo Struct chứa các biến value (ko phải reference) để có thể copy giá trị mỗi khi được gọi, ko ảnh hưởng đến biến. Ngoài ra, biến value này được lưu ở bộ nhớ Stack. 
    {
        public EnumforPooling.ObjectList effectType;
        public GameObject effectPrefab;
        public int effectAmount;
    }

    public EffectInfo[] effectInfos; //Tạo mảng của struct cho từng Effect
    public Dictionary<EnumforPooling.ObjectList, List<GameObject>> effectPools = new Dictionary<EnumforPooling.ObjectList, List<GameObject>>();

    void Start()
    {
        foreach (EffectInfo effectInfo in effectInfos)
        {
            effectPools[effectInfo.effectType] = new List<GameObject>();
            InitializeEffectPool(effectInfo);
        }
    }

    private void InitializeEffectPool(EffectInfo effectInfo)
    {
        for (int i = 0; i < effectInfo.effectAmount; i++)
        {
            GameObject obj = Instantiate(effectInfo.effectPrefab);
            obj.transform.parent = transform;
            obj.SetActive(false);
            effectPools[effectInfo.effectType].Add(obj);
        }
    }

    public GameObject GetEffectPool(EnumforPooling.ObjectList effectType)
    {
        if (effectPools.ContainsKey(effectType))
        {
            List<GameObject> pool = effectPools[effectType];
            foreach (GameObject obj in pool)
            {
                if (!obj.activeSelf)
                {
                    return obj;
                }
            }
        }

        // Tạo một đối tượng mới nếu không có trong Pool
        if (!effectPools.ContainsKey(effectType))
        {
            EffectInfo effectInfo = System.Array.Find(effectInfos, x => x.effectType == effectType);
            GameObject obj = Instantiate(effectInfo.effectPrefab);
            effectPools[effectType].Add(obj);
            obj.SetActive(false);
            return obj;
        }

        return null;
    }
}
