using UnityEngine;
using System.Collections;
using UnityEditor;
using UGUI;
namespace UGUIEditor
{
    [CustomEditor(typeof(UTweenPosition))]
    public class UTweenPositionInspector : UTweenerInspector
    {
        UTweenPosition tagetClass;
        private void OnEnable()
        {
            tagetClass = (UTweenPosition)target;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            tagetClass.from = EditorGUILayout.Vector3Field("from", tagetClass.from);
            tagetClass.to = EditorGUILayout.Vector3Field("to", tagetClass.to);
            tagetClass.worldSpace = EditorGUILayout.Toggle("worldSpace", tagetClass.worldSpace);
        }
    }
}
