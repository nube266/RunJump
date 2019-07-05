using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Manager : MonoBehaviour {
    private bool enemyShotEnable = false;

    private void Update () {
        if (enemyShotEnable) StartCoroutine ("EnemyShot"); // ブレス攻撃
    }

    IEnumerator EnemyShot () {
        enemyShotEnable = false;
        yield return new WaitForSeconds (1);
        Debug.Log ("Hello");
        enemyShotEnable = true;
    }

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

}