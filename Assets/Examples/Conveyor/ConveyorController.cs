using System.Collections.Generic; //<-- List 또는 Dictionary 사용하려면 적어줘야 함.
using NUnit.Framework;
using realvirtual;
using UnityEngine;

public class ConveyorController : MonoBehaviour
{
    public ConveyorBelt belt;
    public List<Collider> enterList;
    //public Vector2 direction = Vector2.up;
    //public float speedPerSec = 1f;

    //private Material _material;
    //private float _uvOffset;

    private void Awake()
    {
        //_material = GetComponent<MeshRenderer>().material;

        if (belt == null)
            belt = GetComponent<ConveyorBelt>();
    }

    //트리거 영역에 다른 콜라이더가 들어왔을 때 호출, 들어오는 족족 넣고
    private void OnTriggerEnter(Collider other)
    {
        enterList.Add(other);
    }

    //트리거 영역 밖으로 다른 콜라이더가 나갔을 때 호출, 들어오늘 족족 빼냄
    private void OnTriggerExit(Collider other)
    {
        enterList.Remove(other);
    }

    private void FixedUpdate()
    {
        //벨트의 이동속도 * 지나간 시간 * 이동 방향 => 앞으로 이동할 방향과 거리
        Vector3 moveDelta = belt.speed * Time.fixedDeltaTime * transform.forward;
        foreach (Collider col in enterList)
            //콜라이더는 움직이지않고 형태일 뿐, 물음표를 붙이면 있으면 true, 없으면 false
            //현재 위치 + 앞으로 이동할 방향과 거리 => 있어야할 위치
            col.attachedRigidbody?.MovePosition(col.attachedRigidbody.position + moveDelta);
    }
}
//private void Update()
//{
//_uvOffset += speedPerSec * Time.deltaTime;
//if(_uvOffset > 1f)
//{
//    _uvOffset -= 1f;
//}

//_material.SetTextureOffset("_BaseMap", direction * _uvOffset);
//_material.SetTextureOffset("_BumpMap", direction * _uvOffset);
//}


