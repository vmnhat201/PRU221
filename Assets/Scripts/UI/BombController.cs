using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BombController : MonoBehaviour, IPointerDownHandler,
    IPointerUpHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image imageCount;
    private float buttonPressDuration = 0f;
    float forceMultiplier = 5f; // Hệ số nhân lực
    private bool isReady = true;
    public float destroyTime = 4f;
    public float cooldown = 5f;

    private bool isButtonPressed = false;
    private bool isButtonReleased = false;
    private float buttonPressStartTime = 0f;
    private Coroutine countdownCoroutine;
    public GameObject bomObjectPrefab;
    private GameObject bombObject;

    private void Start()
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Begin");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Ended");
    }
    void Update()
    {
        if (isButtonPressed && isButtonReleased)
        {

            isButtonPressed = false;
            isButtonReleased = false;
            if (countdownCoroutine == null)
            {
                countdownCoroutine = StartCoroutine(CountDownUtil(cooldown));
            }

        }
        if (!isReady)
        {
            imageCount.fillAmount += Time.deltaTime / cooldown;
            if (imageCount.fillAmount >= 1)
            {

                imageCount.fillAmount = 0;
                isReady = true;
            }
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown: " + eventData.pointerCurrentRaycast.gameObject.name);
        if (bombObject != null)
        {
            return;
        }

        Vector3 position = GameManager.instance.player.transform.position;
        Quaternion rotation = Quaternion.Euler(0f, 0f, GameManager.instance.player.transform.eulerAngles.z);

        // Chuyển đổi vector sang hệ tọa độ toàn cầu
        Vector3 offset = GameManager.instance.player.transform.TransformDirection(new Vector3(-0.6f, 0.6f, 0f));
        position += offset;

        Debug.Log("Sinh ra bomb");
        bombObject = Instantiate(bomObjectPrefab, position, rotation);
        bombObject.transform.SetParent(GameManager.instance.player.transform);

        isButtonPressed = true;
        Invoke("DestroyBomb", destroyTime);
        isButtonReleased = false;
        buttonPressStartTime = Time.time;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp");
        ThrowBomb();
        isButtonReleased = true;
        isReady = false;

    }

    IEnumerator CountDownUtil(float time)
    {
        yield return new WaitForSeconds(time);
        countdownCoroutine = null;
    }

    private void ThrowBomb()
    {   
        if(bombObject != null)
        {
            return;
        }
        Rigidbody2D bombRigidbody = bombObject.GetComponent<Rigidbody2D>();

        // Tính toán lực ném dựa trên thời gian giữ nút. Bạn có thể điều chỉnh các giá trị theo ý muốn.
        float throwForce = buttonPressDuration * forceMultiplier;
        Debug.Log("Throw Force: " + throwForce);

        // Tính toán hướng ném (direction). Trong ví dụ này, chúng ta sử dụng Vector2.right làm hướng ném.
        Vector2 throwDirection = Vector2.right;

        // Áp dụng lực ném lên bomb theo hướng và lực xác định.
        bombRigidbody.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);

    }

    private void DestroyBomb()
    {
        Destroy(bombObject);
    }
}
