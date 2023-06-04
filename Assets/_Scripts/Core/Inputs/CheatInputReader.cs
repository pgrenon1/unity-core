using System.Linq;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CheatInputReader : InputReader, PlayerActions.ICheatActions
{
    // Cheat
    public event UnityAction TimescaleUpEvent = delegate { };
    public event UnityAction TimescaleDownEvent = delegate { };
    public event UnityAction ToggleGizmosEvent = delegate { };
    public event UnityAction ToggleDebugMenu = delegate { };

    protected override void SetupPlayerActions()
    {
        base.SetupPlayerActions();
        
        PlayerActions.Cheat.SetCallbacks(this);
    }

    public override void DisableAllInputs()
    {
        base.DisableAllInputs();
        
        PlayerActions.Cheat.Disable();
    }
    
    public void EnableCheatInputs()
    {
        PlayerActions.Cheat.Enable();
    }
    
    #region Cheat Inputs
    
    public void OnTimescaleUp(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            TimescaleUpEvent.Invoke();
    }

    public void OnTimescaleDown(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            TimescaleDownEvent.Invoke();
    }

    public void OnRespawn(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnToggleGizmos(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            ToggleGizmosEvent.Invoke();
    }

    public void OnDebugMenu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            ToggleDebugMenu.Invoke();
    }

    #endregion
}