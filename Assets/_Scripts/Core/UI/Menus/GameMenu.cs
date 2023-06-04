using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MenuPanel
{
    public MultiTransitionButton playButton;

    protected override void Awake()
    {
        base.Awake();
        
        playButton.onClick.AddListener(GoToGame);
    }

    // Called from ui event
    // public void GoToLobby()
    // {
    //     GameManager.Instance.ChangeState(MainMenu.LobbyState);
    // }
    
    // Called from ui event
    public void GoToGame()
    {
        GameManager.Instance.ChangeState(MainMenu.GameState);
    }

    // Called from ui event
    public void GoToOptionsMenu()
    {
        GameManager.Instance.ChangeState(MainMenu.OptionsState);
    }

    // Called from ui event
    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
}
