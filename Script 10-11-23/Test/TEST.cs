using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TEST : MonoBehaviour
{
    public AnimationCurve leanCurve; // Curve cho nghiêng
    public Transform targetPoint; // Điểm đích
    public Transform playerTransform; // Vị trí của người chơi
    public float moveDuration = 2f; // Thời gian di chuyển
    public float stopDuration = 2f; // Thời gian dừng lại
    private float time = 0f;
    private Quaternion startRotation;
    private Sequence droneSequence;

    private void Start()
    {
        startRotation = transform.localRotation;
        droneSequence = DOTween.Sequence();

        Vector3 toTarget = playerTransform.position - targetPoint.position;
        toTarget.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(toTarget, Vector3.up);

        droneSequence.Append(transform.DOMove(targetPoint.position, moveDuration).SetEase(Ease.InOutQuad));
        droneSequence.Join(transform.DORotateQuaternion(targetRotation, moveDuration).SetEase(Ease.InOutQuad).OnUpdate(() =>
        {
            float leanAngle = Mathf.Lerp(0f, 10f, leanCurve.Evaluate(time));
            Quaternion leanRotation = Quaternion.AngleAxis(leanAngle, transform.right);
            transform.localRotation = leanRotation * transform.localRotation;

            time += Time.deltaTime / moveDuration;
        }));

        droneSequence.AppendInterval(stopDuration);

        droneSequence.Append(transform.DOMove(startRotation.eulerAngles, moveDuration).SetEase(Ease.InOutQuad));
        droneSequence.Join(transform.DORotateQuaternion(startRotation, moveDuration).SetEase(Ease.InOutQuad).OnUpdate(() =>
        {
            float leanAngle = Mathf.Lerp(0f, 10f, leanCurve.Evaluate(time));
            Quaternion leanRotation = Quaternion.AngleAxis(leanAngle, transform.right);
            transform.localRotation = leanRotation * transform.localRotation;

            time += Time.deltaTime / moveDuration;
        }));

        droneSequence.SetLoops(-1); // Lặp vô hạn
    }
}


/*
MachineGun, - DONE
       Rocket, - DONE
       GunSmoke, - DONE
       EnemyBullet,
       EnemyBomb, - DONE
       EnemyTank,
       EnemyDestroy,
       BuidlingDestroy,

       //HIT KEYWORD:
       EnemyHit, - DONE
       HeavyEnemyHit,
       EnvironmentHit

*/





