using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����̉�]���J��Ԃ��X�N���v�g
/// </summary>
public class RotateMiyapiro : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 20f; // ��]���x�i�x/�b�j
    private bool rotateClockwise = true;

    void Start()
    {
        // 0.5�b���Ƃɉ�]������؂�ւ���
        InvokeRepeating(nameof(SwitchRotationDirection), 0.5f, 1f);  //�}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B
    }

    void Update()
    {
        // ��]����������itrue�Ȃ玞�v���Afalse�Ȃ甽���v���j
        float direction = rotateClockwise ? 1f : -1f;
        transform.Rotate(0, 0, rotationSpeed * direction * Time.deltaTime);
    }

    void SwitchRotationDirection()
    {
        rotateClockwise = !rotateClockwise;
    }
}