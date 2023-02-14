using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Enemy
{

    public class EnemyController : BaseEnemy
    {
        void Start()
        {
            enemyMove = new EnemyMove(this);
            B_ResetPlayer = false;
            IsStop = false;
        }

        async void Update()
        {
            switch(EnemyState)
            {

                case EnemyPhase.Move:

                    if(WaitMove == null)
                    {
                        WaitMove = UniTask.Delay(EnemyDatas.MoveInterval * Const.CHANGE_SECOND, cancellationToken: cts.Token);
                        await (UniTask)WaitMove;

                        if(!IsStop)
                        { 
                            enemyMove.Move(cts);
                            WaitMove = null;
                        }
                    }

                    

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
                        IsStop = false;
                    }
                    
                    break;
            }
            
            
        }
    }
}