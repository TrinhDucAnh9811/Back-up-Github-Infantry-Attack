using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float maxHp = 100.0f;
    private float currentHp;
    void Start()
    {
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHp <=0)
        {
            EnemyDied();
        
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerProjectile"))
        {
            TakeDamage(WeaponStats.Instance.machineGun_1_Damage);
        }
    }

    private void TakeDamage(float damage)
    {
        currentHp -= damage;
    }

    private void EnemyDied()
    {
        Destroy(gameObject);   //Change this lines of code to "Inactive gameObject" later on
    }
}
