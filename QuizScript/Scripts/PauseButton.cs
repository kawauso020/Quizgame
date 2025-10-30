using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// このクラスの説明を書いて！！！
/// </summary>
public class PauseButton : MonoBehaviour
{
    [SerializeField] GameObject pausePanel; // ポーズ画面の参照

    /// <summary>
    /// ポーズ画面の呼び出し
    /// </summary>
    public void OpenPause()
    {
        pausePanel.SetActive(true);
    }

    /// <summary>
    /// ポーズ画面を閉じる
    /// </summary>
    public void ClosePause()
    {
        pausePanel.SetActive(false);
    }
}
