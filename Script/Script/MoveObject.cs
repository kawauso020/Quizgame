using UnityEngine;
using UnityEngine.InputSystem;

public class MoveObject : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed = 3f;
    public float destroyX = 10f;

    [Header("エフェクトや見た目")]
    public float scaleSpeed = 2f;
    public float scaleAmount = 0.2f;
    public Color highlightColor = new Color(1f, 0.4f, 0.4f); // 明るめの赤

    [Header("入力アクション")]
    public InputActionAsset inputActions; // PlayerInput で作った InputActionAsset をアタッチ

    private Vector3 originalScale;
    private Vector3 scaleTarget;
    private Color originalColor;

    private Renderer objectRenderer;

    private InputAction pointAction;
    private InputAction clickAction;

    private bool isDragging = false;
    private bool isMoving = true;      // 移動制御用
    private float deleteTimer = -1f;   // 削除タイマー

    private void Awake()
    {
        if (inputActions == null)
        {
            Debug.LogError("InputActions がアタッチされていません", this);
            return;
        }

        var actionMap = inputActions.FindActionMap("Player", true);
        if (actionMap == null)
        {
            Debug.LogError("アクションマップ 'Player' が見つかりません", this);
            return;
        }

        pointAction = actionMap.FindAction("Point", true);
        clickAction = actionMap.FindAction("Click", true);

        if (clickAction == null || pointAction == null)
        {
            Debug.LogError("Point または Click アクションが見つかりません", this);
            return;
        }

        clickAction.started += OnClickStarted;
        clickAction.canceled += OnClickCanceled;
    }

    private void OnEnable()
    {
        pointAction?.Enable();
        clickAction?.Enable();
    }

    private void OnDisable()
    {
        pointAction?.Disable();
        clickAction?.Disable();
    }

    private void Start()
    {
        originalScale = transform.localScale;
        scaleTarget = originalScale + Vector3.one * scaleAmount;

        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
    }

    private void Update()
    {
        // 移動処理
        if (isMoving && !isDragging)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        // ドラッグ中ならスケールアニメーション
        if (isDragging)
        {
            float scaleLerp = Mathf.PingPong(Time.time * scaleSpeed, 1f);
            transform.localScale = Vector3.Lerp(originalScale, scaleTarget, scaleLerp);
        }
        else
        {
            // 元のサイズに戻す
            transform.localScale = originalScale;
        }

        // 画面外で削除
        if (transform.position.x > destroyX)
        {
            Destroy(gameObject);
        }

        // 削除タイマーが有効ならカウント
        if (deleteTimer > 0)
        {
            deleteTimer -= Time.deltaTime;
            if (deleteTimer <= 0)
            {
                Destroy(gameObject);
            }
        }

        // ドラッグ中ならマウス座標に追従
        if (isDragging && pointAction != null)
        {
            Vector2 screenPosition = pointAction.ReadValue<Vector2>();
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(
                new Vector3(screenPosition.x, screenPosition.y, Camera.main.WorldToScreenPoint(transform.position).z)
            );
            transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
        }
    }

    private void OnClickStarted(InputAction.CallbackContext context)
    {
        // レイを飛ばしてこのオブジェクトをクリックしていたらドラッグ開始
        Vector2 screenPos = pointAction.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                isDragging = true;
                if (objectRenderer != null)
                {
                    objectRenderer.material.color = highlightColor;
                }
            }
        }
    }

    private void OnClickCanceled(InputAction.CallbackContext context)
    {
        isDragging = false;
        if (objectRenderer != null)
        {
            objectRenderer.material.color = originalColor;
        }
    }

    // ===== Draggable から呼び出されるメソッド =====
    public void StopMove()
    {
        isMoving = false;
    }

    public void StartDeleteTimer(float time)
    {
        deleteTimer = time;
    }
}
