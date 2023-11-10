using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SuicideCarEnemy : EnemyBase
{
    private Vector3 playerPos;
    private NavMeshAgent agent;

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();

        gatheringPoint = new Vector3(UnityEngine.Random.Range(-20, 20), 0, UnityEngine.Random.Range(10, 15));

        playerPos = PlayerController.instance.transform.position;

        rotateSpeed = 3.0f;

        //Set destination after initialize:
        agent.SetDestination(playerPos);

    }

    private void Update()
    {     
        distanceToPlayer = (playerPos - transform.position).magnitude;
        if (distanceToPlayer <=7)
        {
            EnemyAttack(attackSpeed, bulletSpeed);
        }
    }


    protected override void EnemyAttack(float attackSpeed, float bulletSpeed)
    {
        GameObject effect = EffectPool.instance.GetEffectPool(EnumforPooling.ObjectList.SuicideCarExplode);
        if (effect != null)
        {
            effect.transform.position = transform.position;
            effect.transform.rotation = transform.rotation;
            effect.transform.localScale = Vector3.one * 6;
            effect.SetActive(true);
        }

        PlayerHealth.instance.TakeDamage(suicideCarEnemyDamage);
        gameObject.SetActive(false);
    }

    protected override void EnemyMovement()
    {

    }
}


