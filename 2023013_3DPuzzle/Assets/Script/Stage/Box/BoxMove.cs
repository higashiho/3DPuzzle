using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Box
{
    /// <summary>
    /// ボックスの挙動関数管理クラス
    /// </summary>
    public class BoxMove
    {
       
        
        /// <summary>
        /// 挙動関数
        /// </summary>
        /// <param name="tmpBox"></param> ボックスの実体
        public void Move(BaseBox tmpBox)
        {
            if(Chack(tmpBox.gameObject))  
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
        public bool Chack(GameObject tmpBox)
        {
            // 自分の座標とプレイヤーの座標を比較
            Vector3 tmpPos = tmpBox.transform.position - InGameSceneController.Player.transform.position;

            // 誤差で計算が出来ないためイントにキャスト
            tmpPos.x = Mathf.RoundToInt(tmpPos.x);
            tmpPos.z = Mathf.RoundToInt(tmpPos.z);
            // プレイヤーの周りにいる確認
            if(tmpPos.x == Const.CHECK_POS_X || tmpPos.x == -Const.CHECK_POS_X || tmpPos.z == Const.CHECK_POS_Z || tmpPos.z == -Const.CHECK_POS_Z)
            {

                // プレイヤーの隣にいるか確認
                if(tmpPos.x == 0 || tmpPos.z == 0)   
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Boxの挙動関数
        /// </summary>
        /// <param name="tmpBox">ボックスの実体</param> 
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
        /// <param name="tmpBox">ボックスの実体</param>
        /// <param name="tmpPlayerPos">押す前のプレイヤーの座標</param>
        /// <param name="tmpTileObj">押す前のボックスの位置にあるタイル</param>
        private void compMove(BaseBox tmpBox, Vector3 tmpPlayerPos, GameObject tmpTileObj)
        {
            
            // Boxの座標移動
            var tmpNewBoxPos = tmpBox.Tile.transform.position;
            tmpNewBoxPos.y = tmpBox.transform.position.y;

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
        /// <param name="tmpBox">ボックスの実体</param>
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
                           
                // foreach(GameObject tmpObj in InGameSceneController.Stages.GoneTile)
                // {
                //     // 消えているオブジェクトを表示させて配列に格納
                //     if(!tmpObj.activeSelf)
                //     {
                //         tmpObj.SetActive(true);
                //     }
                // }
            }
        }
    }
}