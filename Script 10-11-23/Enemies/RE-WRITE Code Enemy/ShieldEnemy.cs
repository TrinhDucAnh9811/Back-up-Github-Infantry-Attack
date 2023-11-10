using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//SUMARY:
//1. Shield Enemies don't have Bullet, they attack by shield/hand

public class ShieldEnemy : EnemyBase
{
    private Vector3 playerPos;

    private NavMeshAgent agent;

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();

        gatheringPoint = new Vector3(UnityEngine.Random.Range(-20, 20), 0, UnityEngine.Random.Range(10, 15));

        playerPos = PlayerController.instance.transform.position;
    
        rotateSpeed = 5.0f;

        attackSpeed = 2.0f;

        gatheringPoint = playerPos + gatheringPoint;

        //Set destination after initialize:
        agent.SetDestination(gatheringPoint);
    }


    void Update()
    {
        distanceToGatheringPoint = (gatheringPoint - transform.position).magnitude; //Always check distance to gatherPoint

        // Tính hướng quay đến người chơi + Gọi hàm RotateToWardsPlayer
        Vector3 angleToRotate = (playerPos - transform.position).normalized;
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
        //Nhận sát thương theo attackSpeed, fake animation đánh
        PlayerHealth.instance.TakeDamage(shieldEnemyDamage);

        canAttack = false;

 

    }
}
