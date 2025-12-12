using System.Collections.Generic;     //List, Dictionary 같은 배열을 사용할 때 필요한 라이브러리
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider))]
public class MagneticSensor : MonoBehaviour
{
    //센서 레이어 이름
    public string layerName = "Sensor";
    //센서가 감지 여부작 변경될때마다 호출할 콜백함수가 들어있는 델리게이트 (델리게이트 : 함수를 담을 수 있는 배열)
    public UnityEvent<bool, string> onChangeDetected;

    //List나 Dictionary를 쓸 때 빨간색으로 에러가 난다면 최상단에 using System.Collections.Generic;를 써야함.
    //현재 감지 여부
    private bool _hasDetectedPrev = false;
    private bool _hasDectected = false;
    public List<Collider> _detectedList;

    private void Start()
    {
        //활성화시 게임오브젝트의 레이어를 센서로 변경한다.
        gameObject.layer = LayerMask.NameToLayer(layerName);
    }

    //콜라이더에 다른 콜라이더가 들어왔을 경우 호출됨.(IsTrigger 체크되어 있어야만 함)
    //감지되었다고 알려줌 -> 전기신호를 보냄, 마그네틱이 1개 이상들어왔을 경우를 대비해서 숫자를 카운트함(감지를 하고 있는 상태)
    private void OnTriggerEnter(Collider other)
    {
        _detectedList.Add(other);
        _hasDectected = _detectedList.Count > 0;

        if(_hasDetectedPrev != _hasDectected)        //이전값과 감지된 값이 다른지 확인
        onChangeDetected?.Invoke(_hasDectected, gameObject.name);     //감지하면 무조건 호출, Invoke(bool값이 들어있는 1개를 호출)

        _hasDetectedPrev = _hasDectected;
    }

    //콜라이더에 들어와있던 다른 콜라이더가 나갔을 경우 호출됨.(IsTrigger 체크되어 있어야만 함)
    //감지안하고 있다고 알려줌
    private void OnTriggerExit(Collider other)
    {
        _detectedList.Remove(other);    //나와 같은 콜라이더를 찾아서 제거함
        _hasDectected = _detectedList.Count > 0;

        if (_hasDetectedPrev != _hasDectected)
            onChangeDetected?.Invoke(_hasDectected, gameObject.name);

        _hasDetectedPrev = _hasDectected;
    }
}
