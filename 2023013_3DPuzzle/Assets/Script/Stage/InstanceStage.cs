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
        /// <param name="tmpStage">ステージの実体</param> 
        public void StageMaking(BaseStage tmpStage, int tmpPosY)
        {
            // 最大数ー1
            var tmpMaxNum = tmpStage.StagesData.TileX;
            tmpMaxNum--;

            // 格納個数
            var tmpElement = 0;
            
            // スイッチフラグ
            var tmpSwitchFlag = false;
            // スイッチが生成されたか
            var tmpSwitchCreate = false;
            // Activeかどうかフラグ
            var tmpBoxActive = true;
            // タイル生成個数とタイルのスケールより変数が低い場合は回す
            for (int i = 0; i < tmpStage.StagesData.TileX; i++)
            {
                // 一週目は更新が必要ないので更新しない
                if(i != 0)
                    // ステート更新
                    stateUpdate(tmpStage, tmpMaxNum, i);
                for (int j = 0; j < tmpStage.StagesData.TileZ; j++)
                {
                    // 生成処理
                    instance(tmpStage, i, j, tmpPosY, tmpBoxActive, ref tmpSwitchFlag, ref tmpElement);

                }
                // Activeフラグ初期化
                tmpBoxActive = true;


                // スイッチが生成されたフラグがtrueの場合はもう一度足す
                if(tmpSwitchFlag && !tmpSwitchCreate)
                {
                    tmpSwitchCreate = true;
                    // 次の行はActiveをfalseで生成
                    tmpBoxActive = false;
                }
            }
            // 初期化
            tmpStage.InstanceStatus = BaseStage.InstanceState.START;
        }

        /// <summary>
        /// 生成処理関数
        /// </summary>
        /// <param name="tmpStage">ステージの実体</param> 
        /// <param name="i">生成ｘ座標</param>　
        /// <param name="j">生成ｙ座標</param>　
        /// <param name="tmpSwitchFlag">スイッチを踏んでいるかフラグ</param> 
        /// <param name="tmpBoxActive">アクティブかどうかフラグ</param> 
        /// <param name="tmpElement">GoneTileの配列の要素数</param> 
        private void instance(
            BaseStage tmpStage, int i, int j, int tmpPosY, bool tmpBoxActive, ref bool tmpSwitchFlag,  ref int tmpElement
            )
        {
            // 弾数ー１
            var tmpMaxNum = tmpStage.StagesData.TileY;
            tmpMaxNum--;

            // 作成個数のの半分
            var tmpHalfNum = tmpStage.StagesData.TileX >> 1;
            // ０～９のため１減らす
            tmpHalfNum--;
            // 上段と下段以外は生成しない
            if(tmpPosY > 0 && tmpPosY < tmpMaxNum)
                return;
            // 上段の場合は半分のみ作成
            if(tmpPosY == tmpMaxNum)
            {
                Debug.Log(tmpHalfNum);
                if(i <= tmpHalfNum)
                    return;
            }
            //作成
            int idx = (i + j) % tmpStage.PrefabTile.Length;
            
            //タイルとユニットのポジション
            float x = (i * (int)tmpStage.PrefabTile[idx].transform.localScale.x) - tmpStage.StagesData.TileX;
            float z = (j * (int)tmpStage.PrefabTile[idx].transform.localScale.z) - tmpStage.StagesData.TileZ;
            float y = (tmpPosY * (int)tmpStage.PrefabTile[idx].transform.localScale.z);
            // 座標を確定して生成
            Vector3 pos = new Vector3(x, y, z);
            GameObject tile = MonoBehaviour.Instantiate(
                tmpStage.PrefabTile[idx], pos, Quaternion.identity, tmpStage.TileParemt.transform
                );

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
                // スタンプが作成されていなくて一段目の場合
                    if(!tmpSwitchFlag && tmpPosY == 0)
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
        /// <param name="tmpStage">ステージの実体</param> 
        /// <param name="tmpMaxNum">周回最大数</param> 
        /// <param name="tmpNum">周回数</param> 
        private void stateUpdate(BaseStage tmpStage, int tmpMaxNum, int tmpNum)
        {
             // 最後の一周はゴールステートに変更
            if(tmpNum == tmpMaxNum)
                tmpStage.InstanceStatus = BaseStage.InstanceState.GOAL;
            
            // 最初の一回のみノーマルステートに変更
            if(tmpStage.InstanceStatus == BaseStage.InstanceState.START)
                tmpStage.InstanceStatus = BaseStage.InstanceState.NOMAL;
        }
    }
}