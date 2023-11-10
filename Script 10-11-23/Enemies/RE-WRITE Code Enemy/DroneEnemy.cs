using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroneEnemy : EnemyBase
{
    private Vector3 playerRef;

    public GameObject droneBulletPrefab;

    //Code rieng:
    public AnimationCurve translationCurve;

    public AnimationCurve leanCurve;

    public AnimationCurve rotateCurve;

    private float moveDuration = 6f;

    private float time = 0f;

    private Vector3 startPosition;

    private Vector3 endPosition;

    private float moveDurationInv;

    private Quaternion targetRotation;

    private Quaternion startRotation;

    private float x;

    private Tween tween1;
    private Tween tween2;
    private Tween tween3;
    private Tween tween4;

    private void OnEnable()
    {
        startPosition = transform.localPosition;

        float randomIndex = (Random.Range(0, 2) == 0) ? Random.Range(-10, -8) : Random.Range(8, 10); //Randome Vector
        Vector3 randomVector = new Vector3(randomIndex, 0, 0);


        gatheringPoint = startPosition + randomVector; 

        //Code rieng:
      
        endPosition = gatheringPoint;

        moveDurationInv = 1f / moveDuration;

        startRotation = transform.localRotation;

        Vector3 directionToPlayer = PlayerController.instance.transform.position - gatheringPoint;
        directionToPlayer.y = 0f;
        targetRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);

        //Code mới:

        playerRef = PlayerController.instance.transform.position;

        moveSpeed = 5.0f;
        rotateSpeed = 2.0f;

        attackSpeed = 1.0f;
        bulletSpeed = 20.0f;

        StartCoroutine(DroneLoop());
    }



    IEnumerator DroneLoop()
    {
        while (true)
        {
            //Đi tới vị trí:
            tween1 = transform.DOLocalMove(endPosition, moveDuration).SetEase(Ease.InOutQuad);
            tween2 = transform.DOLocalRotateQuaternion(targetRotation, moveDuration).SetEase(Ease.InOutQuad).
                OnUpdate(() =>
                {
                    float leanAngle = Mathf.Lerp(0f, 10f, leanCurve.Evaluate(time));
                    Quaternion leanRotation = Quaternion.AngleAxis(leanAngle, transform.right);
                    transform.localRotation = leanRotation * transform.localRotation;

                    time += Time.deltaTime * moveDurationInv;


                });


            //Đợi khi đến nơi:
            yield return new WaitForSeconds(6.0f);

            //TEST SHOOT:
            canAttack = true;

            //Quay lại vị trí ban đầu:
            tween3 = transform.DOLocalMove(startPosition, moveDuration).SetEase(Ease.InOutQuad);
            tween4 = transform.DOLocalRotateQuaternion(targetRotation, moveDuration).SetEase(Ease.InOutQuad).
                OnUpdate(() =>
                {

                    float leanAngle = Mathf.Lerp(10f, 0f, leanCurve.Evaluate(time));
                    Quaternion leanRotation = Quaternion.AngleAxis(leanAngle, transform.right);
                    transform.localRotation = leanRotation * transform.localRotation;

                    time += Time.deltaTime * moveDurationInv;
                });

            yield return new WaitForSeconds(6.0f);
            time = 0;
        }
    }

    //Sau phải thêm vào để tránh lỗi khi Transform bị mất khi Drone bị bắn
    private void OnDestroy()
    {
        tween1.Kill();
        tween2.Kill();
        tween3.Kill();
        tween4.Kill();
    }

    protected override void EnemyMovement()
    {

    }



    private void Update()
    {
        if (canAttack)
        {
            EnemyAttack(attackSpeed, bulletSpeed);
        }
    }

    protected override void EnemyAttack(float attackSpeed, float bulletSpeed)
    {
        GameObject droneBullet = Instantiate(droneBulletPrefab, transform.position, Quaternion.identity);

        Vector3 direction = (playerRef - transform.position).normalized;

        droneBullet.transform.rotation = Quaternion.LookRotation(direction); //Đảm bảo viên đạn luôn quay về phái player.

        droneBullet.GetComponent<Rigidbody>().velocity = direction * (Time.deltaTime * bulletSpeed * 50);

        GameObject effect = EffectPool.instance.GetEffectPool(EnumforPooling.ObjectList.EnemyBulletFire);
        if (effect != null)
        {
            effect.transform.position = transform.position;
            effect.SetActive(true);
        }

        canAttack = false;

        StartCoroutine(WaitForShoot(attackSpeed));
    }




}



