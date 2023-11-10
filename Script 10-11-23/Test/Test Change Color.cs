/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChangeColor : MonoBehaviour
{
    public float blinkIntensity;
    public float blinkDuration;
    float blinkTimer;
    public Material testMaterial;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           *//* blinkTimer -= Time.deltaTime;
            float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
            float intensitty = lerp * blinkIntensity;*//*
            testMaterial.color = Color.white;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            testMaterial.color = Color.red;
        }
    }
}
*/