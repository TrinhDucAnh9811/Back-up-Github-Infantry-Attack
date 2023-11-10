using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent agent;

    public float distanceToPlayer;
    private Vector3 playerRef;
    public GameObject troopBulletPrefab;
    private bool canShoot = false;

    private Vector3 angleToRotate;
    public Vector3 gatherPoint;
    private bool arrived = false;

    private float rotateSpeed = 3.0f;

    public Transform target;
    private Vector3 destination;

    void Awake()
    {
        playerRef = PlayerController.instance.transform.position;
        agent = GetComponent<NavMeshAgent>();
        
        gatherPoint = new Vector3(Random.Range(-25, 25), 0, Random.Range(8, 9));
        destination = playerRef + gatherPoint;
        destination = target.position;
        destination.y = 0f;
        agent.SetDestination(destination);
    }

    void Update()
    {
        // Tính hướng quay đến người chơi
        angleToRotate = (playerRef - transform.position).normalized;

        RotateTowardsPlayer(angleToRotate);

        //Tính toán khoảng cách đến người chơi:
        distanceToPlayer = (destination - transform.position).magnitude;

        if (distanceToPlayer <= 2f && !arrived)
        {
            canShoot = true;
            arrived = true;
        }

        else if (canShoot)
        {
            agent.updateRotation = false;
            Shoot();

            StartCoroutine(WaitForAttack(1.5f));
        }
    }

    IEnumerator WaitForAttack(float time)
    {
        yield return new WaitForSeconds(time);
        canShoot = true;

    }

    void RotateTowardsPlayer(Vector3 direction)
    {
        // Tính góc quay
        Quaternion targetAngle = Quaternion.LookRotation(direction);

        // Lerp để quay từ góc hiện tại đến góc muốn quay
        transform.rotation = Quaternion.Lerp(transform.rotation, targetAngle, rotateSpeed * Time.deltaTime);
    }

    void Shoot()
    {
        GameObject effect = EffectPool.instance.GetEffectPool(EnumforPooling.ObjectList.EnemyBulletFire);
        if (effect != null)
        {
            effect.transform.position = transform.position + new Vector3(0, 0, 2); // Đặt vị trí bắn ở phía trước của đối tượng
            effect.transform.rotation = Quaternion.Euler(-90, 0, 90); // Đặt góc bắn
            effect.transform.localScale = Vector3.one * 3;
            effect.SetActive(true);
        }

        GameObject bullet = Instantiate(troopBulletPrefab, transform.position, Quaternion.identity);

        Vector3 direction = (playerRef - transform.position).normalized;
        bullet.GetComponent<Rigidbody>().velocity = direction * (Time.deltaTime * 6000);

        canShoot = false;

    }
}
