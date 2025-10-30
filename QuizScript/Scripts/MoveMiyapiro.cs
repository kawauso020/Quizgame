using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 上下移動を繰り返すスクリプト
/// </summary>
public class MoveMiyapiro : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.001f;
    private bool movingUp = true;

    void Start()
    {
        // 0.5秒ごとに移動方向を切り替える
        InvokeRepeating(nameof(SwitchDirection), 0.5f, 1f); //マジックナンバーです。変数化してください。
    }

    void FixedUpdate()
    {
        // movingUpがtrueなら上方向、falseなら下方向に移動
        float direction = movingUp ? 1f : -1f;
        transform.Translate(0, moveSpeed * direction, 0);
    }

    void SwitchDirection()
    {
        movingUp = !movingUp;
    }
}