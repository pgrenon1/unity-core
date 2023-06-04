using ImGuiNET;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class DebugMenu : BaseBehaviour
{
    private bool _isVisible = false;

    [Button]
    public void Toggle()
    {
        if (!Application.isPlaying)
            return;
            
        _isVisible = !_isVisible;
        GameManager.Instance.GameIsPaused = _isVisible;

        if (_isVisible)
        {
            ImGuiNET.ImGuiUn.Layout -= OnLayout;
            ImGuiNET.ImGuiUn.Layout += OnLayout;
        }
        else
            ImGuiNET.ImGuiUn.Layout -= OnLayout;
    }
    
    protected virtual void EndGUI()
    {
        ImGui.End();
    }

    protected virtual void DrawGUI()
    { 
        ImGui.ShowDemoWindow();

        ImGui.Begin("Debug");
    }

    private void OnLayout()
    {
        if (!_isVisible)
            return;
        
        
        DrawGUI();
        
        EndGUI();
    }

    private void OnDestroy()
    {
        ImGuiUn.Layout -= OnLayout;
    }
}