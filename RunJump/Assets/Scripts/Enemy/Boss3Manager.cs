using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Manager : MonoBehaviour {

    [Header ("ボスの初期位置のx方向へのオフセット")]
    [SerializeField] private float startOffsetX = 4.0f;
    private float initial_pos_x, initial_pos_y, initial_pos_z;
    [Header ("射出する弾のプレハブ")]
    [SerializeField] private GameObject bulletPrefab1 = null;
    [SerializeField] private GameObject bulletPrefab2 = null;
    [SerializeField] private GameObject bulletPrefab3 = null;
    [SerializeField] private GameObject mobPrefab1 = null;
    [SerializeField] private GameObject mobPrefab2 = null;
    [SerializeField] private GameObject mobPrefab3 = null;
    [SerializeField] private GameObject mobPrefab4 = null;
    private void Start () {
        GameObject goalLine = GameObject.Find ("GoalLine");
        float goalLineX = goalLine.transform.position.x;
        this.gameObject.transform.position = new Vector3 (
            goalLineX + startOffsetX,
            this.transform.position.y,
            this.transform.position.z
        );
        initial_pos_x = this.transform.position.x;
        initial_pos_y = this.transform.position.y;
        initial_pos_z = this.transform.position.z;
    }

    private bool enemyActionEnable = false;
    private void Update () {
        this.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
        if (enemyActionEnable && isTackle != true) StartCoroutine ("EnemyAction");
    }

    [Header ("行動間隔")]
    [SerializeField] private float waitTime = 1.0f;

    private int actionCount = 5;
    private string actionPattern = "A";

    private System.Random random = new System.Random ();

    IEnumerator EnemyAction () {
        enemyActionEnable = false;
        yield return new WaitForSeconds (waitTime);
        if (actionCount == 5) {
            switch (random.Next (3)) {
                case 0:
                    actionPattern = "A";
                    break;
                case 1:
                    actionPattern = "B";
                    break;
                case 2:
                    actionPattern = "C";
                    break;
            }
            actionCount = 0;
        }
        switch (actionPattern) {
            case "A":
                this.ActionA ();
                break;
            case "B":
                this.ActionB ();
                break;
            case "C":
                this.ActionC ();
                break;
        }
        actionCount++;
        enemyActionEnable = true;
    }

    private void ActionA () {
        switch (actionCount) {
            case 0:
                this.EnemyShot1 ();
                break;
            case 1:
                this.EnemyShot2 ();
                break;
            case 2:
                this.EnemyPop1 ();
                break;
            case 3:
                this.EnemyPop2 ();
                break;
            case 4:
                this.Tackle ();
                break;
        }
    }
    private void ActionB () {
        switch (actionCount) {
            case 0:
                this.EnemyPop1 ();
                break;
            case 1:
                this.EnemyPop1 ();
                break;
            case 2:
                this.Tackle ();
                break;
            case 3:
                this.EnemyShot1 ();
                break;
            case 4:
                this.EnemyShot1 ();
                break;
        }
    }

    private void ActionC () {
        switch (actionCount) {
            case 0:
                this.EnemyPopAndShot1 ();
                break;
            case 1:
                this.EnemyPopAndShot1 ();
                break;
            case 2:
                this.EnemyPop2 ();
                break;
            case 3:
                this.Tackle ();
                break;
            case 4:
                this.EnemyPop1 ();
                break;
        }
    }

    [Header ("出現から移動開始までの時間")]
    [SerializeField] private float moveStartTime = 2.0f;
    private bool firstVisiable = false;
    private void OnBecameVisible () {
        if (firstVisiable != true) {
            StartCoroutine (WaitMoveTime ());
            firstVisiable = true;
        }
    }
    IEnumerator WaitMoveTime () {
        yield return new WaitForSeconds (moveStartTime);
        this.enemyActionEnable = true;
    }

    [Header ("弾の発生場所のオフセット(X方向)")]
    [SerializeField] private float offsetX = 0.0f; // 弾の発生場所のオフセット(X方向)
    [Header ("弾の発生場所のオフセット(Y方向)")]
    [SerializeField] private float offsetY = -4.0f; // 弾の発生場所のオフセット(Y方向)
    private void EnemyShot1 () {
        Instantiate (
            bulletPrefab1, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX - 2.0f, initial_pos_y + offsetY, 0.0f), // 位置
            new Quaternion (0.0f, 90.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            bulletPrefab2, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX - 2.0f, initial_pos_y + offsetY - 2.0f, 0.0f), // 位置
            new Quaternion (0.0f, 90.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            bulletPrefab1, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX + 6.0f, initial_pos_y + offsetY, 0.0f), // 位置
            new Quaternion (0.0f, 90.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            bulletPrefab2, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX + 6.0f, initial_pos_y + offsetY - 2.0f, 0.0f), // 位置
            new Quaternion (0.0f, 90.0f, 0.0f, 1.0f)); // 角度
    }

    private void EnemyShot2 () {
        Instantiate (
            bulletPrefab1, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX, initial_pos_y + offsetY + 0.25f, 0.0f), // 位置
            new Quaternion (0.0f, 90.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            bulletPrefab3, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX, initial_pos_y + offsetY + 2.25f, 0.0f), // 位置
            new Quaternion (0.0f, 90.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            bulletPrefab1, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX + 1.0f, initial_pos_y + offsetY + 0.25f, 0.0f), // 位置
            new Quaternion (0.0f, 90.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            bulletPrefab3, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX + 1.0f, initial_pos_y + offsetY + 2.25f, 0.0f), // 位置
            new Quaternion (0.0f, 90.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            bulletPrefab1, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX + 2.0f, initial_pos_y + offsetY + 0.25f, 0.0f), // 位置
            new Quaternion (0.0f, 90.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            bulletPrefab3, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX + 2.0f, initial_pos_y + offsetY + 2.25f, 0.0f), // 位置
            new Quaternion (0.0f, 90.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            bulletPrefab1, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX + 3.0f, initial_pos_y + offsetY + 0.25f, 0.0f), // 位置
            new Quaternion (0.0f, 90.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            bulletPrefab3, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX + 3.0f, initial_pos_y + offsetY + 2.25f, 0.0f), // 位置
            new Quaternion (0.0f, 90.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            bulletPrefab1, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX + 4.0f, initial_pos_y + offsetY + 0.25f, 0.0f), // 位置
            new Quaternion (0.0f, 90.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            bulletPrefab3, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX + 4.0f, initial_pos_y + offsetY + 2.25f, 0.0f), // 位置
            new Quaternion (0.0f, 90.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            bulletPrefab1, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX + 5.0f, initial_pos_y + offsetY + 0.25f, 0.0f), // 位置
            new Quaternion (0.0f, 90.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            bulletPrefab3, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX + 5.0f, initial_pos_y + offsetY + 2.25f, 0.0f), // 位置
            new Quaternion (0.0f, 90.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            bulletPrefab1, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX + 6.0f, initial_pos_y + offsetY + 0.25f, 0.0f), // 位置
            new Quaternion (0.0f, 90.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            bulletPrefab3, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX + 6.0f, initial_pos_y + offsetY + 2.25f, 0.0f), // 位置
            new Quaternion (0.0f, 90.0f, 0.0f, 1.0f)); // 角度
    }

    private void EnemyPop1 () {
        Instantiate (
            mobPrefab1, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX, initial_pos_y + offsetY, 0.0f), // 位置
            new Quaternion (0.0f, 0.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            mobPrefab1, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX + 3.0f, initial_pos_y + offsetY, 0.0f), // 位置
            new Quaternion (0.0f, 0.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            mobPrefab4, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX, initial_pos_y + offsetY + 1.0f, 0.0f), // 位置
            new Quaternion (0.0f, 0.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            mobPrefab4, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX + 3.0f, initial_pos_y + offsetY + 1.0f, 0.0f), // 位置
            new Quaternion (0.0f, 0.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            mobPrefab4, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX, initial_pos_y + offsetY + 4.0f, 0.0f), // 位置
            new Quaternion (0.0f, 0.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            mobPrefab4, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX + 3.0f, initial_pos_y + offsetY + 4.0f, 0.0f), // 位置
            new Quaternion (0.0f, 0.0f, 0.0f, 1.0f)); // 角度
    }

    private void EnemyPop2 () {
        Instantiate (
            mobPrefab4, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX, initial_pos_y + offsetY + 1.0f, 0.0f), // 位置
            new Quaternion (0.0f, 0.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            mobPrefab4, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX + 3.0f, initial_pos_y + offsetY + 1.0f, 0.0f), // 位置
            new Quaternion (0.0f, 0.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            mobPrefab4, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX - 3.0f, initial_pos_y + offsetY + 1.0f, 0.0f), // 位置
            new Quaternion (0.0f, 0.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            mobPrefab4, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX, initial_pos_y + offsetY - 2.0f, 0.0f), // 位置
            new Quaternion (0.0f, 0.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            mobPrefab3, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX + 3.0f, initial_pos_y + offsetY - 2.0f, 0.0f), // 位置
            new Quaternion (0.0f, 0.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            mobPrefab4, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX - 3.0f, initial_pos_y + offsetY - 2.0f, 0.0f), // 位置
            new Quaternion (0.0f, 0.0f, 0.0f, 1.0f)); // 角度
    }

    private void EnemyPopAndShot1 () {
        Instantiate (
            bulletPrefab1, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX - 2.0f, initial_pos_y + offsetY, 0.0f), // 位置
            new Quaternion (0.0f, 90.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            bulletPrefab2, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX - 2.0f, initial_pos_y + offsetY - 2.0f, 0.0f), // 位置
            new Quaternion (0.0f, 90.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            bulletPrefab1, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX, initial_pos_y + offsetY, 0.0f), // 位置
            new Quaternion (0.0f, 90.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            bulletPrefab2, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX, initial_pos_y + offsetY - 2.0f, 0.0f), // 位置
            new Quaternion (0.0f, 90.0f, 0.0f, 1.0f)); // 角度
        Instantiate (
            mobPrefab4, // 生成するPrefab
            new Vector3 (initial_pos_x + offsetX - 2.0f, initial_pos_y + offsetY + 2.0f, 0.0f), // 位置
            new Quaternion (0.0f, 0.0f, 0.0f, 1.0f)); // 角度
    }

    private bool isTackle = false;
    private void Tackle () {
        this.transform.position = new Vector3 (
            initial_pos_x,
            initial_pos_y - 10.0f,
            initial_pos_z
        );
        StartCoroutine ("TackleStart");
        isTackle = true;
    }
    IEnumerator TackleStart () {
        yield return new WaitForSeconds (1.0f);
        this.gameObject.GetComponent<EnemyManager> ().MoveStart ();
    }

    private void OnBecameInvisible () {
        this.gameObject.GetComponent<EnemyManager> ().MoveStop ();
        this.transform.position = new Vector3 (
            initial_pos_x,
            initial_pos_y,
            initial_pos_z
        );
        isTackle = false;
    }
}