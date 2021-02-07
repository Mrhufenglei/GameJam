using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LabelAttribute))]
internal sealed class LabelAttributeEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        LabelAttribute range = (LabelAttribute) attribute;
        switch (property.propertyType)
        {
            case SerializedPropertyType.Generic:
                EditorGUI.LabelField(position, label.text, property.ToString());
                break;
            case SerializedPropertyType.Integer:
                EditorGUI.LabelField(position, label.text, property.intValue.ToString());
                break;
            case SerializedPropertyType.Boolean:
                EditorGUI.LabelField(position, label.text, property.boolValue.ToString());
                break;
            case SerializedPropertyType.Float:
                EditorGUI.LabelField(position, label.text, property.floatValue.ToString());
                break;
            case SerializedPropertyType.String:
                EditorGUI.LabelField(position, label.text, property.stringValue.ToString());
                break;
            case SerializedPropertyType.Color:
                EditorGUI.LabelField(position, label.text, property.colorValue.ToString());
                break;
            case SerializedPropertyType.ObjectReference:
                EditorGUI.LabelField(position, label.text,
                    property.objectReferenceValue != null ? property.objectReferenceValue.ToString() : "Null");
                break;
            case SerializedPropertyType.LayerMask:
                EditorGUI.LabelField(position, label.text, property.enumNames[property.enumValueIndex].ToString());
                break;
            case SerializedPropertyType.Enum:
                EditorGUI.LabelField(position, label.text, property.enumNames[property.enumValueIndex].ToString());
                break;
            case SerializedPropertyType.Vector2:
                EditorGUI.LabelField(position, label.text, property.vector2Value.ToString());
                break;
            case SerializedPropertyType.Vector3:
                EditorGUI.LabelField(position, label.text, property.vector3Value.ToString());
                break;
            case SerializedPropertyType.Vector4:
                EditorGUI.LabelField(position, label.text, property.vector4Value.ToString());
                break;
            case SerializedPropertyType.Rect:
                EditorGUI.LabelField(position, label.text, property.rectValue.ToString());
                break;
            case SerializedPropertyType.ArraySize:
                EditorGUI.LabelField(position, label.text, property.arraySize.ToString());
                break;
            case SerializedPropertyType.Character:
                break;
            case SerializedPropertyType.AnimationCurve:
                EditorGUI.LabelField(position, label.text, property.animationCurveValue.ToString());
                break;
            case SerializedPropertyType.Bounds:
                EditorGUI.LabelField(position, label.text, property.boundsValue.ToString());
                break;
            case SerializedPropertyType.Gradient:
                EditorGUI.LabelField(position, label.text, property.ToString());
                break;
            case SerializedPropertyType.Quaternion:
                EditorGUI.LabelField(position, label.text, property.quaternionValue.ToString());
                break;
            case SerializedPropertyType.ExposedReference:
                EditorGUI.LabelField(position, label.text, property.exposedReferenceValue.ToString());
                break;
            case SerializedPropertyType.FixedBufferSize:
                EditorGUI.LabelField(position, label.text, property.fixedBufferSize.ToString());
                break;
            case SerializedPropertyType.Vector2Int:
                EditorGUI.LabelField(position, label.text, property.vector2IntValue.ToString());
                break;
            case SerializedPropertyType.Vector3Int:
                EditorGUI.LabelField(position, label.text, property.vector3IntValue.ToString());
                break;
            case SerializedPropertyType.RectInt:
                EditorGUI.LabelField(position, label.text, property.rectIntValue.ToString());
                break;
            case SerializedPropertyType.BoundsInt:
                EditorGUI.LabelField(position, label.text, property.boundsValue.ToString());
                break;
            default:
                break;
        }
    }
}