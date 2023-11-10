using Knife.Effects;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class TroopEnemy : EnemyBase
{
    //Variables:
    private Vector3 playerPos;

    public GameObject troopBulletPrefab;

    public GameObject firePoint;

    private NavMeshAgent agent;

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();

        gatheringPoint = new Vector3(UnityEngine.Random.Range(-20, 20), 0, UnityEngine.Random.Range(10, 15));

        playerPos = PlayerController.instance.transform.position;

        rotateSpeed = 3.0f;

        attackSpeed = 2.0f;
        bulletSpeed = 10000.0f;

        gatheringPoint = playerPos + gatheringPoint;

        //Set destination after initialize:
        agent.SetDestination(gatheringPoint);
    }



    private void Update()
    {
        distanceToGatheringPoint = (gatheringPoint - transform.position).magnitude; //Always check distance to gatherPoint

        // Tính hướng quay đến người chơi + Gọi hàm RotateToWardsPlayer
        Vector3 angleToRotate = (playerPos - firePoint.transform.position).normalized;
        RotateTowardsPlayer(angleToRotate);

        if (gameObject.activeSelf && !beenToAttackPoint)
        {
            EnemyMovement();
        }

        else if (canAttack)
        {
            //Attack:
            EnemyAttack(attackSpeed, bulletSpeed);
            StartCoroutine(WaitForShoot(attackSpeed));
        }
    }


    void RotateTowardsPlayer(Vector3 direction)
    {
        // Tính góc quay
        Quaternion targetAngle = Quaternion.LookRotation(direction);

        // Lerp để quay từ góc hiện tại đến góc muốn quay
        transform.rotation = Quaternion.Lerp(transform.rotation, targetAngle, rotateSpeed * Time.deltaTime);
    }
    protected override void EnemyMovement()
    {
        if (distanceToGatheringPoint <= 2) 
        {
            canAttack = true;
            beenToAttackPoint = true;
        }    
    }


    protected override void EnemyAttack(float attackSpeed, float bulletSpeed)
    {
        GameObject effect = EffectPool.instance.GetEffectPool(EnumforPooling.ObjectList.EnemyBulletFire);
        if (effect != null)
        {
            effect.transform.position = firePoint.transform.position; 
            effect.transform.rotation = firePoint.transform.rotation; 
            effect.transform.localScale = Vector3.one * 3;
            effect.SetActive(true);
        }

        /*GameObject bullet = Instantiate(troopBulletPrefab, firePoint.transform.position, Quaternion.identity);*/

        GameObject bullet = EnemyBulletPool.instance.GetEnemyBulletPool(EnumForEnemyBullet.ObjectList.TroopEnemyBullet);
        if(bullet != null)
        {
            bullet.transform.position = firePoint.transform.position;
           /* bullet.transform.SetParent(transform, true);*/
            bullet.SetActive(true);

            Vector3 direction = (playerPos - firePoint.transform.position).normalized;
            bullet.GetComponent<Rigidbody>().velocity = direction * (Time.deltaTime * bulletSpeed);
        }

        canAttack = false;


    }
}



