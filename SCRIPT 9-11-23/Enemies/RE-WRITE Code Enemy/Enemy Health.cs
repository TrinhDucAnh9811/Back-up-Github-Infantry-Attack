using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


//SUMARY:
//1. EnemyHealth attach to all Enemy Object
//2. Only Function: Update Health Bar when enemy get damage
public class EnemyHealth : MonoBehaviour, IDamageable
{
    //Create Event when get hit to annonce for script GetHitEffect:
    public Action OnGetHit;

    //Create Event when get dead to annonce for script WaveManager:
    public Action<EnemyHealth> OnEnemyDead;

    public Slider slider;

    public float maxHp = 100.0f;
    public float currentHp;

    public void TakeDamage(float damageAmount)
    {
        currentHp -= damageAmount;

        UpdateHealthBar(maxHp, currentHp);

        if (currentHp <= 0)
        {
            if(gameObject.CompareTag("Enemy"))
            {
                //Code chạy EFFECT nổ ở đây: (QUAN TRỌNG: Pooling Effect)
                GameObject bombEffect = EffectPool.instance.GetEffectPool(EnumforPooling.ObjectList.EnemyDestroy);
                if (bombEffect != null)
                {
                    bombEffect.transform.position = transform.position;
                    bombEffect.SetActive(true);
                }
            }
            else if(gameObject.CompareTag("HeavyEnemy"))
            {
                //Code chạy EFFECT nổ ở đây: (QUAN TRỌNG: Pooling Effect)
                GameObject bombEffect = EffectPool.instance.GetEffectPool(EnumforPooling.ObjectList.HeavyEnemyDestroy);
                if (bombEffect != null)
                {
                    bombEffect.transform.position = transform.position;
                    bombEffect.SetActive(true);
                }
            }
            

            gameObject.SetActive(false);

            OnEnemyDead?.Invoke(this); //Announce to WaveManager         
        }

        //Event annoucement:
        OnGetHit?.Invoke();

    }
    void OnEnable()
    {
        currentHp = maxHp;
    }

    public void UpdateHealthBar(float maxHP, float currentHP)
    {
        if (gameObject != null)
        {
            if (slider == null)
            {
                Debug.LogWarning(gameObject.name);
                return;
            }

            //Update slider:
            slider.value = currentHP / maxHP;
        }
    }




}
