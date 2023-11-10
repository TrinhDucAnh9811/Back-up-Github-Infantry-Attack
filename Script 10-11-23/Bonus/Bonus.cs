using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    private Animator animator;

    private EnemyHealth enemyHealth;

    private float timer;
    private float duration;

    void OnEnable()
    {
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();

        enemyHealth.OnBonusGetHit += BonusGetHitAnim;
        enemyHealth.OnBonusDestroy += BonusDestroyAnim;

        animator.SetBool("isSpinning", true);
    }


    void BonusGetHitAnim(EnemyHealth enemyHealth)
    {
            animator.SetTrigger("isHitting");
    }

    void BonusDestroyAnim(EnemyHealth enemyHealth)
    {
        WeaponStats.fireRate -= (WeaponStats.fireRate * 0.2f);
        if(WeaponStats.fireRate <= 0.05) 
        {
            WeaponStats.fireRate = 0.05f;
        }
    }    

    

}
