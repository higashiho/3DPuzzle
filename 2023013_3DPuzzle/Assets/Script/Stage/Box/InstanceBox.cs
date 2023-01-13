using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceBox
{
    // Box生成
    public void CreateBox(BaseStage tmpStage)
    {
        int i = 0; 
        // インスタンス化した場所保管用
        int[] tmpNum = new int[InGameSceneController.Stages.StagesData.BoxsNum];
        while(i < InGameSceneController.Stages.StagesData.BoxsNum)
        {
            var tmpCheck = false;
            var num = UnityEngine.Random.Range(0,InGameSceneController.Stages.Tiles.Length);

            // 一度生成している要素数ならやり直し
            for(int j = 0; j < tmpNum.Length; j++)
            {
                if(tmpNum[j] == num)
                {
                    Debug.Log("continue");
                    tmpCheck = true;
                }
                if(tmpCheck)
                    break;
            }
            if(tmpCheck)
                continue;

            // 生成したことないnumの場合保管して生成
            tmpNum[i++] = num;
            MonoBehaviour.Instantiate(
                tmpStage.PerfabBox, 
                tmpStage.PerfabBox.transform.position + InGameSceneController.Stages.Tiles[num].transform.position,
                Quaternion.identity,
                tmpStage.Boxs.transform
            );
        }
    }
}
