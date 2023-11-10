/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchiveWave : Singleton<WaveManager>
{
    List<int> enemyList = new List<int>();

    public int waveIndex;
    public int enemyType;
    public int maxEnemyPerWave = 1;

    private Vector3 gatherPoint;

    private int level;
    private int currentWave = 1;
    private int enemyPerWave = 4; //Min = 4;
    private int enemyIndex = 0; //Default: troop enemy
    private int maxWave = 5;


    //TÉT:
    float timer = 0;
    float call = 1.5f;
    bool canCall = true;

    private void Hey()
    {

        SpawnWave(enemyPerWave); //Base on level, type and quantity of Enemy will differ;
    }

    void SpawnWave(int enemyPerWave)
    {
        if (currentWave <= 2)
        {
            enemyPerWave++;
        }
        for (int i = 0; i < enemyPerWave; i++)
        {
            SpawnEnemies(0);
        }
    }

    private void Update()
    {
        //TEST:
        timer += Time.deltaTime;
        if (timer > call && canCall)
        {
            Hey();
            canCall = false;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            SpawnEnemies(1);
            SpawnEnemies(2);
            SpawnEnemies(3);
            SpawnEnemies(4);
            SpawnEnemies(5);

        }
    }

    public void AddEnemyToList()
    {
        enemyList.Add(1);

    }

    public void DeleteEnemyFromList()
    {
        enemyList.Remove(1);


        if (enemyList.Count == 0)
        {
            SpawnEnemies(0); //EXAMPLE enemy 0


            SpawnWave(enemyPerWave); //TEST

        }
    }

    private void SpawnEnemies(int index)
    {

        AddEnemyToList();//Everytime spawn, call this function to add to list:

        switch (index)
        {
            case 0: //Troop Enemy
                {
                    GetEnemyFromPool(EnumForEnemyType.ObjectList.TroopEnemy);
                }
                break;

            case 1://Bomb Enemy
                {
                    GetEnemyFromPool(EnumForEnemyType.ObjectList.BombEnemy);
                }
                break;

            case 2: //Shield Enemy
                {
                    GetEnemyFromPool(EnumForEnemyType.ObjectList.ShieldEnemy);
                }
                break;

            case 3: //DRONE
                {
                    GetEnemyFromPool(EnumForEnemyType.ObjectList.DroneEnemy);
                }
                break;

            case 4: //TANK
                {
                    GetEnemyFromPool(EnumForEnemyType.ObjectList.TankEnemy);
                }
                break;

            case 5: //Suicide car Eneny
                {
                    GetEnemyFromPool(EnumForEnemyType.ObjectList.SuicideCarEnemy);
                }
                break;
        }
    }


    void GetEnemyFromPool(EnumForEnemyType.ObjectList name)
    {
        Vector3 spawnPos = new Vector3(Random.Range(-25, 25), 0.5f, Random.Range(70, 80)); // For Ground Enemy
        gatherPoint = new Vector3(Random.Range(-25, 25), 0, Random.Range(40, 50)); // Gathering Point

        GameObject enemy = EnemyPool.instance.GetEnemyPool(name);
        if (enemy != null)
        {

            enemy.transform.position = spawnPos;
            enemy.SetActive(true);

            //ĐK Event
            enemy.GetComponent<EnemyHealth>().OnEnemyDead += DeleteEnemyFromList;
        }
    }
}
*/