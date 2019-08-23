// 概要：モブの挙動
// 機能：死亡判定、移動、重力
// 補足説明：体力の変動は外部からChangeHpを呼び出して行う

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    private Rigidbody2D body; // このスクリプトが適用されているRigidbody
    private GameObject clearManager = null;

    [Header ("弾による攻撃でダメージが与えられるかどうか")]
    [SerializeField] private bool bulletDamageEnable = true;

    [Header ("踏むことでダメージが与えられるかどうか")]
    [SerializeField] private bool StepDamageEnable = true;

    private void Start () {
        body = GetComponent<Rigidbody2D> ();
        clearManager = GameObject.Find ("ClearManager");
        this.MoveUpDownStart ();
    }
    private void Update () {
        this.HpUpdate (); // 死亡判定
        this.MoveUpdate (); // 移動処理
        this.GravityUpdate (); // 重力処理
        this.MoveUpDownUpdate (); // 上下動
    }

    //---------------------移動開始判定(先頭)---------------------//
    [Header ("移動するかどうかのフラグ")]
    [SerializeField] private bool moveEnable = false; // 移動するオブジェクトならばtrue,そうでないならばfalse
    private bool moveStartFlag = false; // 動き出すまではfalse、動き出したあとはfalse
    private void OnBecameVisible () { // カメラ内に入ったら動き出す
        if (moveEnable == true && this.gameObject != null) { // 移動するオブジェクトならば
            moveStartFlag = true;
        }
    }

    public void MoveStart () {
        moveStartFlag = true;
    }

    public void MoveStop () {
        moveStartFlag = false;
    }
    //---------------------移動開始判定(末尾)---------------------//

    //---------------------死亡判定(先頭)---------------------//
    [Header ("体力(攻撃を耐える回数)")]
    [SerializeField] private int hp = 1; // 攻撃を耐える回数
    private void HpUpdate () {
        if (hp <= 0) {
            if (this.gameObject.tag == "Boss") {
                if (clearManager != null) {
                    clearManager.GetComponent<ClearManager> ().SetBossDied ();
                } else {
                    Debug.Log ("ClearManagerがこのシーンに存在しません");
                }
            }
            Destroy (this.gameObject); // 体力が0以下になったら消滅
        }
    }
    //---------------------死亡判定(末尾)---------------------//

    //---------------------移動処理(先頭)---------------------//
    [Header ("移動速度")]
    [SerializeField] private float moveSpeed = 0.1f; // 移動速度
    [Header ("移動方向(チェックを入れると右、入れていない場合左)")]
    [SerializeField] private bool moveDirection = false; // 移動方向(チェックを入れると右、入れていない場合左)
    private void MoveUpdate () {
        if (moveStartFlag == true) {
            if (moveDirection == true) {
                this.transform.Translate (new Vector2 (moveSpeed, 0));
            } else {
                this.transform.Translate (new Vector2 (-moveSpeed, 0));
            }
        }

    }
    //---------------------移動処理(先頭)---------------------//

    //---------------------重力処理(先頭)---------------------//
    [Header ("重力の強さ")]
    [SerializeField] private float gravityForce = 0.001f; //重力の強さ
    private void GravityUpdate () {
        body.AddForce (Vector3.down * gravityForce);
    }
    //---------------------重力処理(末尾)---------------------//

    //---------------------HPの変動(先頭)---------------------//
    public void ChangeHp (int damage) {
        hp -= damage;
    }
    //---------------------HPの変動(末尾)---------------------//

    //------------------踏まれた際にプレイヤーが飛ぶ強さを返す(先頭)------------------//
    [Header ("踏まれた際にプレイヤーが飛ぶ強さ")]
    [SerializeField] private float playerBoundForce = 2000f;
    public float GetPlayerBoundForce () {
        return this.playerBoundForce;
    }
    //------------------踏まれた際にプレイヤーが飛ぶ強さを返す(末尾)------------------//

    //------------------画面外に出た際の判定(先頭)------------------//
    private void OnBecameInvisible () {
        if (this.gameObject.tag == "Mob") {
            Destroy (this.gameObject);
        }
    }
    //------------------画面外に出た際の判定(末尾)------------------//

    //-------------弾による攻撃でダメージを与えられるかどうかを返す(先頭)-------------//
    public bool GetBulletDamageEnable () {
        return this.bulletDamageEnable;
    }
    //-------------弾による攻撃でダメージを与えられるかどうかを返す(末尾)-------------//

    //-------------弾による攻撃でダメージを与えられるようにする(先頭)-------------//
    public void SetBulletDamageEnable () {
        this.bulletDamageEnable = true;
    }
    //-------------弾による攻撃でダメージを与えられるかどうかを変更する(末尾)-------------//

    //-------------踏むことででダメージを与えられるかどうかを返す(先頭)-------------//
    public bool GetStepDamageEnable () {
        return this.StepDamageEnable;
    }
    //-------------踏むことででダメージを与えられるかどうかを返す(末尾)-------------//

    //-------------上下動(先頭)-------------//
    [Header ("上下動をするかどうか")]
    [SerializeField] private bool moveUpDownEnable = false;
    [Header ("上下動の振幅")]
    [SerializeField] private float amplitude = 0.01f;
    [Header ("上下動のスピード")]
    [SerializeField] private float moveUpDownSpeed = 0.5f;
    private float moveStartTime = 0.0f;
    private void MoveUpDownStart () {
        if (moveUpDownEnable) {
            moveStartTime = Time.frameCount;
        }
    }
    private void MoveUpDownUpdate () {
        if (moveUpDownEnable) {
            this.transform.position = new Vector3 (
                this.transform.position.x,
                this.transform.position.y + moveUpDownSpeed * Mathf.Sin ((Time.frameCount - moveStartTime) * amplitude),
                this.transform.position.z);
        }
    }
    public void SetMoveUpDownEnable () {
        this.moveUpDownEnable = true;
        this.MoveUpDownStart ();
    }
    //-------------上下動(末尾)-------------//
}