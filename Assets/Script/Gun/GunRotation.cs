using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    private float rotateSpeed = 10.0f;
    public void RotateGun()
    {
        transform.Rotate(Vector3.right, rotateSpeed);
    }
}
