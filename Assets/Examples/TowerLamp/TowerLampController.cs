using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class TowerLampController : MonoBehaviour, IPointerClickHandler
{
    //각 램프의 게임오브젝트 이름
    [Header("램프이름")]
    public string redLampName;
    public string yellowLampName;
    public string greenLampName;

    //각 램프의 메쉬렌더러
    private MeshRenderer redLamp;
    private MeshRenderer yellowLamp;
    private MeshRenderer greenLamp;

    [Header("Control UI")]
    public bool useUIButton = false;   //UI를 사용할거냐
    public bool needUpdateUIPosition;  //UI 위치 업데이트할 필요가 있냐
    public RectTransform controlUI;    //사용할 UI
    public Transform uiCenter;         //UI 표시 위치 정보
    public float duration = 0.5f;      //등장 애니메이션 길이
    public Ease easingType = Ease.OutBounce;   //애니메이션 타입

    private bool _isUIDisplayed = false;   //현재 UI가 표시되고 있는지

    private void Start()
    {
        //transform.Find("자식의이름") 자식 게임오브젝트 중에서 이름이 같은 게임오브젝트 하나를 리턴한다.
        uiCenter = transform.Find("UICenter");

        //Find한것의 결과값을 겟컴포넌트하고 ~ 메쉬렌더러 구성요소 반환! (순차적으로 진행)
        //transform에서 GameObject로 변경 / 유니티 씬에서 하나만 있어야 함. 아래 문장 주석처리하고 유니티에서 드래그해도 됨.
        //GameObject.Find("이름") 씬안에 존재하는 활성화 되어 있는 게임오브젝트 중에서 해당 이름을 가진 
        //게임오브젝트를 찾아 반환한다.
        greenLamp = GameObject.Find(greenLampName).GetComponent<MeshRenderer>();
        yellowLamp = GameObject.Find(yellowLampName).GetComponent<MeshRenderer>();
        redLamp = GameObject.Find(redLampName).GetComponent<MeshRenderer>();

        //GetComponentInChildren<구성요소>()직계 자손 게임오브젝트안에 있는 구성요소를 가져온다.
        //()안에 true를 넣으면 비활성화 된 구성요소도 가져올 수 있다.
        controlUI = GetComponentInChildren<RectTransform>(true);

        TurnOnRedLamp(false);
        TurnOnYellowLamp(false);
        TurnOnGreenLamp(false);
    }

    private void Update()
    {
        if (!useUIButton || !_isUIDisplayed || !needUpdateUIPosition || controlUI == null)
            return;

        controlUI.position = Camera.main.WorldToScreenPoint(uiCenter.position);
    }

    public void TurnOnRedLamp(bool isOn)
    {
        //메터리얼.EnableKeyward("_EMISSION")함수로 발광효과 켜짐.
        //메터리얼.DisableKeyward("_EMISSION")함수로 발광효과 제거.

        if (isOn)
            redLamp.materials[0].EnableKeyword("_EMISSION");
        else
            redLamp.materials[0].DisableKeyword("_EMISSION");
    }

    public void TurnOnYellowLamp(bool isOn)
    {
        if (isOn)
            yellowLamp.materials[0].EnableKeyword("_EMISSION");
        else
            yellowLamp.materials[0].DisableKeyword("_EMISSION");
    }

    public void TurnOnGreenLamp(bool isOn)
    {
        if (isOn)
            greenLamp.materials[0].EnableKeyword("_EMISSION");
        else
            greenLamp.materials[0].DisableKeyword("_EMISSION");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!useUIButton)
            return;

        if (_isUIDisplayed)
            return;

        if (controlUI == null)
            return;

        _isUIDisplayed = true;
        //transform.SetParent(부모가 될 게임오브젝트의 트랜스폼) 해당 트랜스폼의 부모를 결정할 수 있다.
        //null로 넣으면 부모가 없는 상태로 만들 수 있다.
        controlUI.SetParent(GameObject.Find("Canvas").transform);
        //Camera,main.WorldToScreen(씬 내 게임오브젝트 좌표)로 UI스크린에 해당하는 좌표를 얻을 수 있다.
        controlUI.position = Camera.main.WorldToScreenPoint(uiCenter.position);

        //gameObject.SetActive(온오프) 해당 게임오브젝트의 활성/비활성 변경함수
        controlUI.gameObject.SetActive(true);
        //등장 애니메이션
        controlUI.DOScale(Vector3.one, duration)
            .SetEase(easingType);
    }

    public void CloseUI()
    {
        //퇴장 애니메이션
        controlUI.DOScale(Vector3.zero, duration)
            .SetEase(easingType)
            .OnComplete(CompleteOffUI);
    }
    private void CompleteOffUI()
    {
        controlUI.gameObject.SetActive(false);
        _isUIDisplayed = false;
    }
}
