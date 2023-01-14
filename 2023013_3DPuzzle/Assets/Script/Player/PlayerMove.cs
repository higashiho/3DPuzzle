using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class PlayerMove 
{
    
    public async void Move(BasePlayer tmpPlayer)
    {
        // 目的地初期化(最初のプレイヤーの位置)
        Vector3 destination = new Vector3(0,1.3f,0);
        //bool setDestination = false;()
        if(Input.GetMouseButtonDown(0) && tmpPlayer.ChooseObj)
        {
            destination = tmpPlayer.ChooseObj.transform.position + new Vector3(0, Const.PLAYER_POSY, 0);
            //setDestination = true;
            var tmpPlayerTween = tmpPlayer.transform.DOMove(destination, tmpPlayer.PlayersData.PlayerMoveTime).SetEase(Ease.Linear)
            .OnStart(() => startMove(tmpPlayer));

            await tmpPlayerTween.AsyncWaitForCompletion();
            tmpPlayer.MoveCounter.GetComponent<MoveCounter>().MoveCount++;
            compMove(tmpPlayer);
        }

           
    }

    private void startMove(BasePlayer tmpPlayer)
    {
        tmpPlayer.ChooseObj = null;
        tmpPlayer.OnMove = true;
    }

    private void compMove(BasePlayer tmpPlayer)
    {
        tmpPlayer.OnMove = false;
    }
}
