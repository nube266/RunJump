using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {
    void Update () {
        if (Input.GetKeyDown (KeyCode.S)) {
            SceneManager.LoadScene ("stage1"); //選択画面に移動
        }
    }
}