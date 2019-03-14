// 概要：モブの挙動
// 機能：死亡判定、移動、重力
// 補足説明：体力の変動は外部からChangeHpを呼び出して行う

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour
{

    private Rigidbody2D body;   // このスクリプトが適用されているRigidbody

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        HpUpdata();         // 死亡判定
        MoveUpdata();       // 移動処理
        GravityUpdata();    // 重力処理
    }

    //---------------------移動開始判定(先頭)---------------------//
    [Header("移動するかどうかのフラグ")]
    [SerializeField] private bool moveEnable = false;  // 移動するオブジェクトならばtrue,そうでないならばfalse
    private bool moveStartFlag = false;  // 動き出すまではfalse、動き出したあとはfalse
    private void OnBecameVisible()
    {  // カメラ内に入ったら動き出す
        if (moveEnable == true && this.gameObject != null)
        {  // 移動するオブジェクトならば
            moveStartFlag = true;
        }
    }
    //---------------------移動開始判定(末尾)---------------------//

    //---------------------死亡判定(先頭)---------------------//
    [Header("体力(攻撃を耐える回数)")]
    [SerializeField] private int hp = 1;  // 攻撃を耐える回数
    private void HpUpdata()
    {
        if (hp <= 0)
        {
            Destroy(this.gameObject);  // 体力が0以下になったら消滅
        }
    }
    //---------------------死亡判定(末尾)---------------------//

    //---------------------移動処理(先頭)---------------------//
    [Header("移動速度")]
    [SerializeField] private float moveSpeed = 0.1f;  // 移動速度
    [Header("移動方向(チェックを入れると右、入れていない場合左)")]
    [SerializeField] private bool moveDirection = false;  // 移動方向(チェックを入れると右、入れていない場合左)
    private void MoveUpdata()
    {
        if (moveStartFlag == true)
        {
            if (moveDirection == true)
            {
                this.transform.Translate(new Vector2(moveSpeed, 0));
            }
            else
            {
                this.transform.Translate(new Vector2(-moveSpeed, 0));
            }
        }

    }
    //---------------------移動処理(先頭)---------------------//

    //---------------------重力処理(先頭)---------------------//
    [Header("重力の強さ")]
    [SerializeField] private float gravityForce = 0.001f;    //重力の強さ
    private void GravityUpdata()
    {
        body.AddForce(Vector3.down * gravityForce);
    }
    //---------------------重力処理(末尾)---------------------//

    //---------------------HPの変動(先頭)---------------------//
    public void ChangeHp(int damage)
    {
        hp -= damage;
    }
    //---------------------HPの変動(末尾)---------------------//

    //------------------踏まれた際にプレイヤーが飛ぶ強さを返す(先頭)------------------//
    [Header("踏まれた際にプレイヤーが飛ぶ強さ")]
    [SerializeField] private float playerBoundForce = 2000f;
    public float GetPlayerBoundForce()
    {
        return this.playerBoundForce;
    }
    //------------------踏まれた際にプレイヤーが飛ぶ強さを返す(末尾)------------------//
}
