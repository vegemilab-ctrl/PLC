using UnityEngine;
using UnityEngine.Events;

//리지드바디가 인스펙터에 강제로 들어감
[RequireComponent(typeof(Rigidbody))]

public class InverterController : MonoBehaviour
{
    public enum RotateAxis
    {
        None,
        XAxis,
        YAxis,
        ZAxis,
        Max
    }

    [Header("인버터 파라미터(Setting")]
    public RotateAxis axis = RotateAxis.YAxis;
    public float maxFrequency = 60f;     //최대 주파수
    public float maxRPM = 1800f;         //정격 회전수
    public float accelTime = 1.0f;       //가속시간(0 -> Max 도달하는데 걸리는 시간)
    public float decelTime = 1.0f;       //감속 시간(Max -> 0 도달하는데 걸리는 시간)

    [Header("제어 입력(PLC Input")]
    public bool STF = false;             //정회전 신호
    public bool STR = false;             //역회전 신호
    [Range(0f, 60f)]
    public float targetHz = 0.0f;        //지령 주파수(아날로그 입력)

    //델리게이트, 상태가 바뀜에 따라 ~
    public UnityEvent<bool> onChangedSTF;
    public UnityEvent<bool> onChangedSTR;

    [Header("모니터링(Read Only")]
    [SerializeField] float _currentHz = 0.0f;     //현재 주파수
    [SerializeField] float _currentRPM = 0.0f;    //현재 RPM

    //값을 가져가기만 하도록 Get으로 사용, 변경하지는 못함.
    public float GetCurrentHZ => _currentHz;
    public float GetCurrentRPM => _currentRPM;


    public Rigidbody shaft = null;

    //Awake - 스타트함수와 같지만 더 먼저 실행되는 함수
    private void Awake()
    {
        shaft = GetComponentInChildren<Rigidbody>();
        shaft.maxAngularVelocity = 1000f;
        shaft.constraints = RigidbodyConstraints.FreezePosition;

        switch (axis)
        {
            case RotateAxis.XAxis:
                shaft.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                break;
            case RotateAxis.YAxis:
                shaft.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                break;
            case RotateAxis.ZAxis:
                shaft.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
                break;
            default:
                break;
        }

        shaft.useGravity = false;
        shaft.automaticCenterOfMass = false;
        shaft.automaticInertiaTensor = false;
        shaft.inertiaTensorRotation = Quaternion.identity;
        shaft.inertiaTensor = Vector3.one;
    }

    private void FixedUpdate()
    {
        //타겟 주파수를 알아내기
        float finalTargetHz = 0.0f;

        if (STF && !STR) finalTargetHz = targetHz;
        else if (!STF && STR) finalTargetHz = -targetHz;
        else finalTargetHz = 0f;

        //가감속 로직을 짜야한다.
        float rampRate = maxFrequency / (finalTargetHz != 0 ? accelTime : decelTime);
        _currentHz = Mathf.MoveTowards(_currentHz, finalTargetHz, rampRate * Time.fixedDeltaTime);

        //Hz -> RPM -> Radian/s 변환
        //공식 : RPM = (120 * Hz) / 극수
        _currentRPM = (_currentHz / maxFrequency) * maxRPM;

        //RPM을 각속도로 변환 RPM * 0.10472 = rad/s
        float radPerSec = _currentRPM * 0.10472f;

        Vector3 rotataionAxis = axis switch
        {
            RotateAxis.XAxis => transform.right,
            RotateAxis.YAxis => transform.up,
            RotateAxis.ZAxis => transform.forward,
            _ => Vector3.zero
        };

        //초당 회전 각도 적용하기.
        shaft.angularVelocity = rotataionAxis * radPerSec;
    }

    public bool IsOnSTF
    {
        get => STF;
        set
        {
            if (STF == value)
                return;

            if (STF = value)
            {
                STR = false;
                onChangedSTR?.Invoke(STR);
            }

            onChangedSTF?.Invoke(STF);
        }
    }

    public bool IsOnSTR
    {
        get => STR;
        set
        {
            if (STR == value)
                return;

            if (STR = value)
            {
                STF = false;
                onChangedSTF?.Invoke(STF);
            }
            onChangedSTR?.Invoke(STR);
        }
    }

    public void ChangeTargetHz(float value)
    {
        if (value < 0f || value > maxFrequency)
            return;

        targetHz = value;
    }

    public void IncreaseTargetHz(float value)
    {
        targetHz = Mathf.Clamp(targetHz + value, 0, maxFrequency);
    }

    public void DecreaseTargetHz(float value)
    {
        targetHz = Mathf.Clamp(targetHz - value, 0, maxFrequency);
    }

    //프로퍼티 사용, 누를때마다 반전
    public void PressSTF()
    {
        IsOnSTF = !IsOnSTF;
    }

    public void PressSTR()
    {
        IsOnSTR = !IsOnSTR;
    }
}