using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �㉺�ړ����J��Ԃ��X�N���v�g
/// </summary>
public class MoveMiyapiro : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.001f;
    private bool movingUp = true;

    void Start()
    {
        // 0.5�b���ƂɈړ�������؂�ւ���
        InvokeRepeating(nameof(SwitchDirection), 0.5f, 1f); //�}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B
    }

    void FixedUpdate()
    {
        // movingUp��true�Ȃ������Afalse�Ȃ牺�����Ɉړ�
        float direction = movingUp ? 1f : -1f;
        transform.Translate(0, moveSpeed * direction, 0);
    }

    void SwitchDirection()
    {
        movingUp = !movingUp;
    }
}