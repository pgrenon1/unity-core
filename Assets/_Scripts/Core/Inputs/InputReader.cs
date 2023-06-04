using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputReader : BaseBehaviour, PlayerActions.IMenuActions
{
    // Menu
    public event UnityAction SubmitEvent = delegate { };
    public event UnityAction<Vector2> NavigateEvent = delegate { };
    public event UnityAction CancelEvent = delegate { };
    public event UnityAction PointEvent = delegate { };
    public event UnityAction ClickEvent = delegate { };
    public event UnityAction ScrollWheelEvent = delegate { };
    public event UnityAction RightClickEvent = delegate { };
    public event UnityAction MiddleClickEvent = delegate { };

    public PlayerActions PlayerActions { get; private set; }

    private PlayerInput _playerInputCache;
    public PlayerInput PlayerInput => GetCachedComponent(ref _playerInputCache);
    
    protected virtual void OnEnable()
    {
        if (PlayerActions == null)
        {
            SetupPlayerActions();
        }
    }

    protected virtual void SetupPlayerActions()
    {
        PlayerActions = new PlayerActions();
        PlayerActions.Menu.SetCallbacks(this);
    }

    protected virtual  void OnDisable()
    {
        DisableAllInputs();
    }

    public void EnableMenuInputs(bool disableOthers)
    {
        PlayerActions.Menu.Enable();
    }

    public virtual void DisableAllInputs()
    {
        PlayerActions.Menu.Disable();
    }

    #region Menus Inputs
    public void OnNavigate(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            NavigateEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            SubmitEvent.Invoke();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            CancelEvent.Invoke();
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            PointEvent.Invoke();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            ClickEvent.Invoke();
    }

    public void OnScrollWheel(InputAction.CallbackContext context)
    {
        // TODO : float/1dvector?
        if (context.phase == InputActionPhase.Performed)
            ScrollWheelEvent.Invoke();
    }

    public void OnMiddleClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            MiddleClickEvent.Invoke();
    }
    
    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            RightClickEvent.Invoke();
    }
    #endregion
}
