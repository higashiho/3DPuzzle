using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage
{
    public class InstanceStage
    {

        // 生成処理
        public void StageMaking(BaseStage tmpStage)
        {
            // num:生成オブジェクト個数
            int i = 0;
            // 周回回数
            var tmpNum = 0;
            // 最大数ー2
            var tmpMaxNum = tmpStage.StagesData.TileX - 2;
            
            // スイッチフラグ
            var tmpSwitchFlag = false;
            // スイッチが生成されたか
            var tmpSwitchCreate = false;
            // Activeかどうかフラグ
            var tmpBoxActive = true;
            // タイル生成個数とタイルのスケールより変数が低い場合は回す
            while (i < tmpStage.StagesData.TileX * tmpStage.PrefabTile[0].transform.localScale.x)
            {
                int j = 0; 
                while (j < tmpStage.StagesData.TileY * tmpStage.PrefabTile[0].transform.localScale.y)
                {
                    // 生成処理
                    instance(tmpStage, i, j, ref tmpSwitchFlag, tmpBoxActive);
                    
                    // タイルのスケール分値を増やす
                    j += (int)tmpStage.PrefabTile[0].transform.localScale.y;

                }
                // Activeフラグ初期化
                tmpBoxActive = true;


                // ステート更新
                stateUpdate(tmpStage, tmpMaxNum, ref tmpNum);
                // タイルのスケール分値を増やす
                i += (int)tmpStage.PrefabTile[0].transform.localScale.x;

                // スイッチが生成されたフラグがtrueの場合はもう一度足す
                if(tmpSwitchFlag && ! tmpSwitchCreate)
                {
                    tmpSwitchCreate = true;
                    // 次の行はActiveをfalseで生成
                    tmpBoxActive = false;
                }
            }
        }

        // 生成処理
        // 第一引数：ステージのベース   第二引数：生成ｘ座標    第三引数：生成ｙ座標
        private void instance(BaseStage tmpStage, int i, int j, ref bool tmpSwitchFlag, bool tmpBoxActive)
        {
            //作成
            int idx = (i + j) % tmpStage.PrefabTile.Length;
            
            //タイルとユニットのポジション
            float x = i - tmpStage.StagesData.TileX;
            float y = j - tmpStage.StagesData.TileY;

            // 座標を確定して生成
            Vector3 pos = new Vector3(x, 0, y);
            GameObject tile = MonoBehaviour.Instantiate(tmpStage.PrefabTile[idx], pos, Quaternion.identity, tmpStage.transform);

            // Active更新
            tile.SetActive(tmpBoxActive);

            // スタート位置とゴール位置のオブジェクトのタグを変更
            switch(tmpStage.InstanceStatus)
            {
                case BaseStage.InstanceState.START:
                    // 親と子のタグを変更
                    tile.tag = "StartTile";
                    tile.transform.GetChild(0).tag = "StartTile";
                    break;
                case BaseStage.InstanceState.NOMAL:
                    if(!tmpSwitchFlag)
                    {
                        //生成する個数分の１でスイッチ生成
                        var tmpNum = UnityEngine.Random.Range(0, tmpStage.StagesData.BoxsNum);
                        if(tmpNum == 0)
                        {
                            tmpSwitchFlag = true;
                            tile.transform.GetChild(0).tag = "SwitchTile";
                        }
                    }
                    break;
                case BaseStage.InstanceState.GOAL:
                    // 親と子のタグを変更
                    tile.tag = "GoalTile";
                    tile.transform.GetChild(0).tag = "GoalTile";
                    break;
                default:
                    break;
            }
        }
    
        // ステート更新
        // 第一引数：ステージのベース   第二引数：周回最大数    第三引数：周回数
        private void stateUpdate(BaseStage tmpStage, int tmpMaxNum, ref int tmpNum)
        {
             // 最後の一周はゴールステートに変更
            tmpNum++;
            if(tmpNum == tmpMaxNum)
                tmpStage.InstanceStatus = BaseStage.InstanceState.GOAL;
            
            // 最初の一回のみノーマルステートに変更
            if(tmpStage.InstanceStatus == BaseStage.InstanceState.START)
                tmpStage.InstanceStatus = BaseStage.InstanceState.NOMAL;
        }
    }
}