using UnityEngine;
using UnityEngine.InputSystem;

public class UICursorController : MonoBehaviour
{
    [Header("Cursor UI")]
    [SerializeField] private RectTransform cursorImage;
    [SerializeField] private Canvas canvas;

    [Header("Settings")]
    [SerializeField] private float cursorSpeed = 1000f;
    [SerializeField] private float inputThreshold = 0.1f;

    private Vector2 cursorPosition;
    private Draggable currentDraggable;
    private bool usingGamepad = false;
    private UnityEngine.UI.Image cursorImageRenderer;

    private void Awake()
    {
        cursorImageRenderer = cursorImage.GetComponent<UnityEngine.UI.Image>();

        usingGamepad = false;
        Cursor.visible = true;
        cursorImageRenderer.enabled = false;

        if (Mouse.current != null)
            cursorPosition = Mouse.current.position.ReadValue();

        UpdateCursorUI();
    }

    private void Update()
    {
        // ポーズ中は何もしない
        if (GameManager.Instance != null && GameManager.Instance.IsGamePaused)
            return;

        DetectInputSource();

        if (usingGamepad)
        {
            Cursor.visible = false;
            cursorImageRenderer.enabled = true;

            HandleControllerMovement();
            SyncMouseCursor();

            if (Gamepad.current == null) return;

            if (Gamepad.current.aButton.wasPressedThisFrame)
                TryPickObject();

            if (Gamepad.current.aButton.isPressed && currentDraggable != null)
                currentDraggable.ControllerToMouseDrag(ScreenToWorld(cursorPosition));

            if (Gamepad.current.aButton.wasReleasedThisFrame && currentDraggable != null)
            {
                currentDraggable.ControllerToMouseUp();
                currentDraggable = null;
            }
        }
        else
        {
            Cursor.visible = true;
            cursorImageRenderer.enabled = false;
        }
    }

    private void DetectInputSource()
    {
        if (Gamepad.current != null)
        {
            if (Gamepad.current.leftStick.ReadValue().sqrMagnitude > inputThreshold * inputThreshold ||
                Gamepad.current.aButton.wasPressedThisFrame ||
                Gamepad.current.startButton.wasPressedThisFrame)
            {
                usingGamepad = true;
                return;
            }
        }

        if (Mouse.current != null)
        {
            if (Mouse.current.delta.ReadValue().sqrMagnitude > 0.01f ||
                Mouse.current.leftButton.wasPressedThisFrame ||
                Mouse.current.rightButton.wasPressedThisFrame)
            {
                usingGamepad = false;
                return;
            }
        }
    }

    private void TryPickObject()
    {
        Vector3 worldPos = ScreenToWorld(cursorPosition);

        RaycastHit2D hit = Physics2D.Raycast((Vector2)worldPos, Vector2.zero);

        if (hit.collider != null)
        {
            Draggable d = hit.collider.GetComponent<Draggable>();
            if (d != null)
            {
                d.ControllerToMouseDown(worldPos);
                currentDraggable = d;
                Debug.Log(hit.collider.gameObject.name + " をドラッグ開始");
            }
        }
    }

    private Vector3 ScreenToWorld(Vector2 screenPos)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        worldPos.z = 0f;
        return worldPos;
    }

    private void HandleControllerMovement()
    {
        Vector2 moveInput = Vector2.zero;

        if (Gamepad.current != null)
            moveInput = Gamepad.current.leftStick.ReadValue();

        cursorPosition += moveInput * cursorSpeed * Time.deltaTime;

        cursorPosition.x = Mathf.Clamp(cursorPosition.x, 0, Screen.width);
        cursorPosition.y = Mathf.Clamp(cursorPosition.y, 0, Screen.height);

        UpdateCursorUI();
    }

    private void UpdateCursorUI()
    {
        Vector2 canvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            cursorPosition,
            canvas.worldCamera,
            out canvasPos
        );

        cursorImage.anchoredPosition = canvasPos;
        cursorImage.SetAsLastSibling();
    }

    private void SyncMouseCursor()
    {
        WarpMouse(cursorPosition);
    }

    private void WarpMouse(Vector2 screenPosition)
    {
        if (Mouse.current != null)
            Mouse.current.WarpCursorPosition(screenPosition);
    }
}
