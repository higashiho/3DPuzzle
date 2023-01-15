using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage
{
    public class InstanceStage
    {
        /// <summary>
        /// 生成処理関数
        /// </summary>
        /// <param name="tmpStage"></param> ステージの実体
        public void StageMaking(BaseStage tmpStage)
        {
            // num:生成オブジェクト個数
            int i = 0;
            // 周回回数
            var tmpNum = 0;
            // 最大数ー1
            var tmpMaxNum = tmpStage.StagesData.TileX - 1;

            // 格納個数
            var tmpElement = 0;
            
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
                    instance(tmpStage, i, j, ref tmpSwitchFlag, tmpBoxActive, ref tmpElement);
                    
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

        /// <summary>
        /// 生成処理関数
        /// </summary>
        /// <param name="tmpStage"></param> ステージの実体
        /// <param name="i"></param>　生成ｘ座標
        /// <param name="j"></param>　生成ｙ座標
        /// <param name="tmpSwitchFlag"></param> スイッチを踏んでいるかフラグ
        /// <param name="tmpBoxActive"></param> アクティブかどうかフラグ
        /// <param name="tmpElement"></param> GoneTileの配列の要素数
        private void instance(BaseStage tmpStage, int i, int j, ref bool tmpSwitchFlag, bool tmpBoxActive, ref int tmpElement)
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
            if(!tmpBoxActive)
                tmpStage.GoneTile[tmpElement++] = tile;

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
    
        /// <summary>
        /// ステート更新関数
        /// </summary>
        /// <param name="tmpStage"></param> ステージの実体
        /// <param name="tmpMaxNum"></param> 周回最大数
        /// <param name="tmpNum"></param> 周回数
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