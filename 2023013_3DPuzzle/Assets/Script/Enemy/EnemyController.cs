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
            EnemyMove = new EnemyMove(this);
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
                        if(cts.IsCancellationRequested)
                        {
                            break;
                        }
                        await (UniTask)WaitMove;

                        
                        if(!IsStop)
                        { 
                            
                            EnemyMove.Move(cts);
                            WaitMove = null;
                        }
                    }

                    

                    // プレイヤーがステージからでたとき
                    if(!EnemyMove.EnterStage())
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