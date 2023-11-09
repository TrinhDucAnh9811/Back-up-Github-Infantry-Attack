using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : EnemyBase
{
    private bool hasExploded = false;
    private float radious = 10.0f;

    EnemyHealth enemyHealth;

    private void OnEnable()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        enemyHealth.OnEnemyDead += Explode;
    }

    protected override void EnemyAttack(float attackSpeed, float bulletSpeed)
    {
      
    }
    protected override void EnemyMovement()
    {
       
    }


    void Explode(EnemyHealth enemyHealth)
    {
        //Call Effect from pool:
        GameObject bombEffect = EffectPool.instance.GetEffectPool(EnumforPooling.ObjectList.EnemyBombExplode);
        if(bombEffect != null)
        {
            bombEffect.transform.position = transform.position;
            bombEffect.SetActive(true);  
        }

        //Cast Sphere to cause damage to other Enemy:
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10);

        foreach(Collider nearbyEnemies in colliders)
        {
            EnemyHealth enemies = nearbyEnemies.GetComponent<EnemyHealth>(); 
            if(enemies != null )
            {
                enemies.TakeDamage(WeaponStats.enemy_Bomb_Damage);
            }
        }
    }
}
