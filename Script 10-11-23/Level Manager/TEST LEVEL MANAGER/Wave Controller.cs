using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public GameObject[] waveEnemyType;


    private int currentWave;

    private void Start()
    {
        for(int i =0; i< waveEnemyType.Length;i++)
        {
            waveEnemyType[i].SetActive(true);
        }
    }
    public virtual void StartWave()
    {
        
    }

    public virtual void EndWave()
    {

    }
}
