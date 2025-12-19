using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIHoldEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent onHold; // 누르고 있을 때 실행될 이벤트
    private bool isPressed = false;

    void Update()
    {
        if (isPressed)
        {
            onHold.Invoke(); // 매 프레임 인스펙터에 등록된 함수 실행
        }
    }

    public void OnPointerDown(PointerEventData eventData) => isPressed = true;
    public void OnPointerUp(PointerEventData eventData) => isPressed = false;
    // 마우스가 버튼 밖으로 나갔을 때 멈추게 하려면 아래 주석 해제
    // public void OnPointerExit(PointerEventData eventData) => isPressed = false;
}
