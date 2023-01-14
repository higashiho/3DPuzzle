using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMove
{
    // 挙動
    public void Move(BaseBox tmpBox)
    {
        if(chack(tmpBox))  
            tmpBox.GetComponent<Renderer>().material.color = Color.green;
        else   
            tmpBox.GetComponent<Renderer>().material.color = tmpBox.StartColor;
            

    }

    // プレイヤーの隣にいるか確認
    private bool chack(BaseBox tmpBox)
    {
        // 自分の座標とプレイヤーの座標を比較
        var tmpPos = tmpBox.transform.position - InGameSceneController.Player.transform.position;

        // 誤差で計算が出来ないためイントにキャスト
        int tmpPosX = (int)Mathf.Round(tmpPos.x);
        int tmpPosZ = (int)Mathf.Round(tmpPos.z);
        // プレイヤーの周りにいる確認
        if(tmpPosX == Const.CHECK_POS_X || tmpPosX == -Const.CHECK_POS_X || tmpPosZ == Const.CHECK_POS_Z || tmpPosZ == -Const.CHECK_POS_Z)
        {

            // プレイヤーの隣にいるか確認
            if(tmpPosX == 0 || tmpPosZ == 0)   
                return true;
        }
        return false;
    }
}
