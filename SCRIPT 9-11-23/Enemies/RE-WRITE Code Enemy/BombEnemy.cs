using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SUMARY:
//1. Bomb Enemies don't have Bullet, they attack by go to Player

public class BombEnemy : EnemyBase
{
    private Vector3 playerRef;

    void OnEnable()
    {
        playerRef = PlayerController.instance.transform.position;

        moveSpeed = 10.0f;
        rotateSpeed = 3.0f;

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
        if (!beenToGatheringPoint)
        {
            // Kiểm tra khoảng cách đến gatheringPoint
            if (distanceToGatheringPoint > 4)
            {
                // Quay và di chuyển đến gatheringPoint
                Vector3 direction = (gatheringPoint - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
                transform.position += direction * Time.deltaTime * moveSpeed;
            }
            else
            {
                beenToGatheringPoint = true;
            }
        }
        else if (!beenToAttackPoint)
        {
            // Kiểm tra khoảng cách đến playerRef
            if (distanceToPlayer > 4)
            {
                // Quay và di chuyển đến player
                rotateAngleToPlayer = playerRef - transform.position;
                Quaternion targetAngle = Quaternion.LookRotation(rotateAngleToPlayer);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetAngle, rotateSpeed * Time.deltaTime);
                transform.position += rotateAngleToPlayer.normalized * Time.deltaTime * moveSpeed;
            }
            else
            {
                beenToAttackPoint = true;
                canAttack = true;
            }
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

