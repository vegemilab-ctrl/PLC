using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlUI_3_46SDSR31H : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public string controlUIName;
    public string normalLayerName = "Default";
    public string outlineLayerName = "Outline";
    public Camera eventCam;
    public RectTransform controlUI;
    public Transform uiCenter;
    public bool needUpdateUIPosition;
    public float duration = 0.5f;
    public Ease easingType = Ease.OutBounce;

    private bool _isUIDisplayed = false;
    private bool _isEntered = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(controlUI == null && !string.IsNullOrEmpty(controlUIName))
        {
            GameObject go = GameObject.Find(controlUIName);
            if(go != null)
            {
                controlUI = go.GetComponent<RectTransform>();
            }
        }

        if(controlUI != null)
        {
            controlUI.localScale = Vector3.zero;
            controlUI.gameObject.SetActive(false);
        }

        if(eventCam == null)
        {
            eventCam = Camera.main;
        }

        if(uiCenter == null)
        {
            uiCenter = transform.Find("UICenter");
        }
    }

    private void Update()
    {
        if (!_isUIDisplayed || !needUpdateUIPosition || controlUI == null)
            return;

        controlUI.position = eventCam.WorldToScreenPoint(uiCenter.position);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
