using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : PushdownState
{
    public MainMenu GameMenu { get; private set; }

    private MenuPanel _menuPanel;
    public MenuPanel MenuPanel
    {
        get
        {
            return _menuPanel;
        }
        set
        {
            _menuPanel = value;
            _menuPanel.MainMenu = GameMenu;
        }
    }

    public MenuState(MainMenu gameMenu, PushdownStateMachine stateMachine) 
        : base(stateMachine)
    {
        GameMenu = gameMenu;
    }

    public override void EndEnter()
    {
        base.EndEnter();

        MenuPanel.WaitAndSetupNavigation();
    }
}