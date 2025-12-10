using UnityEngine;

public class ACMotorController : MonoBehaviour
{
    //회전 샤프트의 리지드바디
    public Rigidbody shaft;
    //회전 축
    public Vector3 torqueAxis = Vector3.forward;

    //초당 최대 회전 속도
    public float maxVelocity = 60f;
    //초당 토크
    public float torque = 1f;

    //정방향 회전에 전류가 흐르냐
    [SerializeField]
    private bool _isOnForward = false;
    //역방향 회전에 전류가 흐르냐
    [SerializeField]
    private bool _isOnBackward = false;
    //회전 방향
    private float _torqueDirection = 0f;

    private void Start()
    {
        //GetComponentInChildren<구성요소>() 자식들 중에서 지정한 구성요소 타입을 찾아
        //있으면 가져오고, 없으면 null 반환
        //직계 자손이여야 함.
        shaft = GetComponentInChildren<Rigidbody>();
        //최대 회전 속도 제한
        //shaft.maxAngularVelocity = maxVelocity;

        IsOnForward = false;
        IsOnBackward = false;
    }

    private void FixedUpdate()
    {
        //rigidbody.AddRelativeTorque(Vector3) 로컬 축의 회전방향으로 토크를 발생시킨다.
        shaft.AddRelativeTorque(_torqueDirection * torque * Time.fixedDeltaTime * torqueAxis);
    }


    public bool IsOnForward
    {
        get => _isOnForward;
        set
        {
            //if (_isOnForward == value)
            //    return;

            //정방향 회전이 On되면 역방향 회전의 전류를 Off시킨다.
            if (_isOnForward = value)
                _isOnBackward = false;

            //현재 전류 흐름을 확인해 회전방향을 정한다.
            if (_isOnForward)
                _torqueDirection = -1f;
            else if (_isOnBackward)
                _torqueDirection = 1f;
            else
                _torqueDirection = 0f;
        }
    }

    public bool IsOnBackward
    {
        get => _isOnBackward;
        set
        {

            //if (_isOnBackward == value)
            //    return;            

            //역방향 회전이 On되면 정방향 회전의 전류를 Off시킨다.
            if (_isOnBackward = value)
                _isOnForward = false;

            //현재 전류 흐름을 확인해 회전방향을 정한다.
            if (_isOnForward)
                _torqueDirection = -1f;
            else if (_isOnBackward)
                _torqueDirection = 1f;
            else
                _torqueDirection = 0f;
        }
    }
}
