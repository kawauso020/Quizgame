using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// このクラスの説明を書いて！！！
/// </summary>
public class MoveBeltConveyor : MonoBehaviour
{

    [SerializeField]
    float speed = 50f;
    float resetPosition = -10f; // 戻る位置
    float threshold = 12.0f; // リセットする閾値

    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);

        if (transform.position.x > threshold)
        {
            // ワープするのではなく、移動量を引いた位置にする
            float overshoot = transform.position.x - threshold;
            transform.position = new Vector3(resetPosition + overshoot, 2.06f, 0); //マジックナンバーです。変数化してください。
        }
    }
}
