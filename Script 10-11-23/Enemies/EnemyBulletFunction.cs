using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletFunction : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && gameObject.CompareTag("TroopEnemyBullet"))
        {
            PlayerHealth.instance.TakeDamage(EnemyBase.troopEnemyDamage);
            gameObject.SetActive(false);
        }

        if(collision.gameObject.CompareTag("Player") && gameObject.CompareTag("TankEnemyBullet"))
        {
            PlayerHealth.instance.TakeDamage(EnemyBase.tankBulletdamage);
            gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("DroneEnemyBullet"))
        {
            PlayerHealth.instance.TakeDamage(EnemyBase.droneEnemyDamage);
            gameObject.SetActive(false);
        }
    }
}
