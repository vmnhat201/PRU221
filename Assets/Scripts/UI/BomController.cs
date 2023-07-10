using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BomController : MonoBehaviour, IPointerDownHandler, IPointerClickHandler,
    IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private bool isButtonPressed = false;
    private float buttonPressStartTime = 0f;
    private float buttonPressDuration = 0f;
    public GameObject bomObject;

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

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Mouse Down: " + eventData.pointerCurrentRaycast.gameObject.name);
        isButtonPressed = true;
        buttonPressStartTime = Time.time;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse Exit");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Mouse Up");
        isButtonPressed = false;
        float buttonPressDuration = Time.time - buttonPressStartTime;
        Debug.Log("Button Press Duration: " + buttonPressDuration + " seconds");
        ThrowBom();
    }
    private void ThrowBom()
    {
        Rigidbody bomRigidbody = bomObject.GetComponent<Rigidbody>();

        // Tính toán lực ném dựa trên thời gian giữ nút. Bạn có thể điều chỉnh các giá trị theo ý muốn.
        float forceMultiplier = 5f; // Hệ số nhân lực
        float throwForce = buttonPressDuration * forceMultiplier;

        // Áp dụng lực ném lên bom
        bomRigidbody.AddForce(transform.forward * throwForce, ForceMode.Impulse);
    }
}
