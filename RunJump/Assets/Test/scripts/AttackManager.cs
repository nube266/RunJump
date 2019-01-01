using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour {
    [Header("射出する弾のプレハブ")]
    [SerializeField] private GameObject bulletPrefab;  //射出する弾のプレハブ
    [Header("弾の発生場所のオフセット(X方向)")]
    [SerializeField] private float offsetX = 1.0f;  // 弾の発生場所のオフセット(X方向)
    [Header("弾の発生場所のオフセット(Y方向)")]
    [SerializeField] private float offsetY = 0.0f;  // 弾の発生場所のオフセット(Y方向)
    private void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            Vector3 pos = transform.position;  // 弾を射出するオブジェクトの位置
            Instantiate(
                bulletPrefab,                                          // 生成するPrefab
                new Vector3(pos.x + offsetX, pos.y + offsetY, pos.z),  // 位置
                Quaternion.identity);                                  // 角度
        }
    }

}
