using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(CustomText), true)]
public class CustomTextInspector : UnityEditor.UI.TextEditor
{
    SerializedProperty m_languageId;
    SerializedProperty m_language;

    protected override void OnEnable()
    {
        base.OnEnable();
        m_languageId = serializedObject.FindProperty("m_languageId");
        m_language = serializedObject.FindProperty("m_language");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(m_languageId);
        EditorGUILayout.PropertyField(m_language);
        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            for (int i = 0; i < targets.Length; i++)
            {
                CustomText text = targets[i] as CustomText;
                if (text != null)
                {
                    text.SetContent();
                }
            }
        }
    }
}

