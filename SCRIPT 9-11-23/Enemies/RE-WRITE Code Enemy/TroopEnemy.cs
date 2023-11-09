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
        gatheringPoint = new Vector3(UnityEngine.Random.Range(-20, 20), 0, UnityEngine.Random.Range(10, 15));

        playerPos = PlayerController.instance.transform.position;

        rotateSpeed = 3.0f;

        attackSpeed = 2.0f;
        bulletSpeed = 10000.0f;

        gatheringPoint = playerPos + gatheringPoint; //Make sure Enemy will gather in front of Player 15 unit

        //Set destination after initialize:
        agent.SetDestination(gatheringPoint);
    }



    private void Update()
    {
        distanceToGatheringPoint = (gatheringPoint - transform.position).magnitude; //Always check distance to gathering Point
        //isdead gethit crowncontrol ->return;
        if (gameObject.activeSelf)
        {
            EnemyMovement();
        }

        if (canAttack)
        {      
            EnemyAttack(attackSpeed, bulletSpeed);        
        }
    }

    protected override void EnemyMovement()
    {
        if (distanceToGatheringPoint > 2)
        {
            //3 dòng đầu để quay:
            Vector3 direction = (gatheringPoint - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Lerp giữa hướng hiện tại và hướng mới
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);


            //Move to gathering Point:
            transform.position += direction * Time.deltaTime * moveSpeed;
            

        }


        //Check if it's reach the gathering Point or not:
        else if (distanceToGatheringPoint <= 2 && !beenToGatheringPoint) //CHECK LAI DIEU KIEN (***)
        {
            beenToGatheringPoint = true;

            rotateAngleToPlayer = playerRef - transform.position;
            Quaternion targetAngle = Quaternion.LookRotation(rotateAngleToPlayer);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetAngle, rotateSpeed * Time.deltaTime);

            //Delay for rotation to attack:
            StartCoroutine(WaitForAttack(1.5f));

        }

        else
        {
            rotateAngleToPlayer = playerRef - firePoint.transform.position;
            Quaternion targetAngle = Quaternion.LookRotation(rotateAngleToPlayer);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetAngle, rotateSpeed * Time.deltaTime);

     
        }

    }


    protected override void EnemyAttack(float attackSpeed, float bulletSpeed)
    {
        GameObject effect = EffectPool.instance.GetEffectPool(EnumforPooling.ObjectList.EnemyBulletFire);
        if (effect != null)
        {
            effect.transform.position = firePoint.transform.position; //VỀ SAU ĐỔI THÀNH FIREPOINT
            effect.transform.rotation = Quaternion.Euler(-90, 0, 90); //VỀ SAU ĐỔI THÀNH FIREPOINT
            effect.transform.localScale = Vector3.one * 3;
            effect.SetActive(true);
        }

        GameObject bullet = Instantiate(troopBulletPrefab, firePoint.transform.position, Quaternion.identity);

        Vector3 direction = (playerRef - firePoint.transform.position).normalized;
        bullet.GetComponent<Rigidbody>().velocity = direction * (Time.deltaTime * bulletSpeed);

      

        canAttack = false;

        StartCoroutine(WaitForShoot(attackSpeed));

    }
}



