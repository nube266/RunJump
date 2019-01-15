// 概要：カメラの移動

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    [Header("対象のオブジェクト")]
    [SerializeField] private GameObject player = null;
    [Header("カメラの高さ")]
    [SerializeField] private float offsetY = -5.0f;
    [Header("カメラの奥行き")]
    [SerializeField] private float offsetZ = -5.0f;

    [Header("カメラの横方向に対するオフセット")]
    [SerializeField] private float offsetX = -5.0f;
    [Header("カメラの横方向に移動する速度")]
    [SerializeField] private float moveSpeed = 0.2f;
    private bool CameraStopFlag = false; // カメラを動かすかどうかのフラグ trueならば止める

    private bool DebugMoveMode = false;  // デバックモードならばtrue

    private void Start () {
        this.transform.position = new Vector3(player.transform.position.x + offsetX, offsetY, offsetZ);
    }

    private void Update () {
        if(CameraStopFlag == false && DebugMoveMode == false) {
            this.transform.Translate(new Vector2 (moveSpeed, 0));
        }
        if(DebugMoveMode == true) {
            this.transform.position = new Vector3(player.transform.position.x + offsetX, offsetY, offsetZ);
        }
    }

    public void CameraStop() {  // カメラの停止
        CameraStopFlag = true;
    }

    public void SetDebugMoveMode() {  // デバッグモード
        DebugMoveMode = true;
    }

}