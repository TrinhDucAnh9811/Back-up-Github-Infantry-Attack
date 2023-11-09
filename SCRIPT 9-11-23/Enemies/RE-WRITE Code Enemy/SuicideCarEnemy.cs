using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideCarEnemy : EnemyBase
{
    private Vector3 playerRef;

    private float original_Y_Axis;
    void OnEnable()
    {
        playerRef = PlayerController.instance.transform.position;

        moveSpeed = 10.0f;
        rotateSpeed = 3.0f;

        gatheringPoint_OffSet = playerRef;

        original_Y_Axis = transform.localPosition.y;

    }

    private void Update()
    {
        Vector3 direction = (playerRef - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        targetRotation.y = original_Y_Axis; //Cố định trục y của xe khi di chuyển, tránh xe bị dịch chuyển lên trên dần

        // Lerp giữa hướng hiện tại và hướng mới
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        transform.position += direction * moveSpeed * Time.deltaTime;
    }


    protected override void EnemyAttack(float attackSpeed, float bulletSpeed)
    {

    }

    protected override void EnemyMovement()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth.instance.TakeDamage(suicideCarEnemyDamage);

            //EFFECT NỔ Ở ĐÂY (EFFECT POOLING)
            GameObject bombEffect = EffectPool.instance.GetEffectPool(EnumforPooling.ObjectList.SuicideCarExplode);
            if (bombEffect != null)
            {
                bombEffect.transform.position = transform.position;
                bombEffect.SetActive(true);
            }

            //Set inactive:
            gameObject.SetActive(false);
        }
    }
}


