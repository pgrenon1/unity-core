public class SplashScreenState : MenuState
{
    private SplashScreen _splashScreen;
    public SplashScreen SplashScreen
    {
        get
        {
            if (_splashScreen == null)
                _splashScreen = MenuPanel as SplashScreen;

            return _splashScreen;
        }
    }

    public SplashScreenState(MainMenu mainMenu, PushdownStateMachine stateMachine)
    : base(mainMenu, stateMachine)
    {
        MenuPanel = mainMenu.GetComponentInChildren<SplashScreen>();
    }

    public override void Enter()
    {
        base.Enter();

        SplashScreen.Show(EndEnter);
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void Exit()
    {
        base.Exit();

        SplashScreen.Hide(EndExit);
    }
}
