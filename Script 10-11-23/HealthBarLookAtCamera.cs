using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarLookAtCamera : MonoBehaviour
{

    void Start()
    {
        
    }

    
    void Update()
    {
        var direction = (PlayerController.instance.transform.position - transform.position).normalized;
        transform.LookAt(direction);
    }
}
