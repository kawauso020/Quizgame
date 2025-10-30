using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���̃N���X�̐����������āI�I�I
/// </summary>
public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    // Start is called before the first frame update
    void OnMouseDown()
    {
        // �N���b�N�����Ƃ��̃I�u�W�F�N�g�ƃ}�E�X�̈ʒu�̍����L�^
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            // �}�E�X�̈ʒu�ɃI�u�W�F�N�g���ړ�
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
    }
}
