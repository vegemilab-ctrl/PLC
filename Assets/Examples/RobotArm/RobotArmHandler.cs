using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RobotArmHandler : MonoBehaviour
{
    public Transform target = null;
    public Transform targetIK;

    public float scale = 1f;
    public float pickableDistance = -2f;

    public Rig animationRig;
    public MultiAimConstraint root;
    public TwoBoneIKConstraint arm;
    public MultiAimConstraint tip;

    public void SetTarget(Transform target)
    {
        this.target = target; 
        targetIK.position = target.position;
    }

    private void Update()
    {
        if(target == null)
        {
            root.weight = Mathf.Clamp01(root.weight - Time.deltaTime * scale);
            arm.weight = Mathf.Clamp01(arm.weight - Time.deltaTime * scale);
            tip.weight = Mathf.Clamp01(tip.weight - Time.deltaTime * scale);
            return;
        }

        targetIK.position = target.position;


        if(Vector3.Distance(target.position, transform.position) < pickableDistance)
        {
            root.weight = Mathf.Clamp01(root.weight + Time.deltaTime * scale);
            arm.weight = Mathf.Clamp01(arm.weight + Time.deltaTime * scale);
            tip.weight = Mathf.Clamp01(tip.weight + Time.deltaTime * scale);
        }

        else
        {
            root.weight = Mathf.Clamp01(root.weight - Time.deltaTime * scale);
            arm.weight = Mathf.Clamp01(arm.weight - Time.deltaTime * scale);
            tip.weight = Mathf.Clamp01(tip.weight - Time.deltaTime * scale);
        }
    }
}
