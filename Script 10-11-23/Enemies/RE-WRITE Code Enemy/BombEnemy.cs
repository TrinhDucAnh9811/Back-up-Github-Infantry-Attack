using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//SUMARY:
//1. Bomb Enemies don't have Bullet, they attack by go to Player

public class BombEnemy : EnemyBase
{
    private Vector3 playerPos;

    private NavMeshAgent agent;

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();

        gatheringPoint = new Vector3(UnityEngine.Random.Range(-20, 20), 0, UnityEngine.Random.Range(10, 15));

        playerPos = PlayerController.instance.transform.position;

        gatheringPoint = playerPos + gatheringPoint;

        rotateSpeed = 3.0f;

        //Set destination after initialize:
        agent.SetDestination(gatheringPoint);
    }


    void Update()
    {
        distanceToGatheringPoint = (gatheringPoint - transform.position).magnitude; //Always check distance to gatherPoint

        distanceToPlayer = (playerPos - transform.position).magnitude;

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
            agent.SetDestination(playerPos);                 
        }

        if(distanceToPlayer <=2)
        {
            canAttack = true;
            beenToAttackPoint = true;
        }
    }
   
    protected override void EnemyAttack(float attackSpeed, float bulletSpeed)
    {
        //Nhận sát thương theo attackSpeed, fake animation đánh
        PlayerHealth.instance.TakeDamage(bombEnemyDamage);

        //Code chạy EFFECT nổ ở đây: (QUAN TRỌNG: Pooling Effect)
        GameObject bombEffect = EffectPool.instance.GetEffectPool(EnumforPooling.ObjectList.EnemyBombExplode);
        if (bombEffect != null)
        {
            bombEffect.transform.position = transform.position;
            bombEffect.SetActive(true);
        }

        //Set Inactive and can't attack Player:
        
        gameObject.GetComponent<EnemyHealth>().TakeDamage(1000);

        canAttack = false;
    }
   
}

