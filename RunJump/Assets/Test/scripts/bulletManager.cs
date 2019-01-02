using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour {

    [Header("弾の移動速度")]
    [SerializeField] private float moveSpeed = 0.5f;  //弾の移動速度

    private void Update() {
        this.transform.Translate(new Vector2 (moveSpeed, 0));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        string layerName = LayerMask.LayerToName(other.gameObject.layer);
        if( layerName == "Map") {  // マップと衝突した場合
            Destroy(this.gameObject);
        }
        if( layerName == "Enemy") {  // 敵と衝突した場合
            Destroy(this.gameObject);  // 弾を消滅
            MobManager manager = other.GetComponent<MobManager> ();
            if(manager != null) {
                manager.ChangeHp(1);    // 敵に引数の分だけダメージを与える
            }
        }
    }

    private void OnBecameInvisible() {  // 画面外に出た場合死亡
        if(this.gameObject != null) {
            Destroy(this.gameObject);
        }
    }
}
