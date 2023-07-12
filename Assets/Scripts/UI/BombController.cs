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
    public float buttonPressDuration = 0f;
    // lực đẩy của bom
    public float forceMultiplier = 3f;
    public bool isReady = true;
    public float destroyTime = 4f;
    public float cooldown = 5f;

    public bool isButtonPressed = false;
    public bool isButtonReleased = false;
    public float buttonPressStartTime = 0f;
    public Coroutine countdownCoroutine;
    public GameObject bomObjectPrefab;
    public GameObject bombObject;

    public bool isThrowing = false; // Biến kiểm tra trạng thái đang di chuyển của bom
    public Quaternion throwRotation; // Hướng quay của bom khi ném

    // phạm vi nổ của bom
    public float bombExploseRange = 4f;
    // sức mạnh nổ của bom
    public float bombExploseForce = 40f;


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
        // chỉ khi nào kinematic thì vị trí của bomb mới không bị thay đổi
        bombObject.GetComponent<Rigidbody2D>().isKinematic = true;
        bombObject.transform.SetParent(GameManager.instance.player.transform);
        isButtonPressed = true;
        Invoke("DestroyBomb", destroyTime);
        isButtonReleased = false;
        buttonPressStartTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (bombObject == null || !isReady)
        {
            return;
        }
        // In thông báo "Ném bomb"
        print("Ném bomb");

        // Lưu thời điểm kết thúc nhấn nút
        float buttonPressEndTime = Time.time;

        // Tính thời gian nhấn nút bằng cách lấy hiệu của thời điểm kết thúc và thời điểm bắt đầu
        buttonPressDuration = buttonPressEndTime - buttonPressStartTime;

        // In thông báo về thời gian nhấn nút
        Debug.Log("Thời gian nhấn nút: " + buttonPressDuration);

        // Kích hoạt Rigidbody2D của bombObject để áp dụng vật lý
        bombObject.GetComponent<Rigidbody2D>().isKinematic = false;

        /* 
        Việc in ra những thông tin dưới đây chỉ để minh họa,
        và có tác dụng là chỉ cần in ra một lần.
        Nếu log trong MoveBomb, thông tin sẽ bị in ra liên tục
        */

        // Gán giá trị quay của player vào throwRotation
        throwRotation = GameManager.instance.player.transform.rotation;

        // Tính toán hướng ném dựa trên throwRotation và vector (0, 1)
        Vector3 throwDirection = throwRotation * Vector3.up;

        // Tính khoảng cách ném dựa trên thời gian nhấn nút và hệ số forceMultiplier
        float throwDistance = buttonPressDuration * forceMultiplier;

        // Lấy Rigidbody2D của bombObject
        Rigidbody2D bombRigidbody = bombObject.GetComponent<Rigidbody2D>();

        // Tính toán lực ném dựa trên hướng ném và khoảng cách ném
        Vector2 throwForce = throwDirection * throwDistance;

        // Áp dụng lực ném vào bombRigidbody bằng cách sử dụng Impulse
        bombRigidbody.AddForce(throwForce, ForceMode2D.Impulse);

        // Đánh dấu đang thực hiện ném
        isThrowing = true;

        // Loại bỏ parent của bombObject để nó di chuyển tự do
        bombObject.transform.SetParent(null);

        // Đánh dấu nút đã được nhả ra
        isButtonReleased = true;

        // Phải ném bomb trước khi đặt isReady thành false
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

    private void MoveBomb()
    {
        if (bombObject == null)
        {
            return;
        }
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
                    AntEnemy ant = collider.gameObject.GetComponent<AntEnemy>();
                    BeeEnemy bee = collider.gameObject.GetComponent<BeeEnemy>();
                    RangedEnemy ranged = collider.gameObject.GetComponent<RangedEnemy>();
                    BossEnemy boss = collider.gameObject.GetComponent<BossEnemy>();
                    Boss1Enemy boss1 = collider.gameObject.GetComponent<Boss1Enemy>();
                    if (ant != null)
                    {
                        ant.TakeDamage(bombExploseForce);
                    }
                    if (bee != null)
                    {
                        bee.TakeDamage(bombExploseForce);
                    }
                    if (ranged != null)
                    {
                        ranged.TakeDamage(bombExploseForce);
                    }
                    if (boss != null)
                    {
                        boss.TakeDamage(bombExploseForce);
                    }
                    if (boss1 != null)
                    {
                        boss1.TakeDamage(bombExploseForce);
                    }

                }
            }
        }
    }

}
