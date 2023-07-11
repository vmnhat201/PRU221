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

    private bool isThrowing = false; // Biến kiểm tra trạng thái đang di chuyển của bom
    private float throwSpeed = 3f; // Tốc độ di chuyển của bom (có thể điều chỉnh)
    private Quaternion throwRotation; // Hướng quay của bom khi ném

    // phạm vi nổ của bom
    float bombExploseRange = 3f;


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
        if (isThrowing)
        {
            MoveBomb();
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
        if (isButtonPressed && isButtonReleased)
        {

            isButtonPressed = false;
            isButtonReleased = false;
            if (countdownCoroutine == null)
            {
                countdownCoroutine = StartCoroutine(CountDownUtil(cooldown));
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
        float buttonPressEndTime = Time.time;
        buttonPressDuration = buttonPressEndTime - buttonPressStartTime;
        Debug.Log("Thời gian nhấn nút: " + buttonPressDuration);
        isButtonReleased = true;
        // phải ném bomb trước sau đó mới thực hiện is ready bằng false
        isReady = false;


    }
    IEnumerator CountDownUtil(float time)
    {
        yield return new WaitForSeconds(time);
        countdownCoroutine = null;
    }
    private void DestroyBomb()
    {
        if (bombObject == null)
        {
            return;
        }
        Debug.Log("Hủy bomb");
        ExploseBomb();
        Destroy(bombObject);
    }
    private void ExploseBomb()
    {
        CheckObjectsInRange();
    }
    private void ThrowBomb()
    {
        if (bombObject == null || !isReady)
        {
            return;
        }
        // việc in ra những thông tin dưới đây chỉ để minh họa, và có tác dụng là chỉ cần 1 lần, nếu log
        // trong MoveBomb thì sẽ bị in ra liên tục
        Vector2 throwDirection = Vector2.up; // Hướng ném (có thể điều chỉnh)
        float throwDistance = buttonPressDuration * forceMultiplier; // Khoảng cách ném (có thể điều chỉnh)
        Debug.Log("Hướng ném: " + throwDirection);
        Debug.Log("Khoảng cách ném: " + throwDistance);

        // Di chuyển bomObject từ từ bằng phương pháp time-based movement
        isThrowing = true;
        bombObject.transform.SetParent(null);
    }

    private void MoveBomb()
    {
        if (bombObject == null)
        {
            return;
        }
        float moveDistance = throwSpeed * Time.deltaTime;
        bombObject.transform.Translate(Vector2.up * moveDistance);

        // Kiểm tra điều kiện dừng di chuyển
        float targetDistance = buttonPressDuration * forceMultiplier;
        if (Vector2.Distance(bombObject.transform.position, GameManager.instance.player.transform.position) >= targetDistance)
        {
            isThrowing = false;
        }
    }

    private void CheckObjectsInRange()
    {
        // Lấy tất cả các Collider2D trong bán kính của bom
        Collider2D[] colliders = Physics2D.OverlapCircleAll(bombObject.transform.position, bombExploseRange);

        // Duyệt qua từng Collider2D và xử lý logic tương ứng
        foreach (Collider2D collider in colliders)
        {
            // Kiểm tra nếu collider không phải là bom (chính nó)
            if (collider.gameObject != bombObject)
            {
                // Kiểm tra xem collider có chứa script "Enemies" hay không
                Enemies enemiesScript = collider.gameObject.GetComponent<Enemies>();
                if (enemiesScript != null)
                {
                    // In ra tên của đối tượng Enemies trong bán kính của bom
                    Debug.Log("Đối tượng Enemies trong bán kính của bom: " + collider.gameObject.name);

                    
                }
            }
        }
    }

}
