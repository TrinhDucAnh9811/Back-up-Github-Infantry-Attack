using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class TankEnemy : EnemyBase
{
    private Vector3 playerRef;

    public GameObject tankBulletPrefab;

    public GameObject firePoint;

    private void OnEnable()
    {
        playerRef = PlayerController.instance.transform.position;

        moveSpeed = 5.0f;
        rotateSpeed = 3.0f;

        attackSpeed = 3.0f;
        bulletSpeed = 6000.0f;

        gatheringPoint_OffSet = new Vector3(Random.Range(-15, 15), 0, 25);

        gatheringPoint = playerRef + gatheringPoint_OffSet; //Make sure Enemy will gather in front of Player 25 unit
    }


    private void Update()
    {
        distanceToGatheringPoint = (gatheringPoint - transform.position).magnitude; //Always check distance to gathering Point

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
        else if (distanceToGatheringPoint <= 2 && !beenToGatheringPoint)
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
        GameObject bullet = Instantiate(tankBulletPrefab, firePoint.transform.position, Quaternion.identity);

        Vector3 direction = (playerRef - transform.position).normalized;
        bullet.transform.rotation = Quaternion.LookRotation(direction); // Đảm bảo góc quay viên đạn đúng hướng player

        bullet.GetComponent<Rigidbody>().velocity = direction * (Time.deltaTime * bulletSpeed);

        //Call effect:
        GameObject effect = EffectPool.instance.GetEffectPool(EnumforPooling.ObjectList.EnemyTankFire);
        if (effect != null)
        {
            effect.transform.position = firePoint.transform.position;
            effect.transform.localScale = Vector3.one * 4;
            effect.SetActive(true);
        }

        canAttack = false;

        StartCoroutine(WaitForShoot(attackSpeed));

    }
}



