using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(VariableTable))]
public class VariableTableEditor : Editor
{
    private ReorderableList reorderableList;

    private void OnEnable()
    {
        reorderableList = new ReorderableList(serializedObject, serializedObject.FindProperty("variableTables"), true, true, true, true);

        reorderableList.drawHeaderCallback = (Rect _rect) =>
        {
            GUI.Label(_rect, "VariableTable");
        };

        reorderableList.drawElementCallback = (Rect _rect, int _index, bool _isActive, bool _isFocused) =>
        {
            SerializedProperty item = reorderableList.serializedProperty.GetArrayElementAtIndex(_index);
            EditorGUI.PropertyField(_rect, item);
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
         reorderableList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}