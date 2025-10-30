using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// このクラスの説明を書いて！！！
/// </summary>
public class GrabAndPulse : MonoBehaviour
{
    [SerializeField] private InputActionReference clickAction; // Clickアクションへの参照

    private Camera mainCamera;
    private GameObject grabbedObject;
    private Vector3 offset;
    private float zDistance;

    private bool isGrabbing = false;
    private float scaleTimer = 0f;
    private bool scaleUp = true;

    [SerializeField] private float scaleSpeed = 3f;
    [SerializeField] private float scaleAmount = 0.3f;
    [SerializeField] private float scaleCycleTime = 0.4f;

    private Vector3 originalScale;

    private void OnEnable()
    {
        clickAction.action.started += OnClickStarted;
        clickAction.action.canceled += OnClickCanceled;
        clickAction.action.Enable();
    }

    private void OnDisable()
    {
        clickAction.action.started -= OnClickStarted;
        clickAction.action.canceled -= OnClickCanceled;
        clickAction.action.Disable();
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (isGrabbing && grabbedObject != null)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z = zDistance;
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
            grabbedObject.transform.position = worldPos + offset;

            AnimateScale(grabbedObject);
        }
    }

    private void OnClickStarted(InputAction.CallbackContext context)
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            grabbedObject = hit.collider.gameObject;
            zDistance = Vector3.Distance(mainCamera.transform.position, grabbedObject.transform.position);
            offset = grabbedObject.transform.position - mainCamera.ScreenToWorldPoint(
                new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, zDistance)
            );

            originalScale = grabbedObject.transform.localScale;
            scaleTimer = 0f;
            scaleUp = true;
            isGrabbing = true;
        }
    }

    private void OnClickCanceled(InputAction.CallbackContext context)
    {
        if (grabbedObject != null)
        {
            grabbedObject.transform.localScale = originalScale;
        }

        grabbedObject = null;
        isGrabbing = false;
    }

    private void AnimateScale(GameObject obj)
    {
        scaleTimer += Time.deltaTime;

        if (scaleTimer >= scaleCycleTime)
        {
            scaleTimer = 0f;
            scaleUp = !scaleUp;
        }

        float delta = scaleAmount * Time.deltaTime * scaleSpeed;
        Vector3 change = Vector3.one * delta;
        obj.transform.localScale += scaleUp ? change : -change;

        // Clamp して暴走防止
        float min = originalScale.x - scaleAmount;
        float max = originalScale.x + scaleAmount;
        float clamped = Mathf.Clamp(obj.transform.localScale.x, min, max);
        obj.transform.localScale = new Vector3(clamped, clamped, clamped);
    }
}

