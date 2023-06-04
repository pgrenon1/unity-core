using System;
using TMPro;
#if UNITY_EDITOR
#endif
using UnityEngine;

[CreateAssetMenu]
public class InputActionIcons : OdinSerializedScriptableObject
{
    public string gamepadName;
    public TMP_SpriteAsset TMPSpriteAsset;
    public Sprite buttonSouth;
    public Sprite buttonNorth;
    public Sprite buttonEast;
    public Sprite buttonWest;
    public Sprite startButton;
    public Sprite selectButton;
    public Sprite leftTrigger;
    public Sprite rightTrigger;
    public Sprite leftShoulder;
    public Sprite rightShoulder;
    public Sprite dpad;
    public Sprite dpadUp;
    public Sprite dpadDown;
    public Sprite dpadLeft;
    public Sprite dpadRight;
    public Sprite leftStick;
    public Sprite rightStick;
    public Sprite leftStickPress;
    public Sprite rightStickPress;

    public Sprite GetSprite(string controlPath)
    {
        // From the input system, we get the path of the control on device. So we can just
        // map from that to the sprites we have for gamepads.
        switch (controlPath)
        {
            case "buttonSouth": return buttonSouth;
            case "buttonNorth": return buttonNorth;
            case "buttonEast": return buttonEast;
            case "buttonWest": return buttonWest;
            case "start": return startButton;
            case "select": return selectButton;
            case "leftTrigger": return leftTrigger;
            case "rightTrigger": return rightTrigger;
            case "leftShoulder": return leftShoulder;
            case "rightShoulder": return rightShoulder;
            case "dpad": return dpad;
            case "dpad/up": return dpadUp;
            case "dpad/down": return dpadDown;
            case "dpad/left": return dpadLeft;
            case "dpad/right": return dpadRight;
            case "leftStick": return leftStick;
            case "rightStick": return rightStick;
            case "leftStickPress": return leftStickPress;
            case "rightStickPress": return rightStickPress;
        }
        return null;
    }

    public string GetGenericName(Sprite sprite)
    {
        if (sprite.name == buttonSouth.name)
            return "buttonSouth";
        else if (sprite.name == buttonNorth.name)
            return "buttonNorth";
        else if (sprite.name == buttonWest.name)
            return "buttonWest";
        else if (sprite.name == buttonEast.name)
            return "buttonEast";
        else if (sprite.name == startButton.name)
            return "start";
        else if (sprite.name == selectButton.name)
            return "select";
        else if (sprite.name == leftTrigger.name)
            return "leftTrigger";
        else if (sprite.name == rightTrigger.name)
            return "rightTrigger";
        else if (sprite.name == leftShoulder.name)
            return "leftShoulder";
        else if (sprite.name == rightShoulder.name)
            return "rightShoulder";
        else if (sprite.name == dpad.name)
            return "dpad";
        else if (sprite.name == dpadUp.name)
            return "dpad/up";
        else if (sprite.name == dpadDown.name)
            return "dpad/down";
        else if (sprite.name == dpadLeft.name)
            return "dpad/left";
        else if (sprite.name == dpadRight.name)
            return "dpad/right";
        else if (sprite.name == leftStick.name)
            return "leftStick";
        else if (sprite.name == rightStick.name)
            return "rightStick";
        else if (sprite.name == leftStickPress.name)
            return "leftStickPress";
        else if (sprite.name == rightStickPress.name)
            return "rightStickPress";
        return "";
    }
}