// 概要：ボスの挙動
// 機能：画面内に入ったら数秒の硬直の後、移動とダメージ受付開始
//      画面外に出たら初期位置に戻る

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Manager : MonoBehaviour {

    [Header ("出現から移動開始までの時間")]
    [SerializeField] private float moveStartTime = 2.0f;
    private void OnBecameVisible () {
        StartCoroutine (WaitMoveTime ());
    }

    IEnumerator WaitMoveTime () {
        yield return new WaitForSeconds (moveStartTime);
        this.gameObject.GetComponent<EnemyManager> ().MoveStart ();
        this.gameObject.GetComponent<EnemyManager> ().SetBulletDamageEnable ();
    }

    [SerializeField] private Vector3 popPosition = new Vector3 (296.0f, -5.5f, 0.0f);
    private void OnBecameInvisible () {
        this.transform.position = popPosition;
    }

}