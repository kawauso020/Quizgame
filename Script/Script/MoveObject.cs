using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ���̃N���X�̐����������āI�I�I
/// </summary>
public class MoveObject : MonoBehaviour
{

    //�ȉ��̂��ׂĂ�public�ϐ��ɂ��āF
    // �Ȃ�public�Ȃ̂����R�������Ă��������Bpublic�ɂ���K�v���Ȃ��Ȃ�[SerializeField]�ɂ��Ă��������B
    [Header("�ړ��ݒ�")]
    public float moveSpeed = 3f; 
    public float destroyX = 10f;

    [Header("�G�t�F�N�g�〈����")]
    public float scaleSpeed = 2f;
    public float scaleAmount = 0.2f;
    public Color highlightColor = new Color(1f, 0.4f, 0.4f); // ����߂̐�

    [Header("���̓A�N�V����")]
    public InputActionAsset inputActions; // PlayerInput �ō���� InputActionAsset ���A�^�b�`

    private Vector3 originalScale;
    private Vector3 scaleTarget;
    private Color originalColor;

    private Renderer objectRenderer;

    private InputAction pointAction;
    private InputAction clickAction;

    private bool isDragging = false;
    private bool isMoving = true;      // �ړ�����p
    private float deleteTimer = -1f;   // �폜�^�C�}�[

    private void Awake()
    {
        if (inputActions == null)
        {
            Debug.LogError("InputActions ���A�^�b�`����Ă��܂���", this);
            return;
        }

        var actionMap = inputActions.FindActionMap("Player", true);
        if (actionMap == null)
        {
            Debug.LogError("�A�N�V�����}�b�v 'Player' ��������܂���", this);
            return;
        }

        pointAction = actionMap.FindAction("Point", true);
        clickAction = actionMap.FindAction("Click", true);

        if (clickAction == null || pointAction == null)
        {
            Debug.LogError("Point �܂��� Click �A�N�V������������܂���", this);
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
        // �ړ�����
        if (isMoving && !isDragging)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        // �h���b�O���Ȃ�X�P�[���A�j���[�V����
        if (isDragging)
        {
            float scaleLerp = Mathf.PingPong(Time.time * scaleSpeed, 1f);
            transform.localScale = Vector3.Lerp(originalScale, scaleTarget, scaleLerp);
        }
        else
        {
            // ���̃T�C�Y�ɖ߂�
            transform.localScale = originalScale;
        }

        // ��ʊO�ō폜
        if (transform.position.x > destroyX)
        {
            Destroy(gameObject);
        }

        // �폜�^�C�}�[���L���Ȃ�J�E���g
        if (deleteTimer > 0)
        {
            deleteTimer -= Time.deltaTime;
            if (deleteTimer <= 0)
            {
                Destroy(gameObject);
            }
        }

        // �h���b�O���Ȃ�}�E�X���W�ɒǏ]
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
        // ���C���΂��Ă��̃I�u�W�F�N�g���N���b�N���Ă�����h���b�O�J�n
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

    // ===== Draggable ����Ăяo����郁�\�b�h =====
    public void StopMove()
    {
        isMoving = false;
    }

    public void StartDeleteTimer(float time)
    {
        deleteTimer = time;
    }
}
