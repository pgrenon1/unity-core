using Sirenix.OdinInspector;
using UnityEngine;

public class FibonacciSphereData
{
    [MinMaxSlider(0f, 1f, true)]
    public Vector2 minMaxAngle;
    [Range(-180f, 180f)]
    public float angleStart;
    [Range(0f, 360f)]
    public float angleRange;

    public FibonacciSphereData(float angleRange, float angleStart, Vector2 minMaxAngle)
    {
        this.angleRange = angleRange;
        this.angleStart = angleStart;
        this.minMaxAngle = minMaxAngle;
    }
}