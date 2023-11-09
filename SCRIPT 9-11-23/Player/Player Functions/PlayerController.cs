using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static EffectPool;

public class PlayerController : Singleton<PlayerController>
{
    //Rocket variable:
    public float rocketCounter = 1.0f;
    public float rocketMaxCount = 20.0f;
    public bool isRocketReady;
    public Slider rocketSlider;

    public float fireRate = 0.2f;

    private float timer;

    private float sensitivity = 0.15f;

    private float bulletSpeed = 200f;
    private float rocketSpeed = 60.0f;

    public Transform bulletFirePoint;
    public Transform rocketFirePoint;

    public float yaw;
    public float pitch;

    private Vector2 previousTouchPosition;

    private GameObject sphere;

    public GunRotation gunRotation;

    public Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();


        //Set timer = 100 so we can shoot at the beggining
        timer = 100;

        //Set up initial pitch and yaw angles
        Vector3 eulerAngles = transform.localEulerAngles;
        yaw = eulerAngles.y;
        pitch = eulerAngles.x;

        //Aiming debug:
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = Vector3.one * 0.05f;
        Destroy(sphere.GetComponent<Collider>());
    }


    void Update()
    {
        //Check Rocket Counter, if > 20, then rocket is ready
        CheckRocketReady();
        UpdateRocketSlider();

        timer += Time.deltaTime;

        if (TouchUtility.TouchCount > 0)
        {
            animator.SetBool("isShooting", true); //Start Animation for shaking gun

            gunRotation.status = 0;  //Rotate Gun

            if (timer > fireRate)
            {
                Shoot(bulletFirePoint.position, EnumforPooling.ObjectList.MachineGunEffect); //Shoot bullets
                timer = 0; 
            }
            

            var touch = TouchUtility.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                previousTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchPosition = touch.position;
                Vector2 delta = touchPosition - previousTouchPosition;

                yaw += delta.x * sensitivity;
                pitch -= delta.y * sensitivity;

                //Constrain angles:
                yaw = Mathf.Clamp(yaw, -50f, 50f);
                pitch = Mathf.Clamp(pitch, -40f, 30f);

                previousTouchPosition = touchPosition;
            }

            else if(touch.phase == TouchPhase.Ended)
            {
                if(isRocketReady == true) //Nếu rocket sẵn sàng để release:
                {             
                    Shoot(rocketFirePoint.position, EnumforPooling.ObjectList.RocketExplode);
                    isRocketReady = false;
                    rocketCounter = 1;
                }

                animator.SetBool("isShooting", false); //Start Animation for shaking gun

                gunRotation.status =1; //Slow rotate Gun

                //Effect Smoke after Shooting:
                GameObject smokeEffect = EffectPool.instance.GetEffectPool(EnumforPooling.ObjectList.GunSmoke);
                if (smokeEffect != null)
                {
                    smokeEffect.transform.position = bulletFirePoint.position;
                    smokeEffect.transform.SetParent(bulletFirePoint.transform, true);
                    smokeEffect.transform.localScale = transform.localScale / 10;
                    smokeEffect.SetActive(true);

                }
            }

            transform.localEulerAngles = new Vector3(pitch, yaw, 0f);
        }

    }
   
    //Check Rocket:
    private void CheckRocketReady()
    {
        if(rocketCounter >=20)
        {
            isRocketReady = true;
            rocketCounter = 1;
        }
    }

    public void UpdateRocketSlider()
    {
        rocketSlider.value = rocketCounter / rocketMaxCount;
    }

    private void Shoot(Vector3 startPos, EnumforPooling.ObjectList index)
    {
        switch(index)
        {
            case EnumforPooling.ObjectList.MachineGunEffect: //Bullet
                {
                    //Gọi Effect Pool:
                    GameObject bulleteffect = EffectPool.instance.GetEffectPool(index);
                    if(bulleteffect != null)
                    {
                        bulleteffect.transform.SetParent(bulletFirePoint.transform, true);

                        bulleteffect.transform.position = bulletFirePoint.position;
                        
                        bulleteffect.SetActive(true);
                    }



                    var targetPos = Aim();

                    Vector3 direction = (targetPos - startPos).normalized;

                    GameObject bullet = BulletPooling.instance.GetPooledBullet(index);

                    if (bullet != null)
                    {
                        bullet.transform.position = startPos;
                        bullet.transform.rotation = Quaternion.LookRotation(direction);

                        bullet.SetActive(true);

                        bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
                    }
                    else
                    {
    
                    }
                }
                break;

            case EnumforPooling.ObjectList.RocketExplode: //Rocket
                {
                    //THAY CODE GỌI EFFECT TỪ POOL Ở ĐÂY

                    var targetPos = Aim();

                    Vector3 direction = (targetPos - startPos).normalized;

                    GameObject bullet = BulletPooling.instance.GetPooledBullet(index);

                    if (bullet != null)
                    {
                        bullet.transform.position = startPos;
                        bullet.transform.rotation = Quaternion.LookRotation(direction);

                        bullet.SetActive(true);

                        bullet.GetComponent<Rigidbody>().velocity = direction * rocketSpeed;

                    }

                    //Debug.Break();
                }
                break;
        }
        

    }

    private Vector3 Aim()
    {
        Vector3 targetPosition;

        //Lấy tạo độ chính giữa màn hình theo ViewPort (0,0) - (1,1):
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        //int layerMask = LayerMask.GetMask("Enemy", "Ground");

        int layerMask = 1 << 3 | 1 << 6; //Chỉ chiếu tia Ray vào 2 layer: 1 và 6 (Ground + Enemy)
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
        {
            targetPosition = hit.point;
            /*if (hit.collider) Debug.Log(hit.collider.gameObject.name);*/
        }
        else
        {
            //Nếu không hit được vật nào trên đường đi, tịnh tiến vị trí của player lên 100 lần, fake:
            targetPosition = transform.localPosition + ray.direction * 100f;
        }

        sphere.transform.position = targetPosition; //Đặt vị trí Shphere ở targetPos

        return targetPosition;
    }
  
/*    IEnumerator WaitForDisable(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }*/
}
