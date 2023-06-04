using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu]
public class InputActionIconsMap : OdinSerializedScriptableObject
{
    [InlineEditor]
    public InputActionIcons xbox360;
    [InlineEditor]
    public InputActionIcons xboxOne;
    [InlineEditor]
    public InputActionIcons xboxSeriesX;

    [InlineEditor]
    public InputActionIcons ps3;
    [InlineEditor]
    public InputActionIcons ps4; 
    [InlineEditor]
    public InputActionIcons ps5;
}