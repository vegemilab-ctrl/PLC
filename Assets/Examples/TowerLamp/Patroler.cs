using UnityEngine;

public class Patroler : MonoBehaviour
{
    public Transform[] pathPositions; //패트롤 위치 배열
    public Transform target;   //패트롤 하는 당사자
    public float moveSpeed = 1f;   //패트롤 스피드

    private int _index = 0;   //현재 패트롤 목표 위치 순서

    void Start()
    {
        //transform.childCount는 자식의 갯수를 알아낼 수 있다.
        pathPositions = new Transform[transform.childCount];
        for(int i = 0; i < pathPositions.Length; ++i)
        {
            //transform.GetChild(몇번째); 0부터 시작해서 지정된 순서의 자식을 가져올 수 있다.
            pathPositions[i] = transform.GetChild(i);
        }
    }

    void Update()
    {
        //Vector3.MoveTowards(현재 위치, 목표위치, 최대이동거리); 현재 위치에서 목표 위치를
        //향해 최대 이동거리만 큼 이동한 위치값을 반환한다.
        target.position = Vector3.MoveTowards(
            target.position, 
            pathPositions[_index].position, 
            moveSpeed * Time.deltaTime);

        if(target.position == pathPositions[_index].position)
        {
            ++_index;
            if(_index >= pathPositions.Length)
                _index = 0;
        }
    }
}
