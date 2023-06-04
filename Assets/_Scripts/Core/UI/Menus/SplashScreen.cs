using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SplashScreen : MenuPanel
{
    private InputAction _anyButtonInputAction;

    protected override void Start()
    {
        base.Start();

        CreateAnyButtonInput();
        EnableAnyButtonInput();
    }

    private void CreateAnyButtonInput()
    {
        _anyButtonInputAction = new InputAction(binding: "/*/<button>");
    }

    private void EnableAnyButtonInput()
    {
        _anyButtonInputAction.performed += AnyButtonInputAction_Performed;

        _anyButtonInputAction.Enable();
    }

    private void AnyButtonInputAction_Performed(InputAction.CallbackContext context)
    {
        DisableAnyButtonInput();

        GameManager.Instance.ChangeState(MainMenu.MainMenuState);
    }

    private void DisableAnyButtonInput()
    {
        _anyButtonInputAction.performed -= AnyButtonInputAction_Performed;

        _anyButtonInputAction.Disable();
    }
}
