using System;
using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]


public class ServoActuator : MonoBehaviour

{
    //액츄에이터의 구동 방식
    public enum ActuatorType
    {
        Linear,
        Rotary
    }

    //컨트롤 유닛 타입
    public enum ControlUnit
    {
        mm,
        degree,
        pulse
    }

    public enum ActuatorState
    {
        Idle,
        Jogging,
        Positioning,
        Homing_Search,
        Homing_Retry,
        Homing_Creep,
        Error
    }

    [Header("[1] 기구 및 단위 설정")]
    public ActuatorType actuatorType = ActuatorType.Linear;
    public ControlUnit controlUnit = ControlUnit.mm;

    [Header("[2] 기구 파라미터")]
    public float motorResolution = 131072f;
    public float gearRatio = 1.0f;
    public float ballscrewLead = 10f;

    [Header("[3] 속도 파라미터")]
    [Tooltip("위치 결정 운전시 최대 속도")]
    public float maxSpeed = 500.0f;

    [Tooltip("수동 JOG 운전시 속도")]
    public float jogSpeed = 100.0f;

    [Tooltip("원점 도그를 찾으러 갈 때 속도(고속)")]
    public float homingHighSpeed = 200.0f;

    [Tooltip("도그 감지 후 정밀하게 멈출 때 속도(저속)")]
    public float homingCreepSpeed = 20.0f;

    [Header("[4] 가감속 시간")]
    public float accelTime = 100.0f;
    public float inPosWidth = 0.1f;

    [Header("모니터링")]
    public float currentPos_Unit;
    public int currentPulse;

    [Header("센서 입력")]
    public bool LimitSP = false;    //상한 리미트
    public bool LimitSN = false;    //하한 리미트
    public bool DOG = false;        //근점 도그

    public bool IsReady = false;       //명령을 받을 수 있는 상태 여부
    public bool IsPosition = false;    //명령 수행 완료 여부
    public bool isWarning = false;     //에러 경고
    public bool OPR_Complete = false;  //원점복귀 완료 여부

    //내부에서 처리하기 용이하게 하기위한 
    private bool _cmd_SON = false;      //서보 모터에 전원을 켜라는 명령이 들어온 상태
    private bool _cmd_StartPos = false; //지정 위치로 이동하라는 명령이 들어온 상태
    private bool _cmd_StartOPR = false; //원점 복귀하라는 명령이 들어온 상태
    private bool _cmd_JogFwd = false;   //수동 조그 전진 명령이 들어온 상태
    private bool _cmd_JogRev = false;   //수동 조그 후퇴 명령이 들어온 상태
    private int _cmd_TargetPulse = 0;   //목표 위치 펄스값

    private ConfigurableJoint _joint;
    private float currentVelocity_Unit = 0f;
    private float internalTarget_Unit = 0f;

    [SerializeField]
    private ActuatorState _currentstate = ActuatorState.Idle;
    private bool _homingHitDog = false;
    private int _homingDir = -1;

    private void Awake()
    {
        _joint = GetComponent<ConfigurableJoint>();

        //시작 위치 계산
        float startPos = (actuatorType == ActuatorType.Linear)?
            -transform.localPosition.z * 1000.0f : transform.localRotation.eulerAngles.x;


    }



    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
