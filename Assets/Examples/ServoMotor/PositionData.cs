using UnityEngine;
using UnityEngine.UI;

public class PositionData : MonoBehaviour
{
    public PositionManager manager;
    public ServoActuator actuator;

    //포지션의 순서 텍스트
    public Text idText;
    //포지션 데이터 텍스트
    public Text positionText;

    private int _position;

    //처음에 어떻게 표현되어야 하는지 초기화
    public void Init(int index, int position)
    {
        _position = position;
        idText.text = "P" + index.ToString("D3");
        positionText.text = position.ToString();
    }

    //순서가 변경될 때 호출
    public void ChangeIndex(int index)
    {
        idText.text = "P" + index.ToString("D3");
    }

    //삭제할 때 호출
    public void Delete(bool removeData)
    {
        if (removeData)
            manager.RemoveData(this);

        Destroy(gameObject);
    }

    public void Go()
    {
        actuator.Cmd_Positioning(_position);
    }
}
