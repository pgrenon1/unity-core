using System;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : OdinSerializedSingletonBehaviour<CheatManager>
{
    private CheatInputReader _cheatInputReaderCache;
    public CheatInputReader CheatInputReader => GetCachedComponent(ref _cheatInputReaderCache);

    private ScriptableObjectDebugMenu _scriptableObjectDebugMenu;
    public ScriptableObjectDebugMenu ScriptableObjectDebugMenu => GetCachedComponent(ref _scriptableObjectDebugMenu);
    
    public bool DrawGizmos { get; private set; }

    private void Start()
    {
        CheatInputReader.EnableCheatInputs();
        
        CheatInputReader.TimescaleDownEvent += OnTimescaleDownEvent;
        CheatInputReader.TimescaleUpEvent += OnTimescaleUpEvent;
        CheatInputReader.ToggleGizmosEvent += OnToggleGizmosEvent;
        CheatInputReader.ToggleDebugMenu += OnToggleDebugMenu;
    }

    private void OnToggleDebugMenu()
    {
        ScriptableObjectDebugMenu.Toggle();
    }

    private void OnToggleGizmosEvent()
    {
        DrawGizmos = !DrawGizmos;
    }

    private void OnTimescaleDownEvent()
    {
        GameManager.Instance.ChangeTimeScaleIndex(GameManager.Instance.TimescaleIndex - 1);
    }
    
    private void OnTimescaleUpEvent()
    {
        GameManager.Instance.ChangeTimeScaleIndex(GameManager.Instance.TimescaleIndex + 1);
    }
}
