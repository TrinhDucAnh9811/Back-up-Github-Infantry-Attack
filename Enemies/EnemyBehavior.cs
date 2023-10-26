using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBehavior : MonoBehaviour
{
    private float moveSpeed = 1.0f;

    private Rigidbody enemyRb;

    public Transform firstPlace;

    private int positionIndex = 0;
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
    }

   
    void Update()
    {
        if((firstPlace.position - transform.position).magnitude <= 1)
        {
            positionIndex = 1;
        }
        else if((PlayerMovement.instance.transform.position - transform.position).magnitude <= 7)
        {
            positionIndex = 2;
        }

        switch(positionIndex)
        {
            case 0:
                {
                    Vector3 moveToFirstPlace = (firstPlace.position - transform.position).normalized;
                    enemyRb.velocity = moveToFirstPlace * moveSpeed;
                }
                break;
            case 1:
                {
                    Vector3 moveDirection = (PlayerMovement.instance.transform.position - transform.position).normalized;
                    enemyRb.velocity = moveDirection * moveSpeed * 2;
                }
                break;

            case 2:
                {
                    enemyRb.velocity = Vector3.zero;
                }
                break;

        }
    }
}
