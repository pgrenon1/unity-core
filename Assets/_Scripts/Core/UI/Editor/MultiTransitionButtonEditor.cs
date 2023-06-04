using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MultiTransitionButton))]
public class MultiTransitionButtonEditor : UnityEditor.UI.ButtonEditor
{
    private SerializedProperty _onSelect;
    private SerializedProperty _transitionTargets;
    private SerializedProperty _holdToClick;
    private SerializedProperty _requiredHoldTime;
    private SerializedProperty _fillImage;
    private SerializedProperty _onLongClick;
    
    protected override void OnEnable()
    {
        base.OnEnable();

        _transitionTargets = serializedObject.FindProperty("transitionTargets");
        _holdToClick = serializedObject.FindProperty("holdToClick");
        _requiredHoldTime = serializedObject.FindProperty("requiredHoldTime");
        _fillImage = serializedObject.FindProperty("fillImage");
        _onLongClick = serializedObject.FindProperty("onLongClick");
        _onSelect = serializedObject.FindProperty("onSelect");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        EditorGUILayout.PropertyField(_holdToClick);
        EditorGUILayout.PropertyField(_requiredHoldTime);
        EditorGUILayout.PropertyField(_fillImage);
        EditorGUILayout.PropertyField(_onLongClick);

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_transitionTargets, includeChildren: true);

        serializedObject.ApplyModifiedProperties();
    }
}
