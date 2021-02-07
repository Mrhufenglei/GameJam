using UnityEngine;
using System.Collections;
using UnityEditor;
using UGUI;
namespace UGUIEditor
{
    [CustomEditor(typeof(UTweener))]
    public class UTweenerInspector : Editor
    {
        private bool isFoldout = true;
        private void OnEnable()
        {
        }
        public override void OnInspectorGUI()
        {
            UTweener uTweener = (UTweener)target;
            GUILayout.BeginVertical((GUIStyle)"LargeButton");
            isFoldout = EditorGUILayout.Foldout(isFoldout, "UTweener");
            if (isFoldout)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Style");
                uTweener.style = (UTweener.Style)EditorGUILayout.EnumPopup(uTweener.style);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Curve");
                uTweener.animationCurve = EditorGUILayout.CurveField(uTweener.animationCurve);
                GUILayout.EndHorizontal();

                uTweener.delay = EditorGUILayout.FloatField("Delay", uTweener.delay);
                uTweener.duration = EditorGUILayout.FloatField("Duration", uTweener.duration);
                uTweener.tweenGroup = EditorGUILayout.IntField("Group", uTweener.tweenGroup);
                uTweener.ignoreTimeScale = EditorGUILayout.Toggle("ignoreTimeScale", uTweener.ignoreTimeScale);

            }
            GUILayout.EndVertical();
            GUILayout.Space(5);
            Undo.RecordObject(uTweener, "uTweener");
            this.Repaint();
        }
    }
}
