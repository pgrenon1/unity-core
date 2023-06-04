using System.Collections.Generic;

public class PushdownStateMachine : StateMachine
{
    public override State CurrentState
    {
        get
        {
            if (StateStack.Count > 0)
                return StateStack.Peek();
            else
                return null;
        }
        protected set
        {
            StateStack.Push(value as PushdownState);
        }
    }

    public Stack<PushdownState> StateStack { get; private set; } = new Stack<PushdownState>();

    public void ChangeStateBack()
    {
        if (CurrentState != null && StateStack.Count > 1)
        {
            var stateToExit = StateStack.Pop();
            stateToExit.Exit();
            
            CurrentState.Enter();
        }
    }
}
