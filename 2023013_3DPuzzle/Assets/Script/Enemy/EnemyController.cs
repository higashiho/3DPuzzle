using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseEnemy
{
    void Start()
    {
        enemyMove = new EnemyMove(this);
    }
}
