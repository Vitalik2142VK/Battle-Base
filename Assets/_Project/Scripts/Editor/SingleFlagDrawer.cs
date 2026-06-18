using BattleBase;
using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SingleFlagAttribute))]
public class SingleFlagDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Type enumType = fieldInfo.FieldType;

        if (!enumType.IsEnum)
        {
            EditorGUI.LabelField(position, label.text, "SingleEnum can only be used on enums");

            return;
        }

        long value = property.longValue;
        Enum enumValue = (Enum)Enum.ToObject(enumType, value);
        Enum newValue = EditorGUI.EnumPopup(position, label, enumValue);
        property.longValue = Convert.ToInt64(newValue);
    }
}
