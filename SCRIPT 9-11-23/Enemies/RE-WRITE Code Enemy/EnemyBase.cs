using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class EnemyBase : MonoBehaviour
{

    protected float currentHp;
    protected float maxHp;
    protected float moveSpeed;
    protected float rotateSpeed;

    protected Vector3 gatheringPoint;
    protected Vector3 gatheringPoint_OffSet;
    protected Vector3 attackPoint;


    protected float bulletSpeed;
    protected float attackSpeed;

    protected float distanceToGatheringPoint;
    protected float distanceToPlayer;

    protected bool canAttack;
    protected bool beenToGatheringPoint; //Đã đến gather point chưa
    protected bool beenToAttackPoint; //Đã đến attack point chưa

    protected Vector3 rotateAngleToPlayer; //Góc quay tính toán cho việc: khi đến được gatherPoint sẽ quay về phía Player

    //Enemy Property:
   
    [HideInInspector] public static float troopEnemyDamage = 1.0f;
    [HideInInspector] public static float shieldEnemyDamage = 1.0f;
    [HideInInspector] public static float bombEnemyDamage = 10.0f;
    [HideInInspector] public static float suicideCarEnemyDamage = 15.0f;
    [HideInInspector] public static float droneEnemyDamage = 10.0f;
    [HideInInspector] public static float tankBulletdamage = 10.0f;


    public float troopDamage;
        

    //Delivered class must emplement this method:
    protected abstract void EnemyMovement();

    //Delivered class must emplement this method:
    protected abstract void EnemyAttack(float attackSpeed, float bulletSpeed);


    protected void DamagePlayer(float damage)
    {
        PlayerHealth.instance.TakeDamage(damage);
    }

    protected IEnumerator WaitForShoot(float time)
    {
        yield return new WaitForSeconds(time);
        canAttack = true;
    }

    protected IEnumerator WaitForAttack(float time)
    {
        yield return new WaitForSeconds(time);
        canAttack = true;
    }


}


