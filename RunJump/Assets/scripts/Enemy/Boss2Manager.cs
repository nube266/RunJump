using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Manager : MonoBehaviour {

    private void Update () {
        if (enemyShotEnable) StartCoroutine ("EnemyShot"); // ブレス攻撃
    }

    private bool enemyShotEnable = false;
    [Header ("射出する弾のプレハブ")]
    [SerializeField] private GameObject bulletPrefab = null; //射出する弾のプレハブ
    [Header ("弾の発生場所のオフセット(X方向)")]
    [SerializeField] private float offsetX = 0.0f; // 弾の発生場所のオフセット(X方向)
    [Header ("弾の発生場所のオフセット(Y方向)")]
    [SerializeField] private float offsetY = 0.0f; // 弾の発生場所のオフセット(Y方向)
    [Header ("ブレスの発生間隔")]
    [SerializeField] private float waitShotTime = 0.6f;
    IEnumerator EnemyShot () {
        enemyShotEnable = false;
        yield return new WaitForSeconds (waitShotTime);
        Vector3 pos = transform.position;
        Instantiate (
            bulletPrefab, // 生成するPrefab
            new Vector3 (pos.x + offsetX, pos.y + offsetY, 0.0f), // 位置
            new Quaternion (0.0f, 90.0f, 0.0f, 1.0f)); // 角度
        enemyShotEnable = true;
    }

    //-------------待機(先頭)-------------//
    [Header ("出現から移動開始までの時間")]
    [SerializeField] private float moveStartTime = 2.0f;
    private void OnBecameVisible () {
        StartCoroutine (WaitMoveTime ());
    }

    IEnumerator WaitMoveTime () {
        yield return new WaitForSeconds (moveStartTime);
        this.enemyShotEnable = true;
        this.gameObject.GetComponent<EnemyManager> ().SetMoveUpDownEnable ();
        this.gameObject.GetComponent<EnemyManager> ().SetBulletDamageEnable ();
    }
    //-------------待機(末尾)-------------//
}