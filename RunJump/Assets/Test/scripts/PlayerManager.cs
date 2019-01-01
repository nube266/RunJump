// 作成者:三明
// 概要：プレイヤーの基本動作
// 機能:ジャンプ，接地判定，重力処理，ボス戦到達判定，横移動，ジャンプ処理

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    private Rigidbody2D body;           // このスクリプトが適用されているRigidbody

    private void Awake() {
        body = GetComponent<Rigidbody2D> ();
        this.NotBossStart(); // ボス戦の判定
    }

    private void Update() {
        this.NotBossUpdata();      // ボス戦の判定
        this.ChangeGroundUpdata(); // 接地の状態の変更
        this.JumpUpdata();         // ジャンプ入力受付
        this.RayUpadata();         // Ray処理(横に壁があるかの判定)
        this.GrabityUpdata();      // 重力処理
        this.MoveUpdata();         // 横移動
    }

    //---------------------ボス戦に到達したかどうかの判定(先頭)---------------------//
    private float goalLineX;                //ゴールラインのx座標
    private void NotBossStart() {
        try {
            GameObject goalLineObject = GameObject.FindGameObjectWithTag("GoalLine");
            goalLineX = goalLineObject.transform.position.x;
        }
        catch {
            Debug.Log("ゴールラインが存在しません");
        }
    }
    private void NotBossUpdata() {
        if(goalLineX > this.transform.position.x) {
            moveRightEnable = true;
        }
        else {
            moveRightEnable = false;
        }
    }
    //---------------------ボス戦に到達したかどうかの判定(末尾)---------------------//

    //---------------------Ray処理(先頭)---------------------//
    [Header("Rayと衝突するマスク")]
    [SerializeField] private LayerMask rayLayerMask;  //Rayと衝突するマスク
    [Header("右方向へのRayの長さ")]
    [SerializeField] private float rightRayDistance = 1.0f;
    private void RayUpadata() {
        // 右方向へのRayの処理
        Ray rightRay = new Ray(this.transform.position, this.transform.right);  //右方向へのRay
        RaycastHit2D rightHit = Physics2D.Raycast((Vector2)rightRay.origin, (Vector2)rightRay.direction, rightRayDistance, rayLayerMask);
        if(rightHit.collider) {
            moveRightEnable = false;
        }
    }
    //---------------------Ray処理(末尾)---------------------//


    /////////////////////接地状態の判定(先頭)/////////////////////
    [Header("ジャンプ中かどうか判定するための閾値(小さいほど判定が甘くなる)")]
    [SerializeField] private float jumpThreshold = 100f;    // ジャンプ中か判定するための閾値
    private bool isGround = false;      //接地判定フラグ
    private void ChangeGroundUpdata () {
        // 接地しているかどうかの判定
        if(Mathf.Abs (body.velocity.y) > jumpThreshold) { //上下の方向における速度が閾値を超えている場合
            isGround = false;
        }
    }
    /////////////////////接地状態の判定(末尾)/////////////////////

    /////////////////////ジャンプ入力の受付(先頭)/////////////////////
    [Header("ジャンプする高さ")]
    [SerializeField] private float jumpForce = 2000f;    // ジャンプする高さ
    [Header("ジャンプする高さ(初動)")]
    [SerializeField] private float firstJumpForce = 0.5f;    // ジャンプする高さ(初動)
    private void JumpUpdata () {
        if (isGround) {
            if (Input.GetKeyDown (KeyCode.Space)) {
                this.transform.Translate(0 , firstJumpForce, 0);
                body.AddForce (Vector3.up * jumpForce);
                isGround = false;
            }
        }
    }
    /////////////////////ジャンプ入力の受付(末尾)/////////////////////


    /////////////////////着地判定(先頭)/////////////////////
    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "GroundObject") {
            if (!isGround)
                isGround = true;
        }
    }
    private void OnCollisionStay2D(Collision2D col){
        if (col.gameObject.tag == "GroundObject") {
            if(!isGround)
                isGround = true;
                this.transform.position = new Vector3 (this.transform.position.x , this.transform.position.y + 0.0001f, transform.position.z);
        }
    }
    /////////////////////着地判定(末尾)/////////////////////

    /////////////////////重力処理(先頭)/////////////////////
    [Header("重力の強さ")]
    [SerializeField] private float grabityForce = 10f;    //重力の強さ
    private void GrabityUpdata() {
        body.AddForce (Vector3.down * grabityForce);
    }
    /////////////////////重力処理(末尾)/////////////////////

    /////////////////////横移動(先頭)/////////////////////
    [Header("横移動の速度")]
    [SerializeField]private float moveSpeed = 0.5f;    //横移動の速度
    [Header("デバックモード(チェックをつけるとキー入力で移動可)")]
    [SerializeField]private  bool debugMove = false;    //デバックモードフラグ
    private bool moveRightEnable = true; //横に移動できる状態ならばtrue,そうでないならばfalse
    private void MoveUpdata() {
        if(moveRightEnable == true && debugMove == false) {
            this.transform.Translate(new Vector2 (moveSpeed, 0));
        }
        if(debugMove == true) {
            if(Input.GetKey(KeyCode.RightArrow)) {
                this.transform.Translate(new Vector2 (moveSpeed, 0));
            }
            if(Input.GetKey(KeyCode.LeftArrow)) {
                this.transform.Translate(new Vector2 (-moveSpeed, 0));
            }
        }
    }
    /////////////////////横移動(末尾)/////////////////////
}
