
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyPool;

public class WaveManager : Singleton<WaveManager>
{
    private List<EnemyHealth> enemyList = new List<EnemyHealth>();

    private List<Vector3> spawnList = new List<Vector3>();

    //Sau sẽ để ở Level Manager, mỗi tọa độ ứng với từng đợt/loại Enemy

    //Stage 1:
    private Vector3 spawnPos1 = new Vector3(14.2f, 1.38f, 77.9f);
    private Vector3 spawnPos2 = new Vector3(-36.6f, 1.38f, 77.9f);
    private Vector3 spawnPos3 = new Vector3(-37.7f, 1.38f, 56.9f);
    private Vector3 currentSpawnPos;

    private int enemyIndex;

    public int currentWave;
    public int maxWave = 3; //Sau sẽ base vào level để set maxWave at Start();

    private int enemyPerWave;

    private Vector3 gatherPoint;

    private void Start()
    {
        spawnList.Add(spawnPos1);
        spawnList.Add(spawnPos2);
        spawnList.Add(spawnPos3);

        currentSpawnPos = spawnList[0];

        //Delay 1s to Spawn Wave 1:
        StartCoroutine(LevelBegin(1));            
    }

    //Get all info of Wave:
    void IndexWave(int wave)
    {
        if (wave == 1)
        {
            enemyIndex = 0;
            currentWave = wave;
            enemyPerWave = 3;
        }

        else if (wave == 2)
        {
            enemyIndex = 1;
            currentWave = wave;
            enemyPerWave = 5;
        }

        else if (wave == 3)
        {
            enemyIndex = 2;
            currentWave = wave;
            enemyPerWave = 10;
        }
        else
        {
            //CODE FOR MOVE TO NEXT STAGE 
        }
    }

    //Start the currentWave
    void WaveSpawn(Vector3 spawnPos)
    {
        for (int i = 0; i < enemyPerWave; i++)
            {
                SpawnEnemies(enemyIndex, spawnPos);
            }
    }
   

    public void AddEnemyToList(EnemyHealth enemyHealth)
    {
        enemyList.Add(enemyHealth);

      /*  Debug.Log("Added List: " +  enemyList.Count);*/
    }

    public void DeleteEnemyFromList(EnemyHealth enemyHealth)
    {
        enemyHealth.OnEnemyDead -= DeleteEnemyFromList;

        enemyList.Remove(enemyHealth);

        /*   Debug.Log("removed List: " + enemyList.Count);*/

        if (enemyList.Count == 0)
        {
            currentWave++;

            if(currentWave > maxWave)
            { 
                currentWave = maxWave;
            }

            IndexWave(currentWave);

            currentSpawnPos = spawnList[Random.Range(0, spawnList.Count)];
            WaveSpawn(currentSpawnPos); //Random Spawn Pos for next Wave       
        }
    }


    private void SpawnEnemies(int index, Vector3 spawnPos)
    {
        switch (index)
        {
            case 0: //Troop Enemy
                {
                    GetEnemyFromPool(EnumForEnemyType.ObjectList.TroopEnemy, spawnPos);
                }
                break;

            case 1://Bomb Enemy
                {
                    GetEnemyFromPool(EnumForEnemyType.ObjectList.BombEnemy, spawnPos);
                }
                break;

            case 2: //Shield Enemy
                {
                    GetEnemyFromPool(EnumForEnemyType.ObjectList.ShieldEnemy, spawnPos);
                }
                break;

            case 3: //DRONE
                {
                    GetEnemyFromPool(EnumForEnemyType.ObjectList.DroneEnemy, spawnPos);
                }
                break;

            case 4: //TANK
                {
                    GetEnemyFromPool(EnumForEnemyType.ObjectList.TankEnemy, spawnPos);
                }
                break;

            case 5: //Suicide car Eneny
                {
                    GetEnemyFromPool(EnumForEnemyType.ObjectList.SuicideCarEnemy, spawnPos);
                }
                break;
        }
    }


    void GetEnemyFromPool(EnumForEnemyType.ObjectList name, Vector3 spawnPosition)
    {
        spawnPosition = currentSpawnPos;
        gatherPoint = new Vector3(Random.Range(-25, 25), 0, Random.Range(40, 50)); // Gathering Point

        GameObject enemy = EnemyPool.instance.GetEnemyPool(name);
        if (enemy != null)
        {
            enemy.transform.position = spawnPosition;
            enemy.SetActive(true);

            //ĐK Event
            var enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyHealth.OnEnemyDead += DeleteEnemyFromList;

            AddEnemyToList(enemyHealth);
        }
    }



    IEnumerator LevelBegin(float time)
    {
        yield return new WaitForSeconds(time);
        IndexWave(currentWave);
        WaveSpawn(currentSpawnPos);
    }
}
