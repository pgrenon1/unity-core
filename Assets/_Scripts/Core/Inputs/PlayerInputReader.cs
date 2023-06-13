using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;

public class PlayerInputReader : InputReader, PlayerActions.IMainActions
{
    public event UnityAction PauseEvent = delegate { };
    
    public event UnityAction<float> MovementHorizontalEvent = delegate(float value) {  };
    public event UnityAction<float> MovementVerticalEvent = delegate(float value) {  };
    public event UnityAction<Vector2> LookEvent = delegate(Vector2 value) {  };
    public event UnityAction JumpEvent = delegate { };
    public event UnityAction JumpCancelledEvent = delegate { };

    protected override void SetupPlayerActions()
    {
        base.SetupPlayerActions();
        
        PlayerActions.Main.SetCallbacks(this);
    }

    public void ToggleMainGameInputs(bool enable)
    {
        if (enable)
            PlayerActions.Main.Enable();
        else
            PlayerActions.Main.Disable();
    }
    
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            PauseEvent.Invoke();
    }
    
    private bool IsDeviceMouse(InputAction.CallbackContext context) => context.control.device.name == "Mouse";
    
    public void OnHorizontal(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            MovementHorizontalEvent(context.ReadValue<float>());
    }

    public void OnVertical(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            MovementVerticalEvent(context.ReadValue<float>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            JumpEvent();

        if (context.phase == InputActionPhase.Canceled)
            JumpCancelledEvent();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            LookEvent(context.ReadValue<Vector2>());
    }
}