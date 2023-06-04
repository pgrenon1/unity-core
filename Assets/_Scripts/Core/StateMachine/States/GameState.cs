public class GameState : PushdownState
{
    public GameState(PushdownStateMachine stateMachine) : base(stateMachine)
    {
        StateMachine = stateMachine;
    }

    public override void Enter()
    {
        GameManager.Instance.LoadGameScene(GameManager.Instance.GetNonBootstrapLoadedScenes());
        
        base.Enter();
    }
}
