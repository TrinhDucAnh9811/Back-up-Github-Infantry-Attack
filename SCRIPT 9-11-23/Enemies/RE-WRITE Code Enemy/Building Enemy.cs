using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingEnemy: EnemyBase
{

    private void OnEnable()
    {
        maxHp = 200;
        currentHp = maxHp;
    }


    protected override void EnemyMovement()
    {
    }

    protected override void EnemyAttack(float attackSpeed, float bulletSpeed)
    {
    }
}
