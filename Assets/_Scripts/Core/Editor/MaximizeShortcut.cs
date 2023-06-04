using UnityEditor;

[InitializeOnLoad]
static class MaximizeShortcut
{
    [MenuItem("Window/Maximize View %#Space")]
    private static void ToggleMaximize()
    {
        EditorWindow.focusedWindow.maximized = !EditorWindow.focusedWindow.maximized;
    }
}