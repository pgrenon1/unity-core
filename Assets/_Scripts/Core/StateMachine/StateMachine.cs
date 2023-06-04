using UnityEngine;

public class StateMachine
{
    public virtual State CurrentState { get; protected set; }
    public bool DebugTransition { get { return GameManager.Instance.debugStateTransitions; } }

    public virtual void ChangeState(State newState)
    {
        if (CurrentState != null)
            CurrentState.Exit();

        if (GameManager.Instance.debugStateMachine) Debug.Log($"{CurrentState} -> <color=green>{newState}</color>");

        CurrentState = newState;
        CurrentState.Enter();
    }

    public void Update()
    {
        if (CurrentState != null)
            CurrentState.Execute();
    }
}