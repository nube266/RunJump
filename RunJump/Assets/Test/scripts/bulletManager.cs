using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletManager : MonoBehaviour {

    [Header("弾が消えるまでの時間")]
    [SerializeField] private float timer = 2.0f;  //弾が消えるまでの時間
    [Header("弾の移動速度")]
    [SerializeField] private float moveSpeed = 0.5f;  //弾の移動速度

    void Start () {
        Destroy (this.gameObject, timer);
    }

    void Update () {
        this.transform.Translate(new Vector2 (moveSpeed, 0));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        string layerName = LayerMask.LayerToName(other.gameObject.layer);
        if( layerName == "Map") {
            Destroy(this.gameObject);
        }
    }
}
