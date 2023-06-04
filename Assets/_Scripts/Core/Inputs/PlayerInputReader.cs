using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;

public class PlayerInputReader : InputReader
{
    // Shared
    public event UnityAction PauseEvent = delegate { };
    
    protected override void SetupPlayerActions()
    {
        base.SetupPlayerActions();
        
        // PlayerActions.TwinStick.SetCallbacks(this);
        // PlayerActions.TwinStick.SetCallbacks(this);
        // PlayerActions.SpaceShip.SetCallbacks(this);
        // PlayerActions.SpaceShip.SetCallbacks(this);
    }

    // public override void DisableAllInputs()
    // {
    //     base.DisableAllInputs();
    //     
    //     // PlayerActions.TwinStick.Disable();
    //     // PlayerActions.SpaceShip.Disable();
    // }

    public void EnableMainGameInputs(bool enable)
    {
        if (enable)
            PlayerActions.Main.Enable();
        else
            PlayerActions.Main.Disable();
    }

    #region Shared Inputs
    
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            PauseEvent.Invoke();
    }
    
    private bool IsDeviceMouse(InputAction.CallbackContext context) => context.control.device.name == "Mouse";
    
    #endregion
}