using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{

    public class EnemyController : BaseEnemy
    {
        void Start()
        {
            enemyMove = new EnemyMove(this);
        }

        void Update()
        {
            switch(EnemyState)
            {

                case EnemyPhase.Move:

                    enemyMove.Move(cts);

                    // プレイヤーがステージからでたとき
                    if(!enemyMove.EnterStage())
                    {
                        EnemyState = EnemyPhase.Reset;
                        this.gameObject.SetActive(false);
                    }
                    break;
                case EnemyPhase.Reset:
                    
                    if(this.gameObject.activeSelf)
                    {
                        EnemyState = EnemyPhase.Move;
                    }
                    
                    break;
            }
            
            
        }
    }
}