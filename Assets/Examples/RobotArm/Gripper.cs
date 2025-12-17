using UnityEngine;

public class Gripper : MonoBehaviour
{
    //운동량 곱연산
    public float velocityMultiply = 1f;

    //줏을 수 있는 아이템 대기열
    private Transform _pickedItem;

    //바로전 프레임의 손 위치
    private Vector3 _prevPos;
    //현재 운동량
    private Vector3 _velocityPerSec;

    //줏을 수 있는 아이템이 손안에 들어왔을 때 호출되는 트리거 함수
    private void OnTriggerEnter(Collider other)
    {
        if (_pickedItem != null)
            return;

        _pickedItem = other.transform;
    }

    //줏을 수 있는 아이템이 손안에서 나갔을 때 호출되는 트리거 함수
    private void OnTriggerExit(Collider other)
    {
        if (_pickedItem != other.transform)
            return;

        _pickedItem = null;
    }

    public void SetItem(GameObject item)
    {
        _pickedItem = item.transform;
    }

    //줏어라.
    public void Pick()
    {
        if (_pickedItem == null)
            return;

        //손의 자식으로 만들고
        _pickedItem.SetParent(transform);
        //리지드바디를 무적 상태인 IsKenemetic = true로 만들어준다.
        _pickedItem.GetComponent<Rigidbody>().isKinematic = true;
    }

    //떨궈라
    public void Drop()
    {
        if (_pickedItem == null)
            return;

        //손의 자식이 아니게 만들고
        _pickedItem.SetParent(null);

        Rigidbody rb = _pickedItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            //리지드바디를 더이상 무적 상태가 아니게 만든다.
            rb.isKinematic = false;
            //손의 운동량만큼 아이템에도 운동량을 적용한다.
            rb.linearVelocity += _velocityPerSec * velocityMultiply;
        }
        _pickedItem = null;
    }

    private void Update()
    {
        Vector3 delta = transform.position - _prevPos;
        _velocityPerSec = delta / Time.deltaTime;

        _prevPos = transform.position;
    }
}
