using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCheckCollision : MonoBehaviour
{
    Vector3 prePos;

    private float liveTime;
    private float timer;

    private float rocketRadious;

    private MeshRenderer flashMaterial;

    void OnEnable()
    {
        prePos = transform.position;

        liveTime = 2f;
        timer = 0f;
        rocketRadious = 10.0f;
    }

    void FixedUpdate()
    {
        int layerMask = 1 << 3 | 1 << 6; //Chỉ chiếu tia Ray vào 2 layer: 1 và 6 (Ground + Enemy)
        if (Physics.Raycast(prePos, (transform.position - prePos).normalized, out RaycastHit hit, (transform.position - prePos).magnitude, layerMask))
        { 

            //SUMARY: Nếu bắn trúng Enemy thường với machine gun:
            if (hit.collider.CompareTag("Enemy") && gameObject.CompareTag("PlayerBullet"))
            {   
                    hit.collider.GetComponent<EnemyHealth>().TakeDamage(WeaponStats.machineGun_1_Damage);

                    SetActiveBullet();

                //Particle when hit:
                GameObject hitEffect = EffectPool.instance.GetEffectPool(EnumforPooling.ObjectList.EnemyHit);
                if(hitEffect != null)
                {
                    hitEffect.transform.position = hit.point;

                    hitEffect.SetActive(true);
                }

                //Only bullet will increase rocket counter:
                if (PlayerController.instance.isRocketReady == false)
                {
                    PlayerController.instance.rocketCounter++;
                }

/*                //Play Flashed Hit Anim:
                flashMaterial = hit.collider.GetComponent<MeshRenderer>();
                flashMaterial.material.color = Color.white;*/
              
            }

            //SUMARY: Nếu bắn trúng "Heavy Enemy" với machine gun:
            else if (hit.collider.CompareTag("HeavyEnemy") && gameObject.CompareTag("PlayerBullet"))
            {
                hit.collider.GetComponent<EnemyHealth>().TakeDamage(WeaponStats.machineGun_1_Damage);

                SetActiveBullet();

                //Particle when hit:
                GameObject hitEffect = EffectPool.instance.GetEffectPool(EnumforPooling.ObjectList.HeavyEnemyHit);
                if (hitEffect != null)
                {
                    hitEffect.transform.position = hit.point;
                 /*   hitEffect.transform.localScale = Vector3.one * 20;*/
                    hitEffect.SetActive(true);
                }


                //Only bullet will increase rocket counter:
                if (PlayerController.instance.isRocketReady == false)
                {
                    PlayerController.instance.rocketCounter++;
                }
            }

            //SUMARY: Nếu Rocket bắn trúng (Enemy thường HOẶC Heavy Enemy):
            else if((hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("HeavyEnemy")) && gameObject.CompareTag("PlayerRocket"))
            {
                RocketExplode();

                SetActiveBullet();

                //Particle when rocket hit:
                GameObject rocketHitEffect = EffectPool.instance.GetEffectPool(EnumforPooling.ObjectList.RocketExplode);
                if (rocketHitEffect != null)
                {
                    rocketHitEffect.transform.position = hit.collider.transform.position;
                    rocketHitEffect.SetActive(true);
                }
            }


            //SUMARY:Nếu đạn bắn trúng Ground
            else if (hit.collider.CompareTag("Ground") && gameObject.CompareTag("PlayerBullet"))
            {
                SetActiveBullet();

                //Particle when hit:
                GameObject hitEffect = EffectPool.instance.GetEffectPool(EnumforPooling.ObjectList.EnvironmentHit);
                if (hitEffect != null)
                {
                    hitEffect.transform.position = hit.point;

                    hitEffect.SetActive(true);
                }
            }
        }
           
        prePos = transform.position; //IMPORTANT: the end of frame of method FixedUpadate, save prePos value to lastest transform.position.

        //Code for auto set Inactive bullet:
        timer += Time.fixedDeltaTime;
        if (timer > liveTime)
        {
            SetActiveBullet();
        }
    }


    void SetActiveBullet()
    {
        gameObject.SetActive(false);
        timer = 0;
    }    
    //Rocket and Bomb Explode:
    void RocketExplode()
    { 
        Collider[] colliders = Physics.OverlapSphere(transform.position, rocketRadious);

        foreach (Collider nearbyEnemies in colliders)
        {
            EnemyHealth enemies = nearbyEnemies.GetComponent<EnemyHealth>();
            if (enemies != null)
            {
            /*    enemies.TakeDamage(WeaponStats.enemy_Bomb_Damage);*/
                enemies.TakeDamage(WeaponStats.rocket_1_Damage);
                //TEST RADIUS of BOMB:
 /*               GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = transform.position;
                sphere.transform.localScale = Vector3.one * rocketRadious;
                Debug.Break();*/
            }
        }
    }
}
