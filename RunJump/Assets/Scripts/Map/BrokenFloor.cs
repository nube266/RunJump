using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenFloor : MonoBehaviour {
    void Update () {
        this.HpUpdata ();
    }

    //---------------------体力による状態の変更(先頭)---------------------//
    [Header ("体力(攻撃を耐える回数)")]
    [SerializeField] private int hp = 1; // 攻撃を耐える回数
    private void HpUpdata () {
        if (hp <= 0) {
            Destroy (this.gameObject); // 体力が0以下になったら消滅
        }
    }
    //---------------------体力による状態の変更(末尾)---------------------//

    //---------------------HPの変動(先頭)---------------------//
    public void ChangeHp (int damage) {
        hp -= damage;
    }
    //---------------------HPの変動(末尾)---------------------//

}