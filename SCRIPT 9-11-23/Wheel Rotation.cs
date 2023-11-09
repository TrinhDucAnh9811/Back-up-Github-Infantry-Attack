using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    private float rotateSpeed = 1;

    private void Update()
    {
        transform.Rotate(Vector3.right, -rotateSpeed);
    }
}
