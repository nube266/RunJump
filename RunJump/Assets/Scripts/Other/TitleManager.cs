using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {
    private GameObject[] stageText = new GameObject[3];
    private int selectStageNum;
    [Header ("カーソル移動音")]
    [SerializeField] private AudioClip moveAudio;
    [Header ("決定音")]
    [SerializeField] private AudioClip decisionAudio;
    AudioSource audioSource;

    private void Start () {
        try {
            GameObject diedManager = GameObject.Find ("DiedManager");
            string beforeStageName = diedManager.GetComponent<DiedManager> ().getStageName ();
            switch (beforeStageName) {
                case "stage1":
                    selectStageNum = 1;
                    break;
                case "stage2":
                    selectStageNum = 2;
                    break;
                case "stage3":
                    selectStageNum = 3;
                    break;
            }
            Destroy (diedManager);
        } catch {
            selectStageNum = 1;
        }
        audioSource = GetComponent<AudioSource> ();
        for (int i = 0; i < stageText.Length; i++) {
            stageText[i] = GameObject.Find ("Stage" + (i + 1).ToString () + "Text");
        }
    }
    private void Update () {
        // input
        if (Input.GetKeyDown (KeyCode.S)) {
            SceneManager.LoadScene ("stage" + selectStageNum.ToString ());
            audioSource.PlayOneShot (decisionAudio);
        }
        if (Input.GetKeyDown (KeyCode.DownArrow) && selectStageNum != stageText.Length) {
            selectStageNum++;
            audioSource.PlayOneShot (moveAudio);
        }
        if (Input.GetKeyDown (KeyCode.UpArrow) && selectStageNum != 1) {
            selectStageNum--;
            audioSource.PlayOneShot (moveAudio);
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