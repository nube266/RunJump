// 概要：ボスの挙動
// 機能：死亡判定、移動、重力
// 補足説明：体力の変動は外部からChangeHpを呼び出して行う

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Manager : MonoBehaviour {

    [SerializeField] private Vector3 popPosition = new Vector3 (296.0f, -5.5f, 0.0f);
    private void OnBecameInvisible () {
        this.transform.position = popPosition;
    }
}