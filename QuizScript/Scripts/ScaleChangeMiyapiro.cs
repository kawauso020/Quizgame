using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 拡大縮小を繰り返すスクリプト
/// </summary>
public class ScaleChangeMiyapiro : MonoBehaviour
{
    [SerializeField] float scaleSpeed = 1f; // 拡大縮小スピード
    [SerializeField] float minScale = 0.5f; // 最小スケール
    [SerializeField] float maxScale = 1.5f; // 最大スケール
    private bool scalingUp = true;

    void Start()
    {
        // 0.5秒ごとに拡大・縮小を切り替える
        InvokeRepeating(nameof(SwitchScaleDirection), 0.5f, 1f); //マジックナンバーです。変数化してください。
    }

    void Update()
    {
        // 拡大・縮小の方向を決定
        float direction = scalingUp ? 1f : -1f;
        float scaleChange = scaleSpeed * direction * Time.deltaTime;

        // 現在のスケールを取得して拡大・縮小
        Vector3 newScale = transform.localScale + new Vector3(scaleChange, scaleChange, scaleChange);

        // 最小・最大スケールを制限
        newScale.x = Mathf.Clamp(newScale.x, minScale, maxScale);
        newScale.y = Mathf.Clamp(newScale.y, minScale, maxScale);
        newScale.z = Mathf.Clamp(newScale.z, minScale, maxScale);

        transform.localScale = newScale;
    }

    void SwitchScaleDirection()
    {
        scalingUp = !scalingUp;
    }
}