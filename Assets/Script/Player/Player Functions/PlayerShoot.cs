using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private float bulletSpeed = 50.0f;
    public Transform firePoint;
    
    void Start()
    {
       
    }


    /*void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            PlayerShooting();
        }
    
    }

    public void PlayerShooting()
    {
        GameObject bullet = BulletPooling.Instance.GetPooledBullet();
        if (bullet != null)
                    {
                        
                        bullet.transform.position = firePoint.position; 
                        bullet.transform.rotation = transform.rotation;

                        bullet.SetActive(true);

                        Rigidbody projectileRb = bullet.GetComponent<Rigidbody>();

                        projectileRb.velocity = firePoint.forward * bulletSpeed;  //Bắn theo hướng thẳng của firepoint

                        StartCoroutine(AutoSetInactive(2));
                    }

        IEnumerator AutoSetInactive(int time)
            {
            yield return new WaitForSeconds(time);
            bullet.SetActive(false);
            }
    }*/

    
}
