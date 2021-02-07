//----------------------------------------------------------------------
//
//              Maggic @  2019/12/13 17:00:31
//
//---------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;

/// <summary>
/// 
/// </summary>
public static class MenuExpand
{
    [MenuItem("Tools/PlayerPrefs/DeleteAll")]
    public static void PlayerPrefsDeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("Tools/Screen/CaptureScreenshot _F5")]
    public static void OnCaptureScreenshot()
    {
        string fileName = string.Format("Screenshot_{0}_{1}.png", Screen.width + "X" + Screen.height,
            System.DateTime.Now.ToString("yyyyMMddHHmmss"));
        ScreenCapture.CaptureScreenshot(fileName, 0);
    }

    [MenuItem("Tools/Game/ApplicationPause _F2")]
    public static void OnSetApplicationPause()
    {
        EditorApplication.isPaused = !EditorApplication.isPaused;
    }

    [MenuItem("Tools/Check/Finding Missing in the Scene")]
    public static void OnCheckMissingInTheScene()
    {
        Debug.Log("<color=blue>Finding Missing in the Scene-------------------------->start</color>");
        GameObject[] objs = GameObject.FindObjectsOfType<GameObject>();
        List<GameObject> selectObjs = new List<GameObject>();
        for (int i = 0; i < objs.Length; i++)
        {
            MonoBehaviour[] monos = objs[i].GetComponents<MonoBehaviour>();
            for (int m = 0; m < monos.Length; m++)
            {
                if (monos[m] == null)
                {
                    selectObjs.Add(objs[i]);
                    Debug.Log("<color=blue>" + objs[i].name + "</color>");
                }
            }
        }

        Debug.Log("<color=blue>Finding Missing in the Scene-------------------------->end</color>");
        Selection.objects = selectObjs.ToArray();
    }

    #region Replace Custom Text

    [MenuItem("Tools/UI/Replace Custom Text")]
    public static void ReplaceCustomText()
    {
        GameObject[] objs = Selection.gameObjects;
        ReplaceCustomText(objs);
    }

    public static void ReplaceCustomText(GameObject[] objs)
    {
        MonoBehaviour[] monos = Resources.FindObjectsOfTypeAll<MonoBehaviour>();

        List<UnityEngine.UI.Text> allTexts = new List<UnityEngine.UI.Text>();
        for (int i = 0; i < objs.Length; i++)
        {
            GameObject obj = objs[i];
            UnityEngine.UI.Text[] texts = obj.GetComponentsInChildren<UnityEngine.UI.Text>(true);
            allTexts.AddRange(texts);
        }

        Dictionary<int, FieldData> fieldDatas = new Dictionary<int, FieldData>();
        for (int m = 0; m < monos.Length; m++)
        {
            System.Type type = monos[m].GetType();
            System.Reflection.FieldInfo[] fieldInfos = type.GetFields(System.Reflection.BindingFlags.Public
                                                                      | System.Reflection.BindingFlags.Instance |
                                                                      System.Reflection.BindingFlags.NonPublic);
            for (int f = 0; f < fieldInfos.Length; f++)
            {
                System.Type fieldType = fieldInfos[f].FieldType;
                object fieldValue = fieldInfos[f].GetValue(monos[m]);
                if (fieldValue != null)
                {
                    UnityEngine.UI.Text text = fieldValue as UnityEngine.UI.Text;
                    if (text != null)
                    {
                        if (allTexts.Contains(text))
                        {
                            fieldDatas.Add(text.GetInstanceID(), new FieldData(monos[m], fieldInfos[f]));
                        }
                    }
                }
            }
        }

        for (int t = 0; t < allTexts.Count; t++)
        {
            UnityEngine.UI.Text text = allTexts[t] as UnityEngine.UI.Text;
            GameObject obj = text.gameObject;
            string context = text.text;
            Font font = text.font;
            FontStyle fontStyle = text.fontStyle;
            int fontSize = text.fontSize;
            bool richText = text.supportRichText;
            TextAnchor alignment = text.alignment;
            bool alignByGeometry = text.alignByGeometry;
            HorizontalWrapMode horizontalOverflow = text.horizontalOverflow;
            VerticalWrapMode verticalOverflow = text.verticalOverflow;
            bool resizeTextForBestFit = text.resizeTextForBestFit;
            Color color = text.color;
            Material material = text.material;
            bool raycastTarget = text.raycastTarget;

            FieldData fieldData = null;
            fieldDatas.TryGetValue(text.GetInstanceID(), out fieldData);
            Undo.DestroyObjectImmediate(text);


            CustomText customText = Undo.AddComponent<CustomText>(obj);
            customText.text = context;
            customText.font = font;
            customText.fontStyle = fontStyle;
            customText.fontSize = fontSize;
            customText.supportRichText = richText;
            customText.alignment = alignment;
            customText.alignByGeometry = alignByGeometry;
            customText.horizontalOverflow = horizontalOverflow;
            customText.verticalOverflow = verticalOverflow;
            customText.resizeTextForBestFit = resizeTextForBestFit;
            customText.color = color;
            customText.material = material;
            customText.raycastTarget = raycastTarget;
            if (fieldData != null)
            {
                Undo.RecordObject(fieldData.m_mono.gameObject, "RangeCustomText");
                fieldData.m_fieldInfo.SetValue(fieldData.m_mono, customText);
            }
        }
    }

    private class FieldData
    {
        public MonoBehaviour m_mono;
        public System.Reflection.FieldInfo m_fieldInfo;

        public FieldData(MonoBehaviour mono, System.Reflection.FieldInfo fieldInfo)
        {
            m_mono = mono;
            m_fieldInfo = fieldInfo;
        }
    }

    #endregion

    [MenuItem("GameObject/UI/Text Custom")]
    public static void CreateCustomText()
    {
        DefaultControls.Resources _controlsResources = new DefaultControls.Resources();
        _controlsResources.standard = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
        _controlsResources.background = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
        _controlsResources.inputField =
            AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/InputFieldBackground.psd");
        _controlsResources.knob = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Knob.psd");
        _controlsResources.checkmark = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Checkmark.psd");
        _controlsResources.dropdown = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/DropdownArrow.psd");
        _controlsResources.mask = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");

        GameObject _obj = DefaultControls.CreateText(_controlsResources);
        ReplaceCustomText(new GameObject[] {_obj});
        GameObjectUtility.SetParentAndAlign(_obj, Selection.activeGameObject);
        Undo.RegisterCreatedObjectUndo(_obj, "Create " + _obj.name);
        if (Selection.activeGameObject != null)
        {
            Undo.SetTransformParent(_obj.transform, Selection.activeGameObject.transform, "Parent " + _obj.name);
        }
    }

    [MenuItem("Tools/UI/Check Text IsChinese")]
    public static void CheckTextIsChinese()
    {
        Debug.Log("<color=blue>Check Text IsChinese-------------------------->start</color>");
        GameObject[] selectGameObjects = Selection.gameObjects;
        Regex reg = new Regex(@"[\u4e00-\u9fa5]"); //正则表达式
        for (int i = 0; i < selectGameObjects.Length; i++)
        {
            GameObject select = selectGameObjects[i];
            Text[] texts = select.GetComponentsInChildren<Text>(true);
            for (int t = 0; t < texts.Length; t++)
            {
                string info = texts[t].text;

                if (reg.IsMatch(info))
                {
                    Debug.Log("<color=blue>" + texts[t].gameObject.name + "</color>", texts[t].gameObject);
                }
            }
        }

        Debug.Log("<color=blue>Check Text IsChinese-------------------------->end</color>");
    }

    #region Get Class

    [MenuItem("Tools/Check/Get Scene Class")]
    public static void GetSceneClass()
    {
        SceneAsset[] activeGOs = Selection.GetFiltered<SceneAsset>(SelectionMode.DeepAssets);
        string info = string.Empty;
        for (int i = 0; i < activeGOs.Length; i++)
        {
            string path = AssetDatabase.GetAssetPath(activeGOs[i]);
            string[] deps = AssetDatabase.GetDependencies(path);
            Debug.Log(path);
            for (int d = 0; d < deps.Length; d++)
            {
                string ext = System.IO.Path.GetExtension(deps[d]);
                if (ext == ".cs" && deps[d].Contains("Assets/"))
                {
                    Debug.Log(deps[d]);
                    string[] infos = System.IO.File.ReadAllLines(Application.dataPath + "/../" + deps[d]);
                    for (int s = 0; s < infos.Length; s++)
                    {
                        if (infos[s].Contains("using "))
                        {
                            continue;
                        }

                        if (infos[s].Contains("[") && infos[s].Contains("]"))
                        {
                            continue;
                        }

                        if (infos[s].Contains("//"))
                        {
                            continue;
                        }

                        if (string.IsNullOrEmpty(infos[s]) == true)
                        {
                            continue;
                        }
                        //if ((infos[s].Contains("public") || infos[s].Contains("private")|| infos[s].Contains("protected"))
                        //    && infos[s].Contains("(")
                        //    && infos[s].Contains(")"))
                        //    {
                        //    //is start
                        //}

                        info += infos[s] + "\n";
                    }
                }
            }
        }

        GUIUtility.systemCopyBuffer = info;
        Debug.Log(info);
    }

    #endregion
}