using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using ImGuiNET;
using UnityEngine;

public class ScriptableObjectDebugMenu : DebugMenu
{
    public List<ScriptableObject> debuggableScriptableObjects = new List<ScriptableObject>();
    
    protected override void DrawGUI()
    {
        base.DrawGUI();

        if (ImGui.BeginTabBar("#tabs"))
        {
            foreach (ScriptableObject debuggableScriptableObject in debuggableScriptableObjects)
            {
                if (ImGui.BeginTabItem(debuggableScriptableObject.name))
                {
                    DrawIMGUIForObject(debuggableScriptableObject);
                    ImGui.EndTabItem();
                }
            }
        }
    }

    private void DrawIMGUIForObject(object inspectedObject, float indent = 0f)
    {
        if (inspectedObject == null)
            return;

        FieldInfo[] fields = inspectedObject.GetType().GetFields();
        for (int j = 0; j < fields.Length; j++)
        {
            FieldInfo field = fields[j];

            object[] debugAttributes = field.GetCustomAttributes(typeof(DebugFieldAttribute), false);
            if (debugAttributes.Length == 0)
                continue;
            
            Type fieldType = field.FieldType;
            object fieldValue = field.GetValue(inspectedObject);
            
            if (fieldValue == null)
                continue;
            
            if (!fieldType.IsValueType)
                continue;
            
            ImGui.Columns(2);
            string fieldName = Utils.NicifyVariable(field.Name);
            ImGui.Selectable(fieldName);
            ImGui.NextColumn();
            if (fieldType == typeof(float))
            {
                float floatValue = (float)fieldValue;
                ImGui.DragFloat("", ref floatValue, 0.01f);
                if (floatValue != (float)fieldValue)
                    field.SetValue(inspectedObject, (float) floatValue);
            }
            else if (fieldType == typeof(int))
            {
                int intValue = (int)fieldValue;
                ImGui.DragInt("", ref intValue, 0.01f);
                if (intValue != (float)fieldValue)
                    field.SetValue(inspectedObject, (int) intValue);
            }
            ImGui.NextColumn();
        }
        
        //
        // using (new GUILayout.VerticalScope())
        // {
        //     for (int j=0; j<fields.Length; j++)
        //     {
        //         FieldInfo field = fields[j];
        //         var fieldType = field.FieldType;
        //         object fieldValue = field.GetValue(inspectedObject);
        //
        //         if (fieldValue == null)
        //             continue;
        //
        //         if (!fieldType.IsValueType)
        //             continue;
        //
        //         using (new GUILayout.HorizontalScope(GUI.skin.box))
        //         {
        //             GUILayout.Space(indent);
        //             
        //             GUILayout.Label(Utils.NicifyVariable(field.Name), GUILayout.ExpandWidth(true));
        //
        //             if (fieldType == typeof(float))
        //             {
        //                 float newValue = HorizontalSlider((float) fieldValue, 0f, 100f, GUILayout.MaxWidth(GetWindowRect().width / 4f));
        //                 field.SetValue(inspectedObject, newValue);
        //                 GUILayout.Box(fieldValue.ToString(), GUILayout.MaxWidth(50f));
        //             }
        //             else if (fieldType == typeof(int))
        //             {
        //                 int newValue = IntField((int) fieldValue, GUILayout.MaxWidth(100f));
        //                 field.SetValue(inspectedObject, Mathf.RoundToInt(newValue));
        //             }
        //             else if (fieldType == typeof(string))
        //             {
        //                 string newValue = TextField((string) fieldValue, 100, null, GUILayout.MaxWidth(100f));
        //                 field.SetValue(inspectedObject, newValue);
        //             }
        //             else
        //             {
        //                 DrawIMGUIForObject(fieldValue, 50f);
        //             }
        //         }
        //     }
        // }
    }

    private string TextField(string value, int maxLength, Func<string, bool> regex, params GUILayoutOption[] options)
    {
        string newValue = GUILayout.TextField(value, maxLength, options);
        
        if (regex != null)
            newValue = regex(newValue) ? newValue : value;
        
        return newValue;
    }

    private int IntField(int value, params GUILayoutOption[] options)
    {
        string newValueString = TextField(value.ToString(), 10, (t) => Regex.IsMatch(t, "^[0-9]+$"), options);

        int newValue = int.Parse(newValueString);
        
        return newValue;
    }

    private float HorizontalSlider(float value, float min, float max, params GUILayoutOption[] options)
    {
        GUIStyle horizontalSliderStyle = new GUIStyle(GUI.skin.horizontalSlider);
        horizontalSliderStyle.alignment = TextAnchor.MiddleCenter;
            
        float newValue = GUILayout.HorizontalSlider(value, min, max, horizontalSliderStyle, GUI.skin.horizontalSliderThumb, options);
        
        return newValue;
    }
}

public class DebugFieldAttribute : Attribute
{
    
}