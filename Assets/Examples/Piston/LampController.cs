using UnityEngine;

public class LampController : MonoBehaviour
{
    // 💡 Unity Inspector에서 연결할 타워 램프 Material
    [Header("타워 램프 Material")]
    public Renderer redLampRenderer;   // Red 램프 오브젝트의 Renderer
    public Renderer greenLampRenderer; // Green 램프 오브젝트의 Renderer

    // (선택 사항) Yellow 램프는 일반적으로 대기/경고 상태에 사용되지만, 여기서는 사용하지 않습니다.
    // public Renderer yellowLampRenderer;

    [Header("센서 연결")]
    public MovementSensor forwardSensor; // 실린더 앞쪽 (전진) 센서
    public MovementSensor backwardSensor; // 실린더 뒤쪽 (후진) 센서

    [Header("램프 발광 설정")]
    // 💡 램프가 켜졌을 때의 Emission 색상과 꺼졌을 때의 색상
    public Color onColorRed = Color.red;
    public Color offColorRed = Color.black;
    public Color onColorGreen = Color.green;
    public Color offColorGreen = Color.black;

    // 💡 발광(Emission) 강도 조절 (Unity Inspector에서 조절 가능)
    public float emissionIntensity = 1.5f;

    // Material의 Emission 속성 이름 (Unity의 표준 쉐이더 기준)
    private const string EmissionColorProperty = "_EmissionColor";


    void Start()
    {
        // 런타임에 Material 인스턴스를 얻어와야 실시간으로 색상 변경이 가능합니다.
        // 이를 통해 씬의 다른 오브젝트에 영향을 주지 않고 이 램프만 제어합니다.
        if (redLampRenderer != null)
            redLampRenderer.material.EnableKeyword("_EMISSION");
        if (greenLampRenderer != null)
            greenLampRenderer.material.EnableKeyword("_EMISSION");

        // 초기 상태 설정
        UpdateLamps();
    }

    void Update()
    {
        // 매 프레임 센서 상태를 확인하고 램프를 업데이트합니다.
        UpdateLamps();
    }

    private void UpdateLamps()
    {
        // 1. 그린 램프 제어 (전진 센서)
        if (forwardSensor != null)
        {
            SetLampState(greenLampRenderer, forwardSensor.HasDetected(), onColorGreen, offColorGreen);
        }

        // 2. 레드 램프 제어 (후진 센서)
        if (backwardSensor != null)
        {
            SetLampState(redLampRenderer, backwardSensor.HasDetected(), onColorRed, offColorRed);
        }

        // (참고: Yellow 램프는 필요시 여기에 추가 로직 구현)
    }

    /// <summary>
    /// 지정된 램프의 켜짐/꺼짐 상태에 따라 Material의 Emission 색상을 변경합니다.
    /// </summary>
    /// <param name="renderer">제어할 램프 오브젝트의 Renderer</param>
    /// <param name="isDetected">센서 감지 여부 (true: 켜짐, false: 꺼짐)</param>
    /// <param name="onColor">켜짐 상태의 기본 색상</param>
    /// <param name="offColor">꺼짐 상태의 기본 색상</param>
    private void SetLampState(Renderer renderer, bool isDetected, Color onColor, Color offColor)
    {
        if (renderer == null) return;

        Color targetColor;

        if (isDetected)
        {
            // 감지되면 (켜짐): 색상 * 강도 (HDR 효과를 위해)
            targetColor = onColor * emissionIntensity;
        }
        else
        {
            // 감지 안되면 (꺼짐): 어두운 색상 (검은색)
            targetColor = offColor;
        }

        // Material의 Emission 색상을 변경합니다.
        renderer.material.SetColor(EmissionColorProperty, targetColor);
        renderer.material.SetColor(EmissionColorProperty, targetColor);
    }
}
