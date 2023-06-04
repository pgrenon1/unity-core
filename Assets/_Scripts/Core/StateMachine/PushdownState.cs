using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushdownState : State
{
    public PushdownStateMachine PushdownStateMachine { get { return StateMachine as PushdownStateMachine; } }

    public PushdownState(PushdownStateMachine stateMachine) : base(stateMachine)
    {
        StateMachine = stateMachine;
    }
}