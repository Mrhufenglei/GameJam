using UGUI;
using UnityEditor;
namespace UGUIEditor
{
    [CustomEditor(typeof(UAtlasData))]
    public class UAtalsDataInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginDisabledGroup(true);
            {
                base.OnInspectorGUI();
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}
