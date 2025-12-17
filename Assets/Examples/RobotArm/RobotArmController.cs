using UnityEngine;

public class RobotArmController : MonoBehaviour
{
    public Animator anim;
    public Gripper gripper;



    void Start()
    {
        //단 하나만 존재할 때 (씬안의 Gripper 하나를 찾아옴)
        //gripper = FindFirstObjectByType<Gripper>(0);

        if (gripper == null)
        {
            //FindObjectByType<찾고 싶은 클래스 타입>(FindObjectSortMode.None)
            //씬 안에 찾고싶은 클래스 인스턴스들을 모두 찾아 배열로 반환한다.
            Gripper[] grippers = FindObjectsByType<Gripper>(FindObjectsSortMode.InstanceID);

            foreach (Gripper g in grippers)
            {
                if (g.gameObject.name == "Gripper")
                {
                    gripper = g;
                    break;
                }
            }
        }

        if (anim == null)
            anim = GetComponent<Animator>();
    }
    public void Pick()
    {
        anim.SetInteger("Pos", 0);
        anim.SetTrigger("PickTrigger");
    }

    public void Pick(bool isEnter)
    {
        //센서 영역안에 들어올 때만 주울 수 있도록 호출
        if (!isEnter)
            return;

        Pick();
    }

    public void OnPick()
    {
        gripper.Pick();
    }


    public void OnDrop()
    {
        gripper.Drop();
    }

}
