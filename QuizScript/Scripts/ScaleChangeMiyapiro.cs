using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �g��k�����J��Ԃ��X�N���v�g
/// </summary>
public class ScaleChangeMiyapiro : MonoBehaviour
{
    [SerializeField] float scaleSpeed = 1f; // �g��k���X�s�[�h
    [SerializeField] float minScale = 0.5f; // �ŏ��X�P�[��
    [SerializeField] float maxScale = 1.5f; // �ő�X�P�[��
    private bool scalingUp = true;

    void Start()
    {
        // 0.5�b���ƂɊg��E�k����؂�ւ���
        InvokeRepeating(nameof(SwitchScaleDirection), 0.5f, 1f); //�}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B
    }

    void Update()
    {
        // �g��E�k���̕���������
        float direction = scalingUp ? 1f : -1f;
        float scaleChange = scaleSpeed * direction * Time.deltaTime;

        // ���݂̃X�P�[�����擾���Ċg��E�k��
        Vector3 newScale = transform.localScale + new Vector3(scaleChange, scaleChange, scaleChange);

        // �ŏ��E�ő�X�P�[���𐧌�
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