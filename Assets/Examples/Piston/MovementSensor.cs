using UnityEditor;
using UnityEngine;

public class MovementSensor : MonoBehaviour
{
    public enum DetectAxis
    {
        None = 0,
        XAxis,
        XAxisMinus,
        YAxis,
        YAxisMinus,
        ZAxis,
        ZAxisMinus,
        Max
    }

    //검출할 게임오브젝트의 레이어들
    public LayerMask detectLayer;
    //검출 가능 거리
    public float detectableDistance = 0.3f;
    //센서 방향
    public DetectAxis axis = DetectAxis.ZAxis;
    //검출할 수 있는 종류
    public string detectableTag = string.Empty;

    //검출 여부
    private bool _hasDetected = false;
    public bool HasDetected()
    {
        return _hasDetected;
    }

    //검출 위치
    private Vector3 _detectedPoint;


    private void Update()
    {
        //레이저나 적외선을 발사해서 충돌한 애가 누구냐
        Vector3 direction = axis switch
        {
            DetectAxis.XAxis => transform.right,
            DetectAxis.XAxisMinus => -transform.right,
            DetectAxis.YAxis => transform.up,
            DetectAxis.YAxisMinus => -transform.up,
            DetectAxis.ZAxis => transform.forward,
            DetectAxis.ZAxisMinus => -transform.forward,
            _ => Vector3.zero
        };

        if (direction == Vector3.zero)
            return;

        //발사할 레이저를 생성한다. new Ray(레이저 시작위치, 레이저 발사 방향)
        Ray ray = new Ray(transform.position, direction);
        //충돌한게 있으면 true = Physics.Raycast(레이저, out RaycastHit 충돌한 객체 정보, 레이저 유지 거리, 레이저와 충돌할 수 있는 레이어들)
        if (Physics.Raycast(ray, out RaycastHit hit, detectableDistance, detectLayer))
        {
            //검사해야 할 태그가 있지만 검출된 게임오브젝트의 태그가 다를 경우 검출하지 못함.
            if (!string.IsNullOrEmpty(detectableTag) && hit.transform.gameObject.tag != detectableTag)
            {
                _hasDetected = false;
                return;
            }

            _hasDetected = true;
            _detectedPoint = hit.point;
        }
        else
        {
            _hasDetected = false;
        }
    }

    //씬뷰에서 그리고 싶은 것들이 있을 때 이 함수를 사용
    private void OnDrawGizmos()
    {
        Vector3 direction = axis switch
        {
            DetectAxis.XAxis => transform.right,
            DetectAxis.XAxisMinus => -transform.right,
            DetectAxis.YAxis => transform.up,
            DetectAxis.YAxisMinus => -transform.up,
            DetectAxis.ZAxis => transform.forward,
            DetectAxis.ZAxisMinus => -transform.forward,
            _ => Vector3.zero
        };

        if (direction == Vector3.zero)
            return;


        if (_hasDetected)
        {
            //검출될 경우 붉은 색 라인 그리기
            Handles.color = Color.red;
            Handles.DrawLine(transform.position, _detectedPoint);
        }
        else
        {
            //검출 안될 경우 녹색 라인 그리기
            Handles.color = Color.green;
            Handles.DrawLine(transform.position, transform.position + direction * detectableDistance);
        }

    }
}