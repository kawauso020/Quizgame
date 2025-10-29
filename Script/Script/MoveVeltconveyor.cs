using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVeltconber : MonoBehaviour
{
    [SerializeField] float speed = 50f;
    [SerializeField] float resetPosition = -10f; // 戻る位置
    [SerializeField] float threshold = 12.0f;     // リセットする閾値
    [SerializeField] float fixedY = 2.06f;        // 固定Y座標

    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);

        if (transform.position.x > threshold)
        {
            // リセット位置に完全に戻す（オーバーシュート分を考慮しない）
            transform.position = new Vector3(resetPosition, fixedY, 0);
        }
    }
}
