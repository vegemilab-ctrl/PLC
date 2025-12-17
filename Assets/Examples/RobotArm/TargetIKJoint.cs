using UnityEngine;

public class TargetIKJoint : MonoBehaviour
{
    
    public Transform root;
    public Transform joint;
    public float jointDistance = 0.3f;
    [Range(-90, 90)]
    public float angle = 0f;

    private void Update()
    {
        Vector3 forward = (root.position - transform.position).normalized;
        forward.y = 0f;
        joint.rotation = transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
        joint.Rotate(new(-angle, 0, 0));
        joint.position = joint.forward * jointDistance + transform.position;
    }
}
