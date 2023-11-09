using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBulletTrail : MonoBehaviour
{
    public TrailRenderer bulletTrail;

    void Start()
    {
        bulletTrail = GetComponent<TrailRenderer>();
    }

    private void OnEnable()
    {
        bulletTrail.Clear();
    }

    private void OnDisable()
    {
        bulletTrail.Clear();
    }
}
