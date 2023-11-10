using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyCount; 
    [SerializeField] private Vector3 groundedEnemySpawnPos; //CÂN NH?C
    [SerializeField] private Vector3 airEnemySpawnPos; //CÂN NH?C
    [SerializeField] private int currentWaves;
    [SerializeField] private int maxWaves_PerLevel;

    //TEST, in future, will make enum for level and use Dictionary to SpawnEnemy at certain waves.

    public int waveIndex;
    public int enemyType;
    public int maxEnemyPerWave = 1;
    

    private Vector3 gatherPoint;
    void Start()
    {
     /*   SpawnEnemies(1);*/
        SpawnEnemies(0);
    }


    void Update()
    {
        /*   if(Input.GetKeyDown(KeyCode.Space))
           {
               SpawnEnemies(1);
               SpawnEnemies(0);
           }*/
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemyCount.Length == 0)
        {
            maxEnemyPerWave++;
            for (int i = 0; i < maxEnemyPerWave; i++)
            {
             /*   SpawnEnemies(1);*/
             /*   SpawnEnemies(0);*/
            }
        }
    }
   
    private void SpawnEnemies(int index)
    {

        switch (index)
        {
            case 0: //Troop Enemy
                {
                    Vector3 spawnPos = new Vector3(Random.Range(-25, 25), 0.5f, Random.Range(70, 80)); // For Ground Enemy
                    gatherPoint = new Vector3(Random.Range(-25, 25), 0, Random.Range(40, 50)); // Gathering Point

                    GameObject enemy = EnemyPool.instance.GetEnemyPool(EnumForEnemyType.ObjectList.TroopEnemy);
                    if (enemy != null)
                    {
                        /*enemyBahavior= enemy.GetComponent<EnemyBehavior>();
                        if (enemyBahavior != null)
                        {
                            enemyBahavior.gatheringPoint = gatherPoint;
                        }

                        Vector3 direction = (PlayerController.instance.transform.position - spawnPos).normalized; // Direction to player

                        enemy.transform.position = spawnPos;
                        enemy.transform.rotation = Quaternion.LookRotation(direction);*/
                        enemy.transform.position = spawnPos;
                        enemy.SetActive(true);

                    }
                }
                break;

            case 1://Bomb Enemy
                {
                    Vector3 spawnPos = new Vector3(Random.Range(-25, 25), 0.5f, Random.Range(70, 80)); // For Ground Enemy
                    gatherPoint = new Vector3(Random.Range(-25, 25), 0, Random.Range(40, 50)); // Gathering Point

                    GameObject enemy = EnemyPool.instance.GetEnemyPool(EnumForEnemyType.ObjectList.BombEnemy);
                    if (enemy != null)
                    {
                        /*enemyBahavior= enemy.GetComponent<EnemyBehavior>();
                        if (enemyBahavior != null)
                        {
                            enemyBahavior.gatheringPoint = gatherPoint;
                        }

                        Vector3 direction = (PlayerController.instance.transform.position - spawnPos).normalized; // Direction to player

                        enemy.transform.position = spawnPos;
                        enemy.transform.rotation = Quaternion.LookRotation(direction);*/
                        enemy.transform.position = spawnPos;
                        enemy.SetActive(true);

                    }
                }
                break;

            case 2: //Shield Enemy
                {
                    Vector3 spawnPos = new Vector3(Random.Range(-25, 25), 0.5f, Random.Range(70, 80)); // For Ground Enemy
                    gatherPoint = new Vector3(Random.Range(-25, 25), 0, Random.Range(40, 50)); // Gathering Point

                    GameObject enemy = EnemyPool.instance.GetEnemyPool(EnumForEnemyType.ObjectList.ShieldEnemy);
                    if (enemy != null)
                    {
                        /*enemyBahavior= enemy.GetComponent<EnemyBehavior>();
                        if (enemyBahavior != null)
                        {
                            enemyBahavior.gatheringPoint = gatherPoint;
                        }

                        Vector3 direction = (PlayerController.instance.transform.position - spawnPos).normalized; // Direction to player

                        enemy.transform.position = spawnPos;
                        enemy.transform.rotation = Quaternion.LookRotation(direction);*/
                        enemy.transform.position = spawnPos;
                        enemy.SetActive(true);

                    }
                }
                break;

            case 3: //DRONE
                {
                    Vector3 spawnPos = new Vector3(Random.Range(-12, 12), 7.0f, Random.Range(20, 30)); // For Drone Enemy
                    gatherPoint = new Vector3(Random.Range(-12, 12), 7.0f, Random.Range(12, 20)); // Gathering Point

                    GameObject enemy = EnemyPool.instance.GetEnemyPool(EnumForEnemyType.ObjectList.DroneEnemy);
                    if (enemy != null)
                    {
                        /*enemyBahavior= enemy.GetComponent<EnemyBehavior>();
                        if (enemyBahavior != null)
                        {
                            enemyBahavior.gatheringPoint = gatherPoint;
                        }

                        Vector3 direction = (PlayerController.instance.transform.position - spawnPos).normalized; // Direction to player

                        enemy.transform.position = spawnPos;
                        enemy.transform.rotation = Quaternion.LookRotation(direction);*/
                        enemy.transform.position = spawnPos;
                        enemy.SetActive(true);

                    }
                }
                break;

            case 4: //TANK
                {
                    Vector3 spawnPos = new Vector3(Random.Range(-25, 25), 0.5f, Random.Range(70, 80)); // For Ground Enemy
                    gatherPoint = new Vector3(Random.Range(-25, 25), 0, Random.Range(40, 50)); // Gathering Point

                    GameObject enemy = EnemyPool.instance.GetEnemyPool(EnumForEnemyType.ObjectList.TankEnemy);
                    if (enemy != null)
                    {
                        /*enemyBahavior = enemy.GetComponent<EnemyBehavior>();
                        if (enemyBahavior != null)
                        {
                            enemyBahavior.gatheringPoint = gatherPoint;
                        }

                        Vector3 direction = (PlayerController.instance.transform.position - spawnPos).normalized; // Direction to player

                        enemy.transform.position = spawnPos;
                        enemy.transform.rotation = Quaternion.LookRotation(direction);*/
                        enemy.transform.position = spawnPos;
                        enemy.SetActive(true);

                    }
                }
                break;

            case 5: //Suicide car Eneny
                {
                    Vector3 spawnPos = new Vector3(Random.Range(-25, 25), 0.5f, Random.Range(70, 80)); // For Ground Enemy
                    gatherPoint = new Vector3(Random.Range(-25, 25), 0, Random.Range(40, 50)); // Gathering Point

                    GameObject enemy = EnemyPool.instance.GetEnemyPool(EnumForEnemyType.ObjectList.SuicideCarEnemy);
                    if (enemy != null)
                    {
                        /*enemyBahavior = enemy.GetComponent<EnemyBehavior>();
                        if (enemyBahavior != null)
                        {
                            enemyBahavior.gatheringPoint = gatherPoint;
                        }

                        Vector3 direction = (PlayerController.instance.transform.position - spawnPos).normalized; // Direction to player

                        enemy.transform.position = spawnPos;
                        enemy.transform.rotation = Quaternion.LookRotation(direction);*/
                        enemy.transform.position = spawnPos;
                        enemy.SetActive(true);

                    }
                }
                break;
                // Handle other cases here
        }
    }


}
