using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ���̃N���X�̐����������āI�I�I
/// </summary>
public class Product : MonoBehaviour
{
    [SerializeField] GameObject productPrefab;

    [SerializeField] string productName;

    private OrderFromLeft orderLeft;
    private OrderFromRight orderRight;
    private GameManager gameManager;
    private GameObject findOrderL;
    private GameObject findOrderR;
    private GameObject findGameManager;

   private bool isOrderSheetL = false;
    private bool isOrderSheetR = false;

    //�R�����g���c���ꍇ�͂Ȃ��c���Ă��邩�������Ă�������
    //[SerializeField]
    //RectTransform uiElement; // Reference to the UI element (e.g., a drop target)

    private Camera mainCamera;
    private Vector3 offset;

    private void Start()
    {
        mainCamera = Camera.main;

        findOrderL = GameObject.Find("�d�l��L");

        findOrderR = GameObject.Find("�d�l��R");

        findGameManager = GameObject.FindWithTag("GameController");

        orderLeft = findOrderL.GetComponent<OrderFromLeft>();

        orderRight = findOrderR.GetComponent<OrderFromRight>();

        gameManager = findGameManager.GetComponent<GameManager>();

        //�R�����g���c���ꍇ�͂Ȃ��c���Ă��邩�������Ă�������
        //findOrder = GameObject.FindWithTag("OrderSheet");

        //order =  findOrder.GetComponent<OrderFrom>();
    }

    private void OnMouseUp()
    {
        //�R�����g���c���ꍇ�͂Ȃ��c���Ă��邩�������Ă�������
        //IsOrderSheet();
        //orderLeft.IsOrderSheet();

        if (IsSpriteOverUI() && isOrderSheetL)
        {
            if (productName == orderLeft.OrderproductTextList[0].text)
            {
                Debug.Log("Sprite dropped on UI!");
                OrderConfirmationLeft(0);//�}�W�b�N�i���o�[�ł��B�ǂ��ϐ������āA���܂��ł��邩�l���Ă��������B
            }
            else if (productName == orderLeft.OrderproductTextList[1].text)
            {
                Debug.Log("Sprite dropped on UI!!");
                OrderConfirmationLeft(1);//�}�W�b�N�i���o�[�ł��B�ǂ��ϐ������āA���܂��ł��邩�l���Ă��������B
            }
            else if (productName == orderLeft.OrderproductTextList[2].text)
            {
                Debug.Log("Sprite dropped on UI!!!");
                OrderConfirmationLeft(2);//�}�W�b�N�i���o�[�ł��B�ǂ��ϐ������āA���܂��ł��邩�l���Ă��������B
            }
            else
            {
                Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
        }

        if (IsSpriteOverUI() && isOrderSheetR)
        {
            if (productName == orderRight.OrderproductTextList[0].text)
            {
                Debug.Log("Sprite dropped on UI!");
                OrderConfirmationRight(0);//�}�W�b�N�i���o�[�ł��B�ǂ��ϐ������āA���܂��ł��邩�l���Ă��������B
            }
            else if (productName == orderRight.OrderproductTextList[1].text)
            {
                Debug.Log("Sprite dropped on UI!!");
                OrderConfirmationRight(1);//�}�W�b�N�i���o�[�ł��B�ǂ��ϐ������āA���܂��ł��邩�l���Ă��������B
            }
            else if (productName == orderRight.OrderproductTextList[2].text)
            {
                Debug.Log("Sprite dropped on UI!!!");
                OrderConfirmationRight(2);//�}�W�b�N�i���o�[�ł��B�ǂ��ϐ������āA���܂��ł��邩�l���Ă��������B
            }
            else
            {
                Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
        }

        //�R�����g���c���ꍇ�͂Ȃ��c���Ă��邩�������Ă�������
        //if (productName == orderRight.OrderproductTextList[0].text)
        //{
        //    Debug.Log("Sprite dropped on UI!");
        //    OrderConfirmationRight(0);
        //}
        //else if (productName == orderRight.OrderproductTextList[1].text)
        //{
        //    Debug.Log("Sprite dropped on UI!!");
        //    OrderConfirmationRight(1);
        //}
        //else if (productName == orderRight.OrderproductTextList[2].text)
        //{
        //    Debug.Log("Sprite dropped on UI!!!");
        //    OrderConfirmationRight(2);
        //}
        //else
        //{
        //    Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        //}

        // Handle the drop action

    }

    private void OrderConfirmationLeft(int elementNum)
    {
        Destroy(gameObject);
       
      
        if (orderLeft.OrderNumList[elementNum] > 1)
        {
            gameManager.PlaySound(0);
            orderLeft.OrderNumList[elementNum]--;
            orderLeft.OrderNumTextList[elementNum].text = orderLeft.OrderNumList[elementNum].ToString();
          
        }
        else
        {
            gameManager.PlaySound(1);
            orderLeft.OrderNumList[elementNum] = 0;
            orderLeft.OrderNumTextList[elementNum].text = "�����傤!!";
            orderLeft.OrderNumTextList[elementNum + 3].enabled = false; //�}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B
        }
    }

    private void OrderConfirmationRight(int elementNum)
    {
        Destroy(gameObject);


        if (orderRight.OrderNumList[elementNum] > 1)
        {
            gameManager.PlaySound(0);
            orderRight.OrderNumList[elementNum]--;
            orderRight.OrderNumTextList[elementNum].text = orderRight.OrderNumList[elementNum].ToString();

        }
        else
        {
            gameManager.PlaySound(1);
            orderRight.OrderNumList[elementNum] = 0;
            orderRight.OrderNumTextList[elementNum].text = "�����傤!!";
            orderRight.OrderNumTextList[elementNum + 3].enabled = false; //�}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "�d�l��L")
        {
            isOrderSheetL = true; // ������L �ɐG�ꂽ���Ƃ��L�^
         
        }

        if (other.gameObject.name == "�d�l��R")
        { 
          isOrderSheetR = true;
           
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "�d�l��L")
        {
            isOrderSheetL = false; // ������L���痣�ꂽ�����
           
        }

        if (other.gameObject.name == "�d�l��R")
        {
            isOrderSheetR = false; // ������L���痣�ꂽ�����
        }
    }

//�R�����g�c���ꍇ�A�Ȃ��c���Ă��邩���R�������Ă��������B
    //private void OnMouseDown()
    //{
    //    offset = transform.position - GetMouseWorldPosition();
    //}

    //private void OnMouseDrag()
    //{
    //    transform.position = GetMouseWorldPosition() + offset;

    //    // Check if the sprite is interacting with the UI element
    //    if (IsSpriteOverUI())
    //    {
    //        Debug.Log("Sprite is over UI!");
    //    }
    //}

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = mainCamera.nearClipPlane;  // Set z position to near clip plane
        return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    }

    private bool IsSpriteOverUI()
    {
        // Convert world position to screen position
        Vector2 spriteScreenPos = mainCamera.WorldToScreenPoint(transform.position);

        // Raycast using the UI GraphicRaycaster
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = spriteScreenPos
        };

        // Create a list of results
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
    

        // Check if we hit any UI elements
        return results.Count > 0;
    }

    //�Ȃ��c���Ă��邩�����āB�c���K�v���Ȃ��ꍇ�͏����Ă��������B
    //private string IsOrderSheet()
    //{

    //    clickedGameObject = null;

    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

    //    if (hit2d)
    //    {
    //        clickedGameObject = hit2d.transform.gameObject;
    //    }

    //    Debug.Log(clickedGameObject);

    //    return clickedGameObject.name;
    //}

}
