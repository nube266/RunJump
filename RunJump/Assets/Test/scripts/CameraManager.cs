using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    [Header("カメラの追従対象")]
    [SerializeField] private GameObject player;
    [Header("カメラの高さ")]
    [SerializeField] private float offsetY = -5.0f;
    [Header("カメラの奥行き")]
    [SerializeField] private float offsetZ = -5.0f;

    [Header("カメラの横方向に対するオフセット")]
    [SerializeField] private float offsetX = -5.0f;

    private void Start () {
    }
    private void Update () {
        transform.position = new Vector3(player.transform.position.x + offsetX, offsetY, offsetZ);
    }
}