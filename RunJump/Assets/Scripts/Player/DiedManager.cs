using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiedManager : MonoBehaviour {
    private bool playerDiedFlag = false; // プレイヤーが死んでいるならばtrue
    private GameObject cameraObject = null; // メインカメラのオブジェクト
    // Start is called before the first frame update
    void Start () {
        cameraObject = GameObject.FindGameObjectWithTag ("MainCamera");
    }

    // Update is called once per frame
    private void Update () {
        if (playerDiedFlag == true) {
            if (Input.GetKeyDown (KeyCode.Space)) {
                SceneManager.LoadScene ("title"); //選択画面に移動
            }
        }
    }

    //---------------------死亡時のフラグ処理(先頭)---------------------//
    public void SetPlayerDied () {
        playerDiedFlag = true;
        this.ShowGameOverText ();
    }
    //---------------------死亡時のフラグ処理(末尾)---------------------//

    //---------------------Game Over時のテキスト表示(先頭)---------------------//
    [Header ("GAME OVER時に表示されるテキスト")]
    [SerializeField] private GameObject GameOverTextObject = null;
    [Header ("GAME OVER時に表示されるテキストの位置に関するオフセット(X方向)")]
    [SerializeField] private float GameOverTextOffsetX = 0.5f;
    [Header ("GAME OVER時に表示されるテキストの位置に関するオフセット(Y方向)")]
    [SerializeField] private float GameOverTextOffsetY = 0.5f;
    private bool isQuit = false; // アプリケーションを終了した際にtrueにする
    private void OnApplicationQuit () {
        isQuit = true;
    }
    private void ShowGameOverText () {
        if (cameraObject != null && !isQuit) {
            if (GameOverTextObject != null) {
                Instantiate (
                    GameOverTextObject, // 生成するPrefab
                    new Vector3 (
                        cameraObject.transform.position.x + GameOverTextOffsetX,
                        cameraObject.transform.position.y + GameOverTextOffsetY,
                        this.transform.position.z
                    ), // 位置
                    Quaternion.identity
                ); // 角度
            } else {
                Debug.Log ("死亡時のテキストが設定されていません");
            }
        }
    }
    //---------------------Game Over時のテキスト表示(末尾)---------------------//
}