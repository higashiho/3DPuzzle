using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceBox
{
    // Box生成
    public void CreateBox(BaseBox tmpBox)
    {
        int i = 0; 
        // インスタンス化した場所保管用
        int[] tmpNum = new int[InGameSceneController.Stages.StagesData.BoxsNum];
        while(i < InGameSceneController.Stages.StagesData.BoxsNum)
        {
            var num = UnityEngine.Random.Range(0,InGameSceneController.Stages.Tiles.Length);

            // 一度生成している要素数ならやり直し
            for(int j = 0; j < tmpNum.Length; j++)
                if(tmpNum[j] == num)
                    continue;

            // 生成したことないnumの場合保管して生成
            tmpNum[i] = num;
            MonoBehaviour.Instantiate
            (
                tmpBox.PerfabBox, 
                tmpBox.PerfabBox.transform.position + InGameSceneController.Stages.Tiles[num].transform.position,
                Quaternion.identity,
                tmpBox.transform
            );
            
            i++;
        }
    }
}
