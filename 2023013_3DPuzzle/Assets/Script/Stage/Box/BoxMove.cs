using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Box
{
    public class BoxMove
    {
        /// <summary>
        /// y座標固定関数
        /// </summary>
        /// <param name="tmpBox"></param> ボックスの実体
        public void FixationPosY(BaseBox tmpBox)
        {
            // y座標を一定値で固定する
            var tmpPos = tmpBox.transform.position;
            tmpPos.y = (float)tmpBox.PosY;
            tmpBox.transform.position = tmpPos;

            // 移動中は色変更
            if(InGameSceneController.Stages.Moving)
                tmpBox.GetComponent<Renderer>().material.color = Color.magenta;
        }
        
        /// <summary>
        /// 挙動関数
        /// </summary>
        /// <param name="tmpBox"></param> ボックスの実体
        public void Move(BaseBox tmpBox)
        {
            if(chack(tmpBox))  
            {
                // 移動不可オブジェクトじゃなくて挙動中じゃない場合
                if(tmpBox.GetComponent<Renderer>().material.color != Color.yellow && !InGameSceneController.Stages.Moving)
                {
                    tmpBox.GetComponent<Renderer>().material.color = Color.green;
                    if(Input.GetMouseButtonDown(1))
                        behavior(tmpBox);
                }
            }
            else   
                tmpBox.GetComponent<Renderer>().material.color = tmpBox.StartColor;
                

        }

        /// <summary>
        /// プレイヤーとの接地判定関数
        /// </summary>
        /// <param name="tmpBox"></param> ボックスの実体
        /// <returns></returns> 接地しているかどうか
        private bool chack(BaseBox tmpBox)
        {
            // 自分の座標とプレイヤーの座標を比較
            var tmpPos = tmpBox.transform.position - InGameSceneController.Player.transform.position;

            // 誤差で計算が出来ないためイントにキャスト
            var tmpPosX = Mathf.RoundToInt(tmpPos.x);
            var tmpPosZ = Mathf.RoundToInt(tmpPos.z);
            // プレイヤーの周りにいる確認
            if(tmpPosX == Const.CHECK_POS_X || tmpPosX == -Const.CHECK_POS_X || tmpPosZ == Const.CHECK_POS_Z || tmpPosZ == -Const.CHECK_POS_Z)
            {

                // プレイヤーの隣にいるか確認
                if(tmpPosX == 0 || tmpPosZ == 0)   
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Boxの挙動関数
        /// </summary>
        /// <param name="tmpBox"></param> ボックスの実体
        private void behavior(BaseBox tmpBox)
        {
            // 動いているフラグを立てる
            InGameSceneController.Stages.Moving = true;

            // プレイヤーの座標一時保管
            var tmpPlayerPos = InGameSceneController.Player.transform.position;
            // ボックスの座標保管
            var tmpTileObj = tmpBox.Tile;


            // プレイヤーを自身の方向に向けて移動させる
            InGameSceneController.Player.transform.LookAt(tmpBox.transform);
            // x座標を固定
            var tmpAngle = InGameSceneController.Player.transform.localEulerAngles;
            tmpAngle.x = 0;
            InGameSceneController.Player.transform.localEulerAngles = tmpAngle;
            
            // 移動
            var tmpPos = tmpBox.transform.position;
            tmpPos.y = InGameSceneController.Player.transform.position.y;
            InGameSceneController.Player.transform.DOMove(
                tmpPos, 
                InGameSceneController.Player.PlayersData.PlayerMoveTime
            ).SetEase(Ease.OutSine).OnComplete(() => 
            {
                // 座標をそろえる
                InGameSceneController.Player.transform.position = tmpPos;
                compMove(tmpBox, tmpPlayerPos, tmpTileObj);
            });
            
        }

        /// <summary>
        /// プレイヤーがBoxを押す動作が終わった時の関数
        /// </summary>
        /// <param name="tmpBox"></param> ボックスの実体
        /// <param name="tmpPos"></param> 押す前のプレイヤーの座標
        /// <param name="tmpTileObj"></panam> 押す前のボックスの位置にあるタイル
        private void compMove(BaseBox tmpBox, Vector3 tmpPlayerPos, GameObject tmpTileObj)
        {
            
            // Boxの座標移動
            var tmpNewBoxPos = tmpBox.Tile.transform.position;
            tmpNewBoxPos.y = Const.BOX_POS_Y;

            // 移動開始
            tmpBox.transform.DOMove(
                tmpNewBoxPos,
                Const.BOX_MOVE_SPEED
                ).SetEase(Ease.Linear).OnComplete(() =>
                {
                    // 初期化
                    compReset(tmpBox);

                    // 座標をそろえる
                    tmpBox.transform.position = tmpNewBoxPos;

                    // ボックスの位置が動いていなかったらプレイヤーを動作前の位置に戻す
                    if(tmpBox.Tile == tmpTileObj)
                    {
                        var tmpPosX = Mathf.RoundToInt(tmpPlayerPos.x);
                        var tmpPosY = Mathf.RoundToInt(tmpPlayerPos.y);
                        var tmpPosZ = Mathf.RoundToInt(tmpPlayerPos.z);
                        var tmpPlayerNewPos = new Vector3(tmpPosX, tmpPosY, tmpPosZ);
                        InGameSceneController.Player.transform.position = tmpPlayerNewPos;
                    }
                    Debug.Log("MoveComp");
                });
        }

        /// <summary>
        /// 挙動終了時の初期化関数
        /// </summary>
        /// <param name="tmpBox"></param> ボックスの実体
        private void compReset(BaseBox tmpBox)
        {

            // 初期化
            tmpBox.transform.SetParent(tmpBox.Parent.transform);
            tmpBox.PosY = null;
            InGameSceneController.Stages.Moving = false;
            InGameSceneController.Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            tmpBox.GetComponent<Renderer>().material.color = tmpBox.StartColor;


            // ヴェロシティに値が入っている場合がある為初期化
            var tmpRb = tmpBox.GetComponent<Rigidbody>();
            tmpRb.velocity = Vector3.zero;

            // ボックスアクティブフラグが経っている場合全てのGoneTileのActiveをtrueにする
            if(tmpBox.TileActiveFlag)
            { 
                foreach(GameObject tmpObj in InGameSceneController.Stages.GoneTile)
                {
                    // 消えているオブジェクトを表示させて配列に格納
                    if(!tmpObj.activeSelf)
                    {
                        tmpObj.SetActive(true);
                    }
                }
            }
        }
    }
}