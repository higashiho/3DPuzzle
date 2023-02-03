using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stage;
using DG.Tweening;

namespace Tile
{
    /// <summary>
    /// タイル挙動関数管理クラス
    /// </summary>
    public class TileMove
    {
        // インスタンス化
        private NeedleMove needleMove = null;
        private StageMove stageMove = new StageMove();
        private TileMove tileMove = null;
        private FallTileMove fallTileMove = null;
        private SwitchTileMove switchTileMove = null;
        private MoveStageMove moveStage = null;

        private BaseTile tmpTile;
        public TileMove(BaseTile tmp)
        {
            tmpTile = tmp;
        }
        /// <summary>
        /// クリア回数が１回じゃないステージ用関数
        /// </summary>
        public void ChangeSwitchTile()
        {
            InGameSceneController.Stages.ClearCount--;

            GameObject tmpObj = null;
            float nearDis = default;
            
            // プレイヤーから遠いオブジェクトを探索
            foreach(var tmp in InGameSceneController.FallTile.FallTiles)
            {
                if(!tmp)
                    break;
                
                if(tmp.activeSelf)
                {
                    if(!tmpObj)
                        tmpObj = tmp;
                    else
                    {
                        var tmpDis = Vector3.Distance(tmp.transform.position, InGameSceneController.Player.transform.position);
                        if(tmpDis > nearDis)
                        {
                            nearDis = tmpDis;
                            tmpObj = tmp;
                        }
                    }
                }
            }

            // オブジェクト変更
            changeTile(tmpObj);

        }

        /// <summary>
        /// オブジェクト変更関数
        /// </summary>
        /// <param name="tmpObj">変更対象オブジェクト</param>
        private void changeTile(GameObject tmpObj)
        {
            tmpTile.gameObject.tag = "Tiles";
            var tmpMaterialRenderer = InGameSceneController.Stages.TileMaterial;
            tmpMaterialRenderer.color = Color.white;
            tmpTile.gameObject.GetComponent<Renderer>().material = tmpMaterialRenderer;

            tmpObj.tag = "KeyTile";
            tmpMaterialRenderer = InGameSceneController.Stages.KeyTileMaterial;
            tmpMaterialRenderer.color = Color.magenta;
            tmpObj.GetComponent<Renderer>().material = tmpMaterialRenderer;
            InGameSceneController.Stages.TileChangeFlag = false;
        }

        

        /// <summary>
        /// ステージClear挙動
        /// </summary>
        private void stageClearMove()
        {
            // インスタンス化がされていない場合はインスタンス化をする
            if(tileMove == null)
                tileMove = new TileMove(tmpTile);
            if(fallTileMove == null)
                fallTileMove = new FallTileMove(InGameSceneController.FallTile);
            if(switchTileMove == null)
                switchTileMove = new SwitchTileMove(InGameSceneController.SwitchTile);
            if(needleMove == null)
                needleMove = new NeedleMove(InGameSceneController.Player.Needle);
            if(moveStage == null)
                moveStage = new MoveStageMove(InGameSceneController.MoveStage);
            
            // どのステージからの帰還か判断
            switch(InGameSceneController.Stages.StageState)
            {
                // 左上ステージ
                case StageConst.STATE_NEEDLE_STAGE:
                    getNum(ref InGameSceneController.Stages.StageClearFlags[0]);
                    stageMove.StageClear();
                    needleMove.ResetTile();
                    break;
                // 左下ステージ
                case StageConst.STATE_MOVE_STAGE:
                    getNum(ref InGameSceneController.Stages.StageClearFlags[1]);
                    stageMove.StageClear();
                    moveStage.ResetMoveStage();
                    break;
                // 右上ステージ
                case StageConst.STATE_FALLING_STAGE:
                    // カウントが1以下になったらクリア処理
                    if(InGameSceneController.Stages.ClearCount <= StageConst.GOAL_TILE_NUM)
                    {
                        getNum(ref InGameSceneController.Stages.StageClearFlags[2]);
                        InGameSceneController.FallTile.TaskChancelFlag = true; 
                        DOTween.Kill(InGameSceneController.FallTile.WarningPanel);
                        stageMove.StageClear();
                        fallTileMove.FallTileReset();
                    }
                    // プレイヤーから一番遠いタイルをスイッチタイルに変更
                    else if(InGameSceneController.Stages.TileChangeFlag)
                    {
                        Debug.Log("In");
                        tileMove.ChangeSwitchTile();
                    }
                    break;
                // 右下ステージ
                case StageConst.STATE_SWITCH_STAGE:
                    getNum(ref InGameSceneController.Stages.StageClearFlags[3]);
                    stageMove.StageClear();
                    switchTileMove.SwitchTileReset();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// プレイヤーの所持数値に値を入れてクリアしたかフラグを立てる
        /// </summary>
        /// <param name="tmpflag">立てるフラグ</param>
        private void getNum(ref bool tmpflag)
        {
            tmpflag = true;
            // ０の値を探索して０の位置に数値格納
            for(int i = 0; i < InGameSceneController.Player.HaveNum.Count; i++)
            {
                if(InGameSceneController.Player.HaveNum[i] == 0)
                {
                    InGameSceneController.Player.HaveNum[i] = 
                        (uint)UnityEngine.Random.Range(StageConst.MIN_NUM, StageConst.MAX_NUM);

                    break;
                }
            }
        }

        /// <summary>
        /// キータイルか判断用関数
        /// </summary>
        /// <param name="col">当たった対象</param>
        /// <param name="tmpObj">自身のオブジェクト</param>
        public void KeyTileCollsionMove(Collision col, GameObject tmpObj)
        {
            // Playerと当たった時に自分がスイッチの場合
            if(col.gameObject.tag == "Player" && tmpObj.gameObject.tag == "KeyTile")
            {
                Debug.Log("Stay");
                if(!InGameSceneController.Player.OnMove && InGameSceneController.Player.PlayerClearTween == null)
                    stageClearMove();
            }
        }
    }
}
