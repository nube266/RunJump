// 概要：プレイヤーの基本動作
// 機能:ジャンプ，接地判定，重力処理，ボス戦到達判定，横移動，ジャンプ処理、画面外に出た場合の死亡判定

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    private Rigidbody2D body; // このスクリプトが適用されているRigidbody
    private GameObject cameraObject; // カメラのゲームオブジェクト
    [Header ("通常状態のレイヤーb暗号")]
    [SerializeField] private int normalPlayerLayerNumber = 30;
    [Header ("無敵状態のレイヤー番号")]
    [SerializeField] private int invinciblePlayerLayerNumber = 31;

    private void Awake () {
        body = GetComponent<Rigidbody2D> ();
        this.NotBossStart (); // ボス戦の判定
        cameraObject = GameObject.FindGameObjectWithTag ("MainCamera");
        this.gameObject.layer = normalPlayerLayerNumber;
        if (debugMove == true) {
            cameraObject.GetComponent<CameraManager> ().SetDebugMoveMode ();
        }
    }

    private void Update () {
        this.NotBossUpdata (); // ボス戦の判定
        this.ChangeGroundUpdata (); // 接地の状態の変更
        this.JumpUpdata (); // ジャンプ入力受付
        this.WallHitUpadata (); // 横に壁があるかの判定
        this.EnemyHitUpdata (); // 敵との当たり判定
        this.TreadOnEnemyUpdata (); // 敵を踏んだ時の判定
        this.GravityUpdata (); // 重力処理
        this.MoveUpdata (); // 横移動
        this.DamageUpdata (); // ダメージ処理
    }

    //---------------------ボス戦に到達したかどうかの判定(先頭)---------------------//
    private float goalLineX; //ゴールラインのx座標
    private void NotBossStart () {
        try {
            GameObject goalLineObject = GameObject.FindGameObjectWithTag ("GoalLine");
            goalLineX = goalLineObject.transform.position.x;
        } catch {
            Debug.Log ("ゴールラインが存在しません");
        }
    }
    private void NotBossUpdata () {
        if (goalLineX > this.transform.position.x) {
            moveRightEnable = true;
        } else {
            cameraObject.GetComponent<CameraManager> ().CameraStop ();
            moveRightEnable = false;
        }
    }
    //---------------------ボス戦に到達したかどうかの判定(末尾)---------------------//

    //---------------------横に壁があるかの判定(先頭)---------------------//
    [Header ("壁判定の対象(Rayと衝突するマスク)")]
    [SerializeField] private LayerMask mapLayerMask = 0; //Rayと衝突するマスク
    [Header ("壁判定(右方向へのRayの長さ)")]
    [SerializeField] private float wallRayDistance = 1.0f;
    private void WallHitUpadata () {
        Ray rightRay = new Ray (this.transform.position, this.transform.right); //右方向へのRay
        RaycastHit2D rightHit = Physics2D.Raycast (
            origin: (Vector2) rightRay.origin,
            direction: (Vector2) rightRay.direction,
            distance : wallRayDistance,
            layerMask : mapLayerMask);
        if (rightHit.collider) {
            moveRightEnable = false;
        }
    }
    //---------------------横に壁があるかの判定(末尾)---------------------//

    //---------------------横方向における敵との当たり判定(先頭)---------------------//
    [Header ("敵として衝突判定を持つレイヤー(Rayと衝突するマスク)")]
    [SerializeField] private LayerMask enemyLayerMask = 0;
    [Header ("横方向の当たり判定の最大距離(右方向へのRayの長さ)")]
    [SerializeField] private float enemyRayDistance = 1.0f;
    [Header ("横方向の当たり判定の大きさ(右方向へのRayの太さ)")]
    [SerializeField] private Vector2 enemyRaySize = new Vector2 (0.5f, 0.5f);
    private void EnemyHitUpdata () {
        Ray rightRay = new Ray (this.transform.position, Vector2.right); //右方向へのRay
        RaycastHit2D rightHit = Physics2D.BoxCast (
            origin: (Vector2) rightRay.origin,
            size : enemyRaySize,
            angle : 0.0f,
            direction: (Vector2) rightRay.direction,
            distance : enemyRayDistance,
            layerMask : enemyLayerMask);
        if (rightHit.collider) {
            isDamage = true;
            lastEnemy = rightHit.collider;
        }
    }
    //---------------------横方向における敵との当たり判定(末尾)---------------------//

    //---------------------地面方向における敵との当たり判定(先頭)---------------------//
    [Header ("下方向の当たり判定の最大距離(下方向へのRayの長さ)")]
    [SerializeField] private float treadOnEnemyRayDistance = 1.0f;
    [Header ("下方向の当たり判定の大きさ(下方向へのRayの太さ)")]
    [SerializeField] private Vector2 treadOnEnemyRaySize = new Vector2 (0.5f, 0.5f);
    private Collider2D lastEnemy; // 最後に踏んだ敵
    private void TreadOnEnemyUpdata () {
        Ray downRay = new Ray (this.transform.position, Vector2.down);
        RaycastHit2D downHit = Physics2D.BoxCast (
            origin: (Vector2) downRay.origin,
            size : treadOnEnemyRaySize,
            angle : 0.0f,
            direction: (Vector2) downRay.direction,
            distance : treadOnEnemyRayDistance,
            layerMask : enemyLayerMask);
        if (downHit.collider) {
            if (lastEnemy != downHit.collider) {
                lastEnemy = downHit.collider;
                body.velocity = new Vector2 (body.velocity.x, 0); // 速度を0にする
                body.AddForce (Vector3.up * lastEnemy.GetComponent<MobManager> ()
                    .GetPlayerBoundForce ());
            }
        }
    }
    //---------------------横方向における敵との当たり判定(末尾)---------------------//

    //---------------------接地状態の判定(先頭)---------------------//
    [Header ("ジャンプ中かどうか判定するための閾値(小さいほど判定が甘くなる)")]
    [SerializeField] private float jumpThreshold = 100f; // ジャンプ中か判定するための閾値
    private bool isGround = false; //接地判定フラグ
    private void ChangeGroundUpdata () {
        // 接地しているかどうかの判定
        if (Mathf.Abs (body.velocity.y) > jumpThreshold) { //上下の方向における速度が閾値を超えている場合
            isGround = false;
        }
    }
    //---------------------接地状態の判定(末尾)---------------------//

    //---------------------画面外に出た場合の死亡判定(先頭)---------------------//
    private void OnBecameInvisible () { // 画面外に出た場合死亡
        if (this.gameObject != null) {
            this.DiedProcess ();
        }
    }
    //---------------------画面外に出た場合の死亡判定(末尾)---------------------//

    //---------------------死亡処理(先頭)---------------------//
    private void DiedProcess () { // 画面外に出た場合死亡
        if (cameraObject != null) { //カメラがあるならばカメラを停止
            cameraObject.GetComponent<CameraManager> ().CameraStop ();
        }
        Destroy (this.gameObject);
    }
    //---------------------死亡処理(末尾)---------------------//

    //---------------------ジャンプ入力の受付(先頭)---------------------//
    [Header ("ジャンプする高さ")]
    [SerializeField] private float jumpForce = 2000f; // ジャンプする高さ
    [Header ("ジャンプする高さ(初動)")]
    [SerializeField] private float firstJumpForce = 0.5f; // ジャンプする高さ(初動)
    private void JumpUpdata () {
        if (isGround) {
            if (Input.GetKeyDown (KeyCode.Space)) {
                this.transform.Translate (0, firstJumpForce, 0);
                body.AddForce (Vector3.up * jumpForce);
                isGround = false;
            }
        }
    }
    //---------------------ジャンプ入力の受付(末尾)---------------------//

    //---------------------着地判定(先頭)---------------------//
    private void OnCollisionStay2D (Collision2D col) {
        if (col.gameObject.tag == "GroundObject") {
            if (!isGround)
                isGround = true;
            this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + 0.0001f, transform.position.z);
        }
    }
    //---------------------着地判定(末尾)---------------------//

    //---------------------重力処理(先頭)---------------------//
    [Header ("重力の強さ")]
    [SerializeField] private float gravityForce = 100f; //重力の強さ
    private void GravityUpdata () {
        body.AddForce (Vector3.down * gravityForce);
    }
    //---------------------重力処理(末尾)---------------------//

    //---------------------横移動(先頭)---------------------//
    [Header ("横移動の速度")]
    [SerializeField] private float moveSpeed = 0.2f; //横移動の速度
    [Header ("デバックモード(チェックをつけるとキー入力で移動可)")]
    [SerializeField] private bool debugMove = false; //デバックモードフラグ
    private bool moveRightEnable = true; //横に移動できる状態ならばtrue,そうでないならばfalse
    private void MoveUpdata () {
        if (moveRightEnable == true && debugMove == false) {
            this.transform.Translate (new Vector2 (moveSpeed, 0));
        }
        if (debugMove == true) {
            if (Input.GetKey (KeyCode.RightArrow)) {
                this.transform.Translate (new Vector2 (moveSpeed, 0));
            }
            if (Input.GetKey (KeyCode.LeftArrow)) {
                this.transform.Translate (new Vector2 (-moveSpeed, 0));
            }
        }
    }
    //---------------------横移動(末尾)---------------------//

    //---------------------ダメージを受けた際の処理(先頭)---------------------//
    [Header ("ダメージを受けた際の点滅周期(倍率)")]
    [SerializeField] private float blinkingCycle = 10f;
    private bool isDamage = false; // ダメージを受けた場合true
    [Header ("ダメージを受けた際の無敵時間")]
    [SerializeField] private float maxInvicibleTime = 2.0f;
    private float invincibleTime = 0.0f; // ダメージを受けてからの経過時間(累積無敵時間)
    private void DamageUpdata () {
        if (isDamage == true && (invincibleTime < maxInvicibleTime)) {
            invincibleTime += Time.deltaTime;
            float level = Mathf.Abs (Mathf.Sin (Time.time * blinkingCycle));
            this.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, level);
            this.gameObject.layer = invinciblePlayerLayerNumber;
        } else if (isDamage == true) {
            isDamage = false;
            invincibleTime = 0.0f;
            this.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
            this.gameObject.layer = normalPlayerLayerNumber;
        }
    }
    //---------------------ダメージを受けた際の処理(末尾)---------------------//

}