public class MainMenu : BaseBehaviour
{
    public SplashScreenState SplashScreenState { get; private set; }
    public GameMenuState MainMenuState { get; private set; }
    public OptionsState OptionsState { get; private set; }
    // public LobbyState LobbyState { get; private set; }
    public GameState GameState { get; private set; }

    private void Awake()
    {
        SplashScreenState = new SplashScreenState(this, GameManager.Instance.PushdownStateMachine);
        // LobbyState = new LobbyState(this, GameManager.Instance.PushdownStateMachine);
        MainMenuState = new GameMenuState(this, GameManager.Instance.PushdownStateMachine);
        OptionsState = new OptionsState(this, GameManager.Instance.PushdownStateMachine);
        GameState = new GameState(GameManager.Instance.PushdownStateMachine);
    }

    private void Start()
    {
        GameManager.Instance.ChangeState(SplashScreenState);
    }
}
