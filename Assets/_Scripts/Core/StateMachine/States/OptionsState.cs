public class OptionsState : MenuState
{
    private OptionsMenu _optionsMenu;
    public OptionsMenu OptionsMenu
    {
        get
        {
            if (_optionsMenu == null)
                _optionsMenu = MenuPanel as OptionsMenu;

            return _optionsMenu;
        }
    }

    public OptionsState(MainMenu gameMenu, PushdownStateMachine stateMachine)
        : base(gameMenu, stateMachine)
    {
        MenuPanel = gameMenu.GetComponentInChildren<OptionsMenu>();
    }

    public override void Enter()
    {
        base.Enter();

        OptionsMenu.Show(EndEnter);
    }

    public override void Exit()
    {
        base.Exit();

        OptionsMenu.Hide(EndExit);
    }
}
