/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STAGEMANAGERTEST : MonoBehaviour
{
    public LevelManagerScriptable level1;
    private int currentLevel = 0;
    private int currentWave = 0;

    private void Start()
    {
        //Delay 1s to Spawn Wave 1:
        StartCoroutine(WaitForNewWave(1.5f));
    }

    void SpawnEntities()
    {
        if (currentLevel == 0)
        {
            if (level1.enemyTypeInWave.Length == 1)
            {
                for (int i = 0; i < level1.amountEachEnemyTypePerWave[currentWave]; i++)
                {
                    // Creates an instance of the prefab at the current spawn point.
                    GameObject currentEntity = Instantiate(level1.enemyTypeInWave[currentWave], level1.spawnPos[currentWave], Quaternion.identity);
                }
            }

            else if (level1.enemyTypeInWave.Length == 2)
            {
                //Spawn Enemy Type 1 theo Level:
                for (int i = 0; i < level1.amountEachEnemyTypePerWave[currentWave]; i++)
                {
                    // Creates an instance of the prefab at the current spawn point.
                    GameObject currentEntity = Instantiate(level1.enemyTypeInWave[currentWave], level1.spawnPos[currentWave], Quaternion.identity);
                }

                //Spawn Enemy Type 2 theo Level:
                for (int i = 0; i < level1.amountEachEnemyTypePerWave[currentWave + 1]; i++)
                {
                    // Creates an instance of the prefab at the current spawn point.
                    GameObject currentEntity = Instantiate(level1.enemyTypeInWave[currentWave + 1], level1.spawnPos[currentWave], Quaternion.identity);
                }

            }

            currentWave++;

            if (currentWave >= level1.maxWave)
            {
                //End Level + Return To Screen...
            }
        }
    }

    IEnumerator WaitForNewWave(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnEntities();
    }
}
*/