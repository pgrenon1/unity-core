using System;
using UnityEngine.InputSystem;

[Serializable]
public class GameInputAction
{
    public string inputActionFullPath;

    private InputAction _inputActionCache;
    public InputAction GetInputAction(PlayerInput playerInput)
    {
        if (_inputActionCache == null)
            _inputActionCache = playerInput.actions.FindAction(inputActionFullPath);

        return _inputActionCache;
    }
}
