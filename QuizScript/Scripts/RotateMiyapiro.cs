using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 少しの回転を繰り返すスクリプト
/// </summary>
public class RotateMiyapiro : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 20f; // 回転速度（度/秒）
    private bool rotateClockwise = true;

    void Start()
    {
        // 0.5秒ごとに回転方向を切り替える
        InvokeRepeating(nameof(SwitchRotationDirection), 0.5f, 1f);  //マジックナンバーです。変数化してください。
    }

    void Update()
    {
        // 回転方向を決定（trueなら時計回り、falseなら反時計回り）
        float direction = rotateClockwise ? 1f : -1f;
        transform.Rotate(0, 0, rotationSpeed * direction * Time.deltaTime);
    }

    void SwitchRotationDirection()
    {
        rotateClockwise = !rotateClockwise;
    }
}