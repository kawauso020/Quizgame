using UnityEngine;

[RequireComponent(typeof(MoveObject))]
[RequireComponent(typeof(SpriteRenderer))]
public class Draggable : MonoBehaviour
{
    private MoveObject moveObject;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private Vector3 offset;       // 掴んだ位置とオブジェクト中心の差
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
    // マウス入力用 (従来の動作)
    // ==========================
    private void OnMouseDown()
    {
        if (Time.timeScale == 0) return;

        gameManager?.PlaySound(2);
        spriteRenderer.color = dragColor;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        offset = transform.position - mouseWorld;
        isDragging = true;

        moveObject.StopMove();
        moveObject.StartDeleteTimer(10f);
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

        gameManager?.PlaySound(3);
        spriteRenderer.color = originalColor;
        isDragging = false;
    }

    // ==========================
    // コントローラー入力用
    // ==========================

    // 掴んだ瞬間
    public void ControllerToMouseDown(Vector3 cursorWorldPos)
    {
        if (Time.timeScale == 0) return;

        gameManager?.PlaySound(2);
        spriteRenderer.color = dragColor;

        offset = transform.position - cursorWorldPos;
        isDragging = true;

        moveObject.StopMove();
        moveObject.StartDeleteTimer(10f);
    }

    // ドラッグ中（毎フレーム呼ぶ）
    public void ControllerToMouseDrag(Vector3 cursorWorldPos)
    {
        if (isDragging && Time.timeScale != 0)
        {
            transform.position = cursorWorldPos + offset;
        }
    }

    // 離した瞬間
    public void ControllerToMouseUp()
    {
        if (!isDragging) return;

        gameManager?.PlaySound(3);
        spriteRenderer.color = originalColor;
        isDragging = false;
    }
}
