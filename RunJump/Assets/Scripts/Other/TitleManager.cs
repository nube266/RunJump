using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {
    private GameObject[] stageText = new GameObject[3];
    private int selectStageNum;

    private void Start () {
        selectStageNum = 1;
        for (int i = 0; i < stageText.Length; i++) {
            stageText[i] = GameObject.Find ("Stage" + (i + 1).ToString () + "Text");
        }
    }
    private void Update () {
        // input
        if (Input.GetKeyDown (KeyCode.S)) {
            SceneManager.LoadScene ("stage" + selectStageNum.ToString ());
        }
        if (Input.GetKeyDown (KeyCode.DownArrow) && selectStageNum != stageText.Length) {
            selectStageNum++;
        }
        if (Input.GetKeyDown (KeyCode.UpArrow) && selectStageNum != 1) {
            selectStageNum--;
        }
        // color update
        this.UpdateColor ();
    }

    private void UpdateColor () {
        for (int i = 0; i < stageText.Length; i++) {
            if (selectStageNum == i + 1) {
                stageText[i].gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
            } else {
                stageText[i].gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0.5f);;
            }
        }
    }
}