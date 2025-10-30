using UnityEngine;

/// <summary>
/// ���̃N���X�̐����������āI�I�I
/// </summary>
[RequireComponent(typeof(MoveObject))]
[RequireComponent(typeof(SpriteRenderer))]
public class Draggable : MonoBehaviour
{
    private MoveObject moveObject;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private Vector3 offset;       // �͂񂾈ʒu�ƃI�u�W�F�N�g���S�̍�
    private bool isDragging = false;

    [SerializeField] private Color dragColor = Color.red;
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        moveObject = GetComponent<MoveObject>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        if (gameManager == null)
        {
            GameObject gm = GameObject.FindWithTag("GameController");
            if (gm != null) gameManager = gm.GetComponent<GameManager>();
        }
    }

    // ==========================
    // �}�E�X���͗p (�]���̓���)
    // ==========================
    private void OnMouseDown()
    {
        if (Time.timeScale == 0) return;

        gameManager?.PlaySound(2); //�Q�̓}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B
        spriteRenderer.color = dragColor;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        offset = transform.position - mouseWorld;
        isDragging = true;

        moveObject.StopMove();
        moveObject.StartDeleteTimer(10f); //�}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B
    }

    private void OnMouseDrag()
    {
        if (isDragging && Time.timeScale != 0)
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = 0f;
            transform.position = mouseWorld + offset;
        }
    }

    private void OnMouseUp()
    {
        if (!isDragging) return;

        gameManager?.PlaySound(3); //�}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B
        spriteRenderer.color = originalColor;
        isDragging = false;
    }

    // ==========================
    // �R���g���[���[���͗p
    // ==========================

    // �͂񂾏u��
    public void ControllerToMouseDown(Vector3 cursorWorldPos)
    {
        if (Time.timeScale == 0) return;

        gameManager?.PlaySound(2); //�}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B
        spriteRenderer.color = dragColor;

        offset = transform.position - cursorWorldPos;
        isDragging = true;

        moveObject.StopMove();
        moveObject.StartDeleteTimer(10f); //�}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B
    }

    // �h���b�O���i���t���[���Ăԁj
    public void ControllerToMouseDrag(Vector3 cursorWorldPos)
    {
        if (isDragging && Time.timeScale != 0)
        {
            transform.position = cursorWorldPos + offset;
        }
    }

    // �������u��
    public void ControllerToMouseUp()
    {
        if (!isDragging) return;

        gameManager?.PlaySound(3); //�}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B
        spriteRenderer.color = originalColor;
        isDragging = false;
    }
}
