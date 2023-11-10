using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuteBehavior : MonoBehaviour
{
    public GameObject parachutePrefab;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            parachutePrefab.SetActive(false);
        }
    }
}
