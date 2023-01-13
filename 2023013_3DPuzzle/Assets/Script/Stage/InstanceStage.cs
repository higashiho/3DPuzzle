using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceStage
{

    // 生成処理
    public void StageMaking(BaseStage tmpStage)
    {
        // num:生成オブジェクト個数
        int i = 0, num = 0;
        // タイル生成個数とタイルのスケールより変数が低い場合は回す
        while (i < tmpStage.StagesData.TileX * tmpStage.PrefabTile[0].transform.localScale.x)
        {
            int j = 0; 
            while (j < tmpStage.StagesData.TileY * tmpStage.PrefabTile[0].transform.localScale.y)
            {
                //作成
                int idx = (i + j) % tmpStage.PrefabTile.Length;
                
                //タイルとユニットのポジション
                float x = i - tmpStage.StagesData.TileX;
                float y = j - tmpStage.StagesData.TileY;

                Vector3 pos = new Vector3(x, 0, y);
                GameObject tile = MonoBehaviour.Instantiate(tmpStage.PrefabTile[idx], pos, Quaternion.identity, tmpStage.transform);

                if(false);
                tmpStage.Tiles[num++] = tile;
                // タイルのスケール分値を増やす
                j += (int)tmpStage.PrefabTile[0].transform.localScale.y;

            }
            // タイルのスケール分値を増やす
            i += (int)tmpStage.PrefabTile[0].transform.localScale.x;
        }
    }
}
