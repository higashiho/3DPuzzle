using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stage;

namespace Box
{
    /// <summary>
    /// ボックス生成の関数管理クラス
    /// </summary>
    public class InstanceBox
    {
        /// <summary>
        /// Box生成関数
        /// </summary>
        /// <param name="tmpStage">ステージの実体</param> 
        // public void CreateBox(BaseStage tmpStage)
        // {
        //     // インスタンス化した場所保管用
        //     int[] tmpNum = new int[InGameSceneController.Stages.StagesData.BoxsNum];
        //     for(int i = 0;i < InGameSceneController.Stages.StagesData.BoxsNum; i++)
        //     {
        //         var tmpCheck = false;
        //         var num = UnityEngine.Random.Range(0,InGameSceneController.Stages.Tiles.Length);

        //         // 一度生成している要素数ならやり直し
        //         for(int j = 0; j < tmpNum.Length; j++)
        //         {
        //             if(tmpNum[j] == num)
        //             {
        //                 Debug.Log("continue");
        //                 tmpCheck = true;
        //             }
        //             if(tmpCheck)
        //                 break;
        //         }
        //         if(tmpCheck)
        //             continue;

        //         // 生成したことないnumの場合保管して生成
        //         tmpNum[i] = num;
        //         MonoBehaviour.Instantiate(
        //             tmpStage.PerfabBox, 
        //             tmpStage.PerfabBox.transform.position + InGameSceneController.Stages.Tiles[num].transform.position,
        //             Quaternion.identity,
        //             tmpStage.Boxs.transform
        //         );
        //     }
        // }
    }
}