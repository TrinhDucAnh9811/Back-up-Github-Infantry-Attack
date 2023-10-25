using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private float fireRate = 10f;

    private float timer;

    private float sensitivity = 0.15f;

    private float bulletSpeed = 100f;

    public Transform firePoint;

    private bool canShoot = true;

    public float yaw;
    public float pitch;

    private Vector2 previousTouchPosition;

    private GameObject sphere;

    public GunRotation gunRotation;

    void Start()
    {

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
        timer += Time.deltaTime;

        if (TouchUtility.TouchCount > 0)
        {
            gunRotation.RotateGun(); //Rotate Gun

            if (timer > 5/fireRate)
            {
                Shoot(firePoint.position);
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
                yaw = Mathf.Clamp(yaw, -50f, 20f);
                pitch = Mathf.Clamp(pitch, -40f, 40f);

                previousTouchPosition = touchPosition;
            }

            else if(touch.phase == TouchPhase.Ended)
            {

            }

            transform.localEulerAngles = new Vector3(pitch, yaw, 0f);
        }

    }

    private void Shoot(Vector3 startPos)
    {
        var targetPos = Aim();

        Vector3 direction = (targetPos - startPos).normalized;

        GameObject bullet = BulletPooling.Instance.GetPooledBullet();

        if(bullet != null)
        {
            bullet.transform.position = startPos;
            bullet.transform.rotation = Quaternion.LookRotation(direction);

            bullet.SetActive(true);

            bullet.GetComponent<Rigidbody>().velocity = direction  * bulletSpeed;

            StartCoroutine(WaitForShoot(2.0f));
        }

        //Set bullet inactive after 2s;
        IEnumerator WaitForShoot(float delay)
        {
            yield return new WaitForSeconds(delay);
            canShoot = true;
            bullet.SetActive(false);
        }

    }

    private Vector3 Aim()
    {
        Vector3 targetPosition;

        //Lấy tạo độ chính giữa màn hình theo ViewPort (0,0) - (1,1):
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            targetPosition = hit.point;
        }
        else
        {
            //Nếu không hit được vật nào trên đường đi, tịnh tiến vị trí của player lên 100 lần, fake:
            targetPosition = transform.localPosition + ray.direction * 100f;
        }

        sphere.transform.position = targetPosition; //Đặt vị trí Shphere ở targetPos

        return targetPosition;
    }


  



    /*Vector3 screenCenterPoint = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);

    // Lấy vị trí hit tại trung tâm màn hình
    RaycastHit hit;
    if (Physics.Raycast(Camera.main.ScreenPointToRay(screenCenterPoint), out hit, 1000.0f))
    {
        transform.LookAt(hit.point);
    }

    GameObject bullet = BulletPooling.Instance.GetPooledBullet();
    if (bullet != null && canShoot == true)
    {
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.SetActive(true);

        Vector3 direction = (hit.point - firePoint.position).normalized;
        bullet.GetComponent<Rigidbody>().velocity = direction * 50;
        canShoot = false;
        StartCoroutine(WaitForShoot(0.5f));

    }

    IEnumerator WaitForShoot(float delay)
    {
        yield return new WaitForSeconds(delay);
        canShoot = true;
        bullet.SetActive(false);
    }*/

}
