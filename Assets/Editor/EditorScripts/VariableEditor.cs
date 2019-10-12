using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Variable))]
public class VariableEditor : PropertyDrawer
{
    public override void OnGUI(Rect _rect, SerializedProperty _property, GUIContent _label)
    {
        using (new EditorGUI.PropertyScope(_rect, _label, _property))
        {
            EditorGUIUtility.labelWidth = 60;
            _rect.height = EditorGUIUtility.singleLineHeight;

            Rect nameRect = new Rect(_rect)
            {
                width = 150
            };

            Rect typeRect = new Rect(nameRect)
            {
                x = nameRect.x + 160,
                width = 85,
            };

            Rect componentRect = new Rect(typeRect)
            {
                x = typeRect.x + 100,
                width = _rect.width - 260
            };

            var name = _property.FindPropertyRelative("name");
            var type = _property.FindPropertyRelative("type");
            name.stringValue = EditorGUI.TextField(nameRect, name.stringValue);

            VariableType enumType = (VariableType)type.enumValueIndex;
            enumType = (VariableType)EditorGUI.EnumPopup(typeRect, enumType);
            type.enumValueIndex = (int)enumType;

            if (enumType == VariableType.Component)
            {
                var component = _property.FindPropertyRelative("component");
                component.objectReferenceValue = EditorGUI.ObjectField(componentRect, component.objectReferenceValue, typeof(Component), true);
            }
            else {
                var gameObject = _property.FindPropertyRelative("gameObject");
                gameObject.objectReferenceValue = EditorGUI.ObjectField(componentRect, gameObject.objectReferenceValue, typeof(GameObject), true);
            }
        }
    }
}
