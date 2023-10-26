using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    public float rotateSpeed = 100;

    public float timeToStop = 0f;

    public int status;

    private void Start()
    {
        //At the beginning:
        status = 2;
    }

    private void Update()
    {

        switch (status)
        {
            case 0: //Rolling
                {
                    transform.Rotate(Vector3.right, rotateSpeed);
                    timeToStop = 0;
                }
                break;

            case 1: //Slowly stop
                {
                    timeToStop += Time.deltaTime;
                    float delta = rotateSpeed - timeToStop*10;
                    transform.Rotate(Vector3.right, delta);
                    if (delta <= 0)
                    {
                        status = 2;
                        
                        timeToStop = rotateSpeed;
                    }
                }
                break;

            case 2: //Stop
                {
                    transform.Rotate(Vector3.right, 0f);
                }
                break;
        }

    }
}

