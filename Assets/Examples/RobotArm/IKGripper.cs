using UnityEngine;
using DG.Tweening;

public class IKGripper : MonoBehaviour
{
    //잡은 제품의 리지드바디를 변수 놔둠
    public Rigidbody child;
    //현재 잡은 제품이 있는지 여부
    public bool hasItem = false;

    //제품을 잡아라
    public bool Pick()
    {
        if (child == null)
            return false;

        //child는 리지드바디, return - 잡는데 성공했다. 아이템을 자식으로 넣고, 아이템을 가지고 있다에 true
        child.isKinematic = true;
        child.transform.SetParent(transform);
        hasItem = true;
        return true;
    }

    //제품을 놓아라
    public bool Drop()
    {
        if (child == null)
            return false;

        //isKinematic 끄고, child 해제, 아이템을 가지고있는지 여부를 bool값에 반환
        child.transform.SetParent(null);
        child.isKinematic = false;
        child = null;
        hasItem = false;
        return true;
    }

    //그리퍼 안에 들어온 제품이 있으면 추가
    private void OnTriggerEnter(Collider other)
    {
        if(child == null)
        child = other.attachedRigidbody;
    }

    //그리퍼 밖으로 나가는 제품이 있으면 제거
    private void OnTriggerExit(Collider other)
    {
        if (child == other.attachedRigidbody)
            child = null;
    }
}