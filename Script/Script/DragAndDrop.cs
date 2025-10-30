using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// このクラスの説明を書いて！！！
/// </summary>
public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    // Start is called before the first frame update
    void OnMouseDown()
    {
        // クリックしたときのオブジェクトとマウスの位置の差を記録
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            // マウスの位置にオブジェクトを移動
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
    }
}
