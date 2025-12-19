using UnityEngine;
using UnityEngine.UI;

public class PulseDisplayer : MonoBehaviour
{
    public Text pulseText;

    public void OnChangedPulse(int pulse)
    {
        pulseText.text = pulse.ToString();
    }
}