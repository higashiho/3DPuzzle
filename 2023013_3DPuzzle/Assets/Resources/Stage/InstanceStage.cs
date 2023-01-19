using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Stage
{
    
    /// <summary>
    /// ステージ生成の関数管理クラス
    /// </summary>
    public class InstanceStage
    {
        
        
        /// <summary>
        /// 生成処理関数
        /// </summary>
        /// <param name="tmpStage">ステージの実体</param> 
        /// <param name="tmpDataName">生成するステージのCSVファイルのアドレス</param> 
        public void StageMaking(BaseStage tmpStage, string tmpDataName)
        {
            // CSVファイルデータ読み取り
            TextAsset tmpCsvFile = Resources.Load<TextAsset>(tmpDataName);
            StringReader tmpReader = new StringReader(tmpCsvFile.text);
            List<string[]> tmpStageDatas = new List<string[]>();
            // 行列確認用変数
            int maxX = -1;
            int maxZ = -1;

            // ロードしたstring格納用
            string tmpLine = null;
            //CSVファイルの読み出し
            while (tmpReader.Peek() != -1)
            {
                
                // 行数カウント追加
                maxZ++;
                // 一行ずつ読み込み
                tmpLine = tmpReader.ReadLine();

                tmpStageDatas.Add(tmpLine.Split(','));
                
            }

            // ロードが出来ていたら列数を取得
            if(tmpLine != null)
                maxX = countChar(tmpLine, ',');
            Debug.Log("maxX = " + maxX);
            Debug.Log("maxZ = " + maxZ);
            // 生成
            instance(tmpStage, maxX, maxZ, tmpStageDatas);
        }

        /// <summary>
        /// ステージ生成関数
        /// </summary>
        /// <param name="tmpStage">ステージの実体</param>
        /// <param name="x">列最大値</param>
        /// <param name="z">行最大値</param>
        /// <param name="tmpStageData">生成するステージのデータリスト</param>
        /// <param name="tmpDataName">生成するステージのCSVファイルのアドレス</param>
        private void instance(BaseStage tmpStage, int x,int z, List<string[]> tmpStageData)
        {
            for(int i = 0; i <= x; i++)
            {
                for(int j = 1; j <= z; j++)
                {
                    // ファイルの０の値以外は生成
                    if(tmpStageData[j][i] != "None")
                        createStageObject(tmpStage, i, z - j, tmpStageData[j][i]);
                }
            }
        }

        /// <summary>
        /// オブジェクト生成関数
        /// </summary>
        /// <param name="tmpStage">ステージの実体</param>
        /// <param name="x">生成x座標</param>
        /// <param name="y">生成y座標</param>
        /// <param name="objName">生成するオブジェクト名</param>
        private void createStageObject(BaseStage tmpStage, int x, int z, string objName)
        {

            Debug.Log(objName);
            // 読み込んだオブジェクトをゲームオブジェクト型に変更
            var tmpNum = int.Parse(objName);
            var obj = MonoBehaviour.Instantiate(
                tmpStage.Stages[tmpNum], 
                new Vector3(tmpStage.Stages[tmpNum].transform.localScale.x * x, 0,  tmpStage.Stages[tmpNum].transform.localScale.z * z), 
                Quaternion.identity, tmpStage.TileParemt.transform);
        }

        /// <summary>
        /// xの文字数を数える関数
        /// </summary>
        /// <param name="s">CSVで読み込んだ文字列</param>
        /// <param name="c">区切り文字</param>
        /// <returns>xの文字列数</returns>
        private int countChar(string s, char c)
        {
            return s.Length - s.Replace(c.ToString(), "").Length;
        }
    }
}