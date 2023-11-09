using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSetInactive : MonoBehaviour
{
    public float delay;
    private float timer;

    private void OnEnable()
    {
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > delay)
        {
            gameObject.SetActive(false);
        }
    }
    /*IEnumerator SetInActive(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }*/
}
