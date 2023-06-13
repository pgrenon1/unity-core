using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[InitializeOnLoad]
public static class HeaderExtender
{
    private static string _searchString;
    
    static HeaderExtender()
    {
        Editor.finishedDefaultHeaderGUI += DrawHeaderExtension;
    }

    private static void DrawHeaderExtension(Editor editor)
    {
        if (editor.target is not GameObject)
            return;
        
        GUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Expand All", GUILayout.ExpandWidth(false)))
            {
                ExpandAll();
            }

            if (GUILayout.Button("Collapse All", GUILayout.ExpandWidth(false)))
            {
                CollapseAll();
            }
            
            GUILayout.BeginHorizontal(GUI.skin.FindStyle("Toolbar"));
            string newSearchString = GUILayout.TextField(_searchString, GUI.skin.FindStyle("ToolbarSeachTextField"), GUILayout.ExpandWidth(true));
            
            if (GUILayout.Button("", GUI.skin.FindStyle("ToolbarSeachCancelButton")))
            {
                // Remove focus if cleared
                newSearchString = "";
                GUI.FocusControl(null);
            }
            
            if (_searchString != newSearchString)
            {
                _searchString = newSearchString;
                
                if (_searchString == "")
                {
                    ExpandAll();
                }
                else
                {
                    var activeEditorTracker = ActiveEditorTracker.sharedTracker;
                    for (var i = 0; i < activeEditorTracker.activeEditors.Length; i++)
                    {
                        var activeEditor = activeEditorTracker.activeEditors[i];
                        bool expanded;
                        if (!FuzzySearch.Contains(_searchString, activeEditor.target.GetType().Name))
                        {
                            expanded = false;
                        }
                        else
                        {
                            expanded = true;
                        }
                        
                        activeEditorTracker.SetVisible(i, expanded ? 1 : 0);
                    }
                }
            }
            
            GUILayout.EndHorizontal();
        }
        GUILayout.EndHorizontal();
    }
    
    private static void CollapseAll()
    {
        SetAllInspectorsExpanded(false);
    }

    private static void ExpandAll()
    {
        SetAllInspectorsExpanded(true);
    }

    private static void SetAllInspectorsExpanded(bool expanded)
    {
        var activeEditorTracker = ActiveEditorTracker.sharedTracker;

        for (var i = 0; i < activeEditorTracker.activeEditors.Length; i++)
        {
            activeEditorTracker.SetVisible(i, expanded ? 1 : 0);
        }
    }
}