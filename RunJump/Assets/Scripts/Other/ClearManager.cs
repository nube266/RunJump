using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearManager : MonoBehaviour {
    [Header ("次のステージ名")]
    [SerializeField] private string nextStageName = "title";
    private bool BossDiedFlag = false; // ボスが死んでいるならばtrue
    private GameObject cameraObject = null; // メインカメラのオブジェクト

    [Header ("決定音")]
    [SerializeField] private AudioClip decisionAudio;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start () {
        cameraObject = GameObject.FindGameObjectWithTag ("MainCamera");
        audioSource = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    private void Update () {
        if (BossDiedFlag == true) {
            if (Input.GetKeyDown (KeyCode.S)) {
                SceneManager.LoadScene (nextStageName);
                audioSource.PlayOneShot (decisionAudio);
            }
        }
    }

    public bool getBossDiedFlag () {
        return BossDiedFlag;
    }

    //---------------------死亡時のフラグ処理(先頭)---------------------//
    public void SetBossDied () {
        BossDiedFlag = true;
        this.ShowGameClearText ();
    }
    //---------------------死亡時のフラグ処理(末尾)---------------------//

    //---------------------Game Clear時のテキスト表示(先頭)---------------------//
    [Header ("GAME CLEAR時に表示されるテキスト")]
    [SerializeField] private GameObject GameClearTextObject = null;
    [SerializeField] private GameObject PressTextObject = null;
    [SerializeField] private GameObject STextObject = null;
    [SerializeField] private GameObject KeyTextObject = null;
    [Header ("GAME CLEAR時に表示されるテキストの位置に関するオフセット(X方向)")]
    [SerializeField] private float GameClearTextOffsetX = 0.5f;
    [Header ("GAME Clear時に表示されるテキストの位置に関するオフセット(Y方向)")]
    [SerializeField] private float GameClearTextOffsetY = 0.5f;
    private void ShowGameClearText () {
        if (cameraObject != null) {
            if (GameClearTextObject != null) {
                Instantiate (
                    GameClearTextObject, // 生成するPrefab
                    new Vector3 (
                        cameraObject.transform.position.x + GameClearTextOffsetX,
                        cameraObject.transform.position.y + GameClearTextOffsetY,
                        this.transform.position.z
                    ), // 位置
                    Quaternion.identity
                ); // 角度
                Instantiate (
                    PressTextObject, // 生成するPrefab
                    new Vector3 (
                        cameraObject.transform.position.x + GameClearTextOffsetX - 2.0f,
                        cameraObject.transform.position.y + GameClearTextOffsetY - 2.0f,
                        this.transform.position.z
                    ), // 位置
                    Quaternion.identity
                ); // 角度
                Instantiate (
                    STextObject, // 生成するPrefab
                    new Vector3 (
                        cameraObject.transform.position.x + GameClearTextOffsetX + 0.5f,
                        cameraObject.transform.position.y + GameClearTextOffsetY - 2.0f,
                        this.transform.position.z
                    ), // 位置
                    Quaternion.identity
                ); // 角度
                Instantiate (
                    KeyTextObject, // 生成するPrefab
                    new Vector3 (
                        cameraObject.transform.position.x + GameClearTextOffsetX + 2.5f,
                        cameraObject.transform.position.y + GameClearTextOffsetY - 2.0f,
                        this.transform.position.z
                    ), // 位置
                    Quaternion.identity
                ); // 角度
            } else {
                Debug.Log ("死亡時のテキストが設定されていません");
            }
        } else {
            Debug.Log ("カメラが指定されていません");
        }
    }
    //---------------------Game Clear時のテキスト表示(末尾)---------------------//
}