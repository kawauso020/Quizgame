using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���̃N���X�̐����������āI�I�I
/// </summary>
/// 
/// �����������̃N���X��MoveBeltConveyor�͂ǂ��Ⴄ�́H�ǂ����Ƃ��g���Ă���̂��H
/// �ł���΁A���O�������Ƃ킩��₷�����Ă��������B
public class MoveVeltconber : MonoBehaviour
{
    [SerializeField] float speed = 50f;
    [SerializeField] float resetPosition = -10f; // �߂�ʒu
    [SerializeField] float threshold = 12.0f;     // ���Z�b�g����臒l
    [SerializeField] float fixedY = 2.06f;        // �Œ�Y���W

    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);

        if (transform.position.x > threshold)
        {
            // ���Z�b�g�ʒu�Ɋ��S�ɖ߂��i�I�[�o�[�V���[�g�����l�����Ȃ��j
            transform.position = new Vector3(resetPosition, fixedY, 0);
        }
    }
}
