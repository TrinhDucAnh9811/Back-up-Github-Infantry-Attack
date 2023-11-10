using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


//SUMARY:
//1. EnemyHealth attach to all Enemy Object
//2. Only Function: Update Health Bar when enemy get damage
public class EnemyHealth : MonoBehaviour, IDamageable
{
    //Create Event when get hit to anounce for script GetHitEffect:
    public Action OnGetHit;

    //Create Event when get dead to anounce for script WaveManager:
    public Action<EnemyHealth> OnEnemyDead;

    //Create Event when BONUS get hit to anounce for Bonus Script
    public Action<EnemyHealth> OnBonusGetHit;
    public Action<EnemyHealth> OnBonusDestroy;

    public Slider slider;

    public float maxHp = 100.0f;
    public float currentHp;

    private NavMeshAgent agent;
    private float timer;
    private float stunTime = 0.5f;
    private bool stun;

    public void TakeDamage(float damageAmount)
    {
        stun = true;

        currentHp -= damageAmount;

        UpdateHealthBar(maxHp, currentHp);

        if(currentHp >0)
        {
            if(gameObject.CompareTag("BonusFireRate"))
            {
                OnBonusGetHit?.Invoke(this);
            }
        }


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

                OnEnemyDead?.Invoke(this); //Announce to WaveManager         
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

                OnEnemyDead?.Invoke(this); //Announce to WaveManager      
            }

            else if(gameObject.CompareTag("BonusFireRate"))
            {
                //Code chạy EFFECT nổ ở đây: (QUAN TRỌNG: Pooling Effect)
                GameObject bombEffect = EffectPool.instance.GetEffectPool(EnumforPooling.ObjectList.BonusDestroy);
                if (bombEffect != null)
                {
                    bombEffect.transform.position = transform.position;
                    bombEffect.SetActive(true);
                    //KHONG ANNOUNCE TO WAVE MANAGER, Announce to Bonus script
                    OnBonusDestroy?.Invoke(this);
                }
                 
             
            }


            gameObject.SetActive(false);

          
        }

        //Event annoucement:
        OnGetHit?.Invoke();

    }
    void OnEnable()
    {
        currentHp = maxHp;

        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(stun ==true)
        {
          
            agent.speed = 0.1f;
            timer += Time.deltaTime;
            {
                if (timer >= stunTime)
                {
                    agent.speed = 5.0f;
                    stun = false;
                    timer = 0;

                }
            }
        }

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
