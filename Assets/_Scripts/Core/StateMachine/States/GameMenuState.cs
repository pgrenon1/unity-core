using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuState : MenuState
{
    private GameMenu _mainMenu;
    public GameMenu MainMenu
    {
        get
        {
            if (_mainMenu == null)
                _mainMenu = MenuPanel as GameMenu;

            return _mainMenu;
        }
    }

    public GameMenuState(MainMenu gameMenu, PushdownStateMachine stateMachine)
    : base(gameMenu, stateMachine)
    {
        MenuPanel = gameMenu.GetComponentInChildren<GameMenu>();
    }

    public override void Enter()
    {
        base.Enter();

        MainMenu.Show(EndEnter);
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void Exit()
    {
        base.Exit();

        MainMenu.Hide(EndExit);
    }
}
