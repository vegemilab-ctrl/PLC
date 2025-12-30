using UnityEngine;
using UnityEngine.Events;
using ActUtlType64Lib;

public class MxComponent : MonoBehaviour
{
    //MX Component 라이브러리 인터페이스
    private ActUtlType64 utltype;

    //읽어올 디바이스 주소0
    public string address0;
    public UnityEvent<bool> onChangeValue0;
    //읽어올 디바이스 주소1
    public string address1;
    public UnityEvent<bool> onChangeValue1;
    //읽어올 디바이스 주소2
    public string address2;
    public UnityEvent<bool> onChangeValue2;

    private bool _isConnected = false;

    void Start()
    {
        //시작하면 인터페이스를 생성해서
        utltype = new ActUtlType64()
        {
            ActLogicalStationNumber = 1
        };

        //PLC와 통신을 연결.
        int ret = utltype.Open();
        if (ret != 0)
        {
            Debug.Log($"0x{ret:x}");
        }
        else
        {
            _isConnected = true;
            Debug.Log("성공적으로 시뮬레이터와 연결되었습니다.");
        }

    }

    //어플리케이션이 종료될 때 호출되는 함수
    private void OnApplicationQuit()
    {
        if (utltype != null)
        {
            //PLC와 통신 연결 해제.
            int ret = utltype.Close();
            if (ret != 0)
            {
                Debug.Log($"0x{ret:x}");
            }
            else
            {
                Debug.Log("성공적으로 시뮬레이터와 연결해제되었습니다.");
            }
        }
    }

    private void Update()
    {
        if (!_isConnected)
            return;

        int ret = utltype.GetDevice2(address0, out short result);
        if (ret == 0)
        {
            onChangeValue0.Invoke(result != 0);
        }
        else
        {
            Debug.LogWarning($"{address0}의 디바이스 정보를 가져오는데 실패했습니다.({ret:x})");
        }

        ret = utltype.GetDevice2(address1, out result);
        if (ret == 0)
        {
            onChangeValue1.Invoke(result != 0);
        }
        else
        {
            Debug.LogWarning($"{address1}의 디바이스 정보를 가져오는데 실패했습니다.({ret:x})");
        }

        ret = utltype.GetDevice2(address2, out result);
        if (ret == 0)
        {
            onChangeValue2.Invoke(result != 0);
        }
        else
        {
            Debug.LogWarning($"{address2}의 디바이스 정보를 가져오는데 실패했습니다.({ret:x})");
        }
    }
}