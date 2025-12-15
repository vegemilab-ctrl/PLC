using UnityEngine;

[RequireComponent(typeof(Rigidbody))]   //<---RequireComponent(typeof(반드시 있어야 하는 컴포넌트)), 실린더에는 리지드바디가 반드시 필요함.

public class CylinderController : MonoBehaviour
{
    //실린더의 전진 방향(로컬)
    public Vector3 forward = Vector3.forward;
    //물리 강체
    public Rigidbody rod;
    //전후진시 가하는 힘.
    public float power = 100f;

    private void Awake()
    {
        rod = GetComponent<Rigidbody>();
        rod.useGravity = false;
        rod.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void OnForward()
    {
        rod.AddRelativeForce(forward * power);
    }

    public void OnBackward()
    {
        rod.AddRelativeForce(-forward * power);
    }
}