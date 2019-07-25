using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeAnim : MonoBehaviour
{
    void OnAnimationFinish() {
        Destroy(gameObject);
    }
}
