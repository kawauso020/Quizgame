using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���̃N���X�̐����������āI�I�I
/// </summary>
public class PauseButton : MonoBehaviour
{
    [SerializeField] GameObject pausePanel; // �|�[�Y��ʂ̎Q��

    /// <summary>
    /// �|�[�Y��ʂ̌Ăяo��
    /// </summary>
    public void OpenPause()
    {
        pausePanel.SetActive(true);
    }

    /// <summary>
    /// �|�[�Y��ʂ����
    /// </summary>
    public void ClosePause()
    {
        pausePanel.SetActive(false);
    }
}
