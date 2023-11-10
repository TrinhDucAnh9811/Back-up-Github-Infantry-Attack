using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWave : WaveController
{
    public int enemyCount;
    public GameObject enemyPrefab;
    void Start()
    {
        StartWave();
    }

    public override void StartWave()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject obj = Instantiate(enemyPrefab, new Vector3(0, 1, 35), Quaternion.identity);
        }
    }

    public override void EndWave()
    {

    }
}
