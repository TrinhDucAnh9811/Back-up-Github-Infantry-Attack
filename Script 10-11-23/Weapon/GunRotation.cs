using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    private float rotateSpeed = 10;

    public float timeToStop = 0f;

    public int status;

/*    PlayerController playerController;*/

    private void Start()
    {
        //At the beginning:
        status = 2;
   /*     rotateSpeed = 10;*/
     /*   playerController = GetComponent<PlayerController>(); */
    }

    private void Update()
    {
  /*      //Custom rotate speed base on fireRate of machine gun: (IMPORTANT)
        rotateSpeed = playerController.fireRate *200;*/
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
                    float delta = (rotateSpeed - timeToStop*10)*100;
                    transform.Rotate(Vector3.right, -delta * Time.deltaTime, Space.Self);
                    if (delta <= 0)
                    {
                        status = 2;
                        
                        timeToStop = rotateSpeed;
                    }
                }
                break;

            case 2: //Stop
                {
                    transform.Rotate(Vector3.right, 0f * Time.deltaTime, Space.Self);
                }
                break;
        }

    }
}

