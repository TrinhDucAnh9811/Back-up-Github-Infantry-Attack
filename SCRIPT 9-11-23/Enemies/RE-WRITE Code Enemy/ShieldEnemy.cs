using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SUMARY:
//1. Shield Enemies don't have Bullet, they attack by shield/hand

public class ShieldEnemy : EnemyBase
{
    private Vector3 playerRef;

    void OnEnable()
    {
        playerRef = PlayerController.instance.transform.position;

        moveSpeed = 3.0f;
        rotateSpeed = 5.0f;

        attackSpeed = 2.0f;
        /*bulletSpeed = 10.0f;*/

        gatheringPoint_OffSet = new Vector3(Random.Range(-10, 10), 0, Random.Range(10, 20));

        gatheringPoint = playerRef + gatheringPoint_OffSet;
    }


    void Update()
    {
        distanceToGatheringPoint = (gatheringPoint - transform.position).magnitude; //Always check distance to gathering

        distanceToPlayer = (playerRef - transform.position).magnitude; //Always check distance to gathering Point

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
        //Nếu chưa đến gatheringPoint và distance > 4
        if (!beenToGatheringPoint)
        {
            if(distanceToGatheringPoint > 4)
            {
                //3 dòng đầu để quay:
                Vector3 direction = (gatheringPoint - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // Lerp giữa hướng hiện tại và hướng mới
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);


                //Move to gathering Point:
                transform.position += direction * Time.deltaTime * moveSpeed;
            }    
            else
            {
                beenToGatheringPoint = true;
            }    
      

        }


        //Check if it's reach the gathering Point or not:
        else if (!beenToAttackPoint)
        {
            if(distanceToGatheringPoint <= 4)
            {
                //Rotate to player direction:
                rotateAngleToPlayer = playerRef - transform.position;
                Quaternion targetAngle = Quaternion.LookRotation(rotateAngleToPlayer);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetAngle, rotateSpeed * Time.deltaTime);

                //Then move to player place:
                transform.position += rotateAngleToPlayer.normalized * Time.deltaTime * moveSpeed;
            }    
            else
            {
                beenToAttackPoint = true;
                canAttack = true;
            }    
          
        }

        else if (distanceToPlayer <= 4)
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

        //Code chạy Animation đánh ở đây:
        StartCoroutine(WaitForShoot(attackSpeed));

    }
}
