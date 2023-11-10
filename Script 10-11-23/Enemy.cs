using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar
{
    public float percent;
}

public class Enemy : MonoBehaviour
{
    public bool showHealthBar;

    private HealthBar healthBar;

    public void Awake()
    {
        if (showHealthBar)
        {
            //spawn healthbar
        }
    }

    private void Update()
    {
       if (showHealthBar)
        {
            //healthBar.transform.position = transform.position;
        }
    }
}
