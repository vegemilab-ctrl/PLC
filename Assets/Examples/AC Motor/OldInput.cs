using UnityEngine;
using UnityEngine.Events;

public class OldInput : MonoBehaviour
{
    public ACMotorController controller;


    private void Start()
    {
        controller = GetComponent<ACMotorController>();
    }
    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.A))
        //{
        //    controller.InvertForward();
        //}

        if(Input.GetKeyDown(KeyCode.A))
        {
            controller.IsOnForward = true;
        }

        else if(Input.GetKeyDown(KeyCode.A))
        {
            controller.IsOnForward = false;
        }

        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    controller.InvertBackward();
        //}

        if (Input.GetKeyDown(KeyCode.D))
        {
            controller.IsOnForward = true;
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            controller.IsOnForward = false;
        }

        float ho = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Debug.Log($"H : {ho}, V : {ver}");
        if(Input.GetButton("Fire1"))
        {
            Debug.Log("Fire");
        }

        if(Input.GetMouseButtonUp(1))
        {
            Debug.Log("마우스 오른쪽버튼 방끔 뗐다.");
        }
    }

    ////인풋 키1 지정
    //public KeyCode input1 = KeyCode.None;
    ////인풋 키1 눌렀을 때 호출할 함수 델리게이트
    //public UnityEvent input1Down;
    ////인풋 키1 누르는 동안 호출할 함수 델리게이트
    //public UnityEvent input1Hold;
    ////인풋 키 1 누르는 순간 호출할 함수 델리게이트
    //public UnityEvent input1Release;

    //public KeyCode input2 = KeyCode.None;
    //public UnityEvent input2Down;
    //public UnityEvent input2Hold;
    //public UnityEvent input2Release;
    //public KeyCode input3 = KeyCode.None;
    //public UnityEvent input3Down;
    //public UnityEvent input3Hold;
    //public UnityEvent input3Release;
    //public KeyCode input4 = KeyCode.None;
    //public UnityEvent input4Down;
    //public UnityEvent input4Hold;
    //public UnityEvent input4Release;


    //void Update()
    //{
    //    //만약 인풋 키1이 지정이 되어있으면
    //    if (input1 != KeyCode.None)
    //    {
    //        //방금 인풋키1을 눌렀나 물어보고
    //        if (Input.GetKeyDown(input1))
    //        {
    //            //맞으면 등록된 콜백함수를 호출 / ?는 드래그한 게 있으면 호출, 없으면 호출하지않음(그냥 .이면 있든없든 호출함)
    //            input1Down?.Invoke();
    //        }
    //        //이전부터 계속 누르고 있는 상태인지 물어보고
    //        else if (Input.GetKey(input1))
    //        {
    //            //맞으면 등록된 콜백함수를 호출
    //            input1Hold?.Invoke();
    //        }
    //        //방금 인풋키1을 뗏는지 물어보고
    //        else if (Input.GetKeyUp(input1))
    //        {
    //            //맞으면 등록된 콜백함수를 호출
    //            input1Release?.Invoke();
    //        }

    //    }

    //    if (input2 != KeyCode.None)
    //    {
    //        if (Input.GetKeyDown(input2))
    //        {
    //            input2Down?.Invoke();
    //        }

    //        else if (Input.GetKey(input2))
    //        {
    //            input2Hold?.Invoke();
    //        }

    //        else if (Input.GetKeyUp(input2))
    //        {
    //            input2Release?.Invoke();
    //        }

    //    }

    //    if (input3 != KeyCode.None)
    //    {
    //        if (Input.GetKeyDown(input3))
    //        {
    //            input3Down?.Invoke();
    //        }

    //        else if (Input.GetKey(input3))
    //        {
    //            input3Hold?.Invoke();
    //        }

    //        else if (Input.GetKeyUp(input3))
    //        {
    //            input3Release?.Invoke();
    //        }
    //    }

    //    if (input4 != KeyCode.None)
    //    {
    //        if (Input.GetKeyDown(input4))
    //        {
    //            input4Down?.Invoke();
    //        }

    //        else if (Input.GetKey(input4))
    //        {
    //            input4Hold?.Invoke();
    //        }

    //        else if (Input.GetKeyUp(input4))
    //        {
    //            input4Release?.Invoke();
    //        }
    //    }
    //}
}