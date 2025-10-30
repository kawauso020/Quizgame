using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���̃N���X�̐����������āI�I�I
/// </summary>
public class MoveBeltConveyor : MonoBehaviour
{

    [SerializeField]
    float speed = 50f;
    float resetPosition = -10f; // �߂�ʒu
    float threshold = 12.0f; // ���Z�b�g����臒l

    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);

        if (transform.position.x > threshold)
        {
            // ���[�v����̂ł͂Ȃ��A�ړ��ʂ��������ʒu�ɂ���
            float overshoot = transform.position.x - threshold;
            transform.position = new Vector3(resetPosition + overshoot, 2.06f, 0); //�}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B
        }
    }
}
