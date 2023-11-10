using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class TankEnemy : EnemyBase
{
    private Vector3 playerPos;

    public GameObject tankBulletPrefab;

    public GameObject tankHeadPrefab;

    public GameObject firePoint;

    private NavMeshAgent agent;

    private Vector3 angleToRotate;

    Tween tween;

    private void OnEnable()
    {

        agent = GetComponent<NavMeshAgent>();

        gatheringPoint = new Vector3(UnityEngine.Random.Range(-20, 20), 0, UnityEngine.Random.Range(34, 45));

        playerPos = PlayerController.instance.transform.position;

        rotateSpeed = 3.0f;

        attackSpeed = 3.0f;

        bulletSpeed = 6000.0f;

        gatheringPoint = playerPos + gatheringPoint;

        //Set destination after initialize:
        agent.SetDestination(gatheringPoint);
    }


    private void Update()
    {
        distanceToGatheringPoint = (gatheringPoint - transform.position).magnitude; //Always check distance to gatherPoint

        // Tính hướng quay đến người chơi + Gọi hàm RotateToWardsPlayer
        angleToRotate = (playerPos - firePoint.transform.position).normalized;
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
        Quaternion targetAngle = Quaternion.LookRotation(angleToRotate);

        // Lerp để quay từ góc hiện tại đến góc muốn quay
        tankHeadPrefab.transform.rotation = Quaternion.Lerp(tankHeadPrefab.transform.rotation, targetAngle , rotateSpeed * Time.deltaTime);
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

        GameObject bullet = Instantiate(tankBulletPrefab, firePoint.transform.position, Quaternion.identity);

        Vector3 direction = (playerPos - transform.position).normalized;
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


       

    }
}



