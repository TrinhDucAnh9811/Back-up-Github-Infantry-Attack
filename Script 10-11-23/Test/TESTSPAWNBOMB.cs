using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTSPAWNBOMB : MonoBehaviour
{
    public GameObject bombPrefab;
    private Vector3 spawnPos;
    void Start()
    {
        spawnPos = new Vector3(0, 0.5f, 20.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            Instantiate(bombPrefab, spawnPos, Quaternion.identity);
        }
    }
}
