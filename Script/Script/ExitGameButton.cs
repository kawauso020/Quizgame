using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���̃N���X�̐����������āI�I�I
/// </summary>
public class ExitGameButton : MonoBehaviour
{
   
    // ���̃��\�b�h���{�^���� OnClick �ɓo�^���Ă�������
    public void ExitGame()
    {
        // �G�f�B�^��ł͎~�߂�A�r���h�łł͏I������
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

