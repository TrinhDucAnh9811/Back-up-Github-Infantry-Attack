using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCheckCollision : MonoBehaviour
{
    Vector3 prePos;
    void Start()
    {
        prePos = transform.position;
    }

    
    void FixedUpdate()
    {
        if (Physics.Raycast(prePos, (transform.position - prePos).normalized, out RaycastHit hit, (transform.position - prePos).magnitude));   
        { 
            if (hit.collider)
            {
                Debug.Log(hit.collider.gameObject.name);
            }
        }
           
        prePos = transform.position;
    }
}
