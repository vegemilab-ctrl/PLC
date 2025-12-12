using UnityEngine;

public class SensorTest : MonoBehaviour
{
    public void OnDetectedSensor(bool hasDetected, string name)
    {
        Debug.Log($"{name}::{(hasDetected ? "감지했다" : "감지못했다")}");
    }
}