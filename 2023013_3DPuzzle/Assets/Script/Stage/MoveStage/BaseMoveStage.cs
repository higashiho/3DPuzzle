using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Stage
{
    /// <summary>
    /// 動かせるステージのベースクラス
    /// </summary>
    public class BaseMoveStage : MonoBehaviour
    {
        // 初期マテリアルカラー取得
        [SerializeField, Header("初期色")]
        protected Color startColor;
        public Color StartColor{get{return startColor;}set{startColor = value;}}

        // 挙動前の座標
        protected Vector3 lastAngle;
        public Vector3 LastAngle{get{return lastAngle;}set{lastAngle = value;}}

        // 挙動中のTween
        protected Tween nowTween = null;
        public Tween NowTween{get{return nowTween;}set{nowTween = value;}}

        /// <summary>
        /// 挙動ステージ管理ステート
        /// </summary>
        protected uint moveStageState = Const.STATE_STAND_UP;
        /// <summary>
        /// 挙動ステージ管理ステートプロパティ
        /// </summary>
        public uint MoveStageState{get{return moveStageState;}set{moveStageState = value;}}


        //マウスカーソルが階段に乗った時の処理
        private void OnMouseOver()
        {
            scaleMode.Move(this);
        }

        //マウスカーソルが階段の上から離れた時の処理
        private void OnMouseExit()
        {
            //Sphereの色が元の色に戻す
            this.GetComponent<Renderer>().material.color = startColor;

        }


        // インスタンス化
        protected MoveStageMove scaleMode = new MoveStageMove();
    }
}