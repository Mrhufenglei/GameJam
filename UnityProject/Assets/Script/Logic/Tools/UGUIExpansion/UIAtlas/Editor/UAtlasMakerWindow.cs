using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using UnityEditor;
using UGUI;

namespace UGUIEditor
{
    public class UAtlasMakerWindow : EditorWindow
    {
        #region Fields

        private static int m_padding = 2;
        private static bool m_unityPacker = true;
        private static bool m_forceSquare = true;
        private static bool m_texturePacker = false;
        private List<Texture2D> m_textures = new List<Texture2D>();
        private Vector2 m_scrollPosition = Vector2.zero;
        private string m_helpInfo = string.Empty;
        private MessageType m_helpMessageType = MessageType.Info;


        private UGUI.UAtlasData m_atlas;

        private Texture2D[] m_allSelectTextures;
        private List<Texture2D> m_addTextures;
        private List<Texture2D> m_updataTextures = new List<Texture2D>();
        private List<Texture2D> m_loseTextures = new List<Texture2D>();
        private List<Texture2D> m_unUpdateTextures = new List<Texture2D>();
        private List<Texture2D> m_removeTextures = new List<Texture2D>();


        private List<UGUI.USpriteData> m_updataDatas = new List<UGUI.USpriteData>();
        private List<UGUI.USpriteData> m_loseDatas = new List<UGUI.USpriteData>();
        private List<UGUI.USpriteData> m_unUpdateDatas = new List<UGUI.USpriteData>();
        private List<UGUI.USpriteData> m_removeDatas = new List<UGUI.USpriteData>();

        #endregion

        [MenuItem("Tools/UGUI/Open/UI Atlas Maker", false, 1)]
        static void OpenUIAtlasMakerWindow()
        {
            UAtlasMakerWindow _window = EditorWindow.GetWindow<UAtlasMakerWindow>();
            _window.titleContent = new GUIContent("UI Atlas Maker");
        }

        #region Mono Methods

        private void OnEnable()
        {
            m_atlas = AssetDatabase.LoadAssetAtPath<UGUI.UAtlasData>(
                AssetDatabase.GUIDToAssetPath(EditorPrefs.GetString("UIAtlasMakerWindow.m_atlas", string.Empty)));
            if (m_atlas != null)
            {
                m_padding = m_atlas.m_padding;
                m_unityPacker = m_atlas.m_unityPacker;
                m_forceSquare = m_atlas.m_forceSquare;
                m_texturePacker = m_atlas.m_texturePacker;
            }
        }

        private void OnDestroy()
        {
            if (m_atlas != null)
            {
                EditorPrefs.SetString("UIAtlasMakerWindow.m_atlas",
                    AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(m_atlas)));
            }
        }

        public void OnGUI()
        {
            m_helpInfo = string.Empty;

            RefreshData();

            GUILayout.Space(5);
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.LabelField("current ui atlas with the following parameters:", (GUIStyle) "BoldLabel");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("current atlas", GUILayout.Width(90));
            GUILayout.Space(10);
            m_atlas = EditorGUILayout.ObjectField(m_atlas, typeof(UGUI.UAtlasData), false) as UGUI.UAtlasData;
            EditorGUI.BeginDisabledGroup(!m_atlas);
            {
                if (GUILayout.Button("New", GUILayout.Width(50)))
                {
                    m_atlas = null;
                }
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
            if (EditorGUI.EndChangeCheck())
            {
                if (m_atlas != null)
                {
                    m_padding = m_atlas.m_padding;
                    m_unityPacker = m_atlas.m_unityPacker;
                    m_forceSquare = m_atlas.m_forceSquare;
                    m_texturePacker = m_atlas.m_texturePacker;
                    RefreshData();
                }
            }

            if (m_atlas != null && m_atlas.m_texture != null && m_atlas.m_material != null)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Texture", GUILayout.Width(100)))
                {
                    Selection.objects = new Object[] {m_atlas.m_texture};
                }

                EditorGUILayout.LabelField(m_atlas.m_texture.width + "X" + m_atlas.m_texture.height,
                    GUILayout.Width(200));
                EditorGUILayout.LabelField("texture size,click texture button to select the texture");
                if (GUILayout.Button("S", GUILayout.Width(20)))
                {
                    string _folderPath =
                        EditorUtility.SaveFolderPanel("save sprite textures", Application.dataPath, m_atlas.name);
                    for (int i = 0; i < m_atlas.m_sprites.Length; i++)
                    {
                        if (m_atlas.m_sprites[i].m_sprite != null)
                        {
                            EditorUtility.DisplayProgressBar("save sprite Textures",
                                "save " + m_atlas.m_sprites[i].m_name + " . . .", i * 1.0f / m_atlas.m_sprites.Length);
                            Texture2D _spriteTexture = m_atlas.GetSpriteTextureBySprite(m_atlas.m_sprites[i].m_sprite);
                            byte[] _bytes = _spriteTexture.EncodeToPNG();
                            System.IO.File.WriteAllBytes(_folderPath + "/" + m_atlas.m_sprites[i].m_name + ".png",
                                _bytes);
                        }
                    }

                    EditorUtility.ClearProgressBar();
                }

                EditorGUILayout.LabelField("save sprite Texture to other folder");
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Material", GUILayout.Width(100)))
                {
                    Selection.objects = new Object[] {m_atlas.m_material};
                }

                EditorGUILayout.LabelField("click material button to select the material");
                EditorGUILayout.EndHorizontal();
            }

            GUILayout.Space(5);


            GUILayout.Box("", "PopupCurveSwatchBackground", GUILayout.Width(this.position.width), GUILayout.Height(5));
            EditorGUI.BeginDisabledGroup(m_texturePacker);
            {
                EditorGUILayout.LabelField("Create a new ui atlas with the following parameters:",
                    (GUIStyle) "BoldLabel");

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Padding", GUILayout.Width(100));
                m_padding = EditorGUILayout.IntField(m_padding, GUILayout.Width(15));
                EditorGUILayout.LabelField("pixel between sprites");
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Unity Packer", GUILayout.Width(100));
                m_unityPacker = EditorGUILayout.Toggle(m_unityPacker, GUILayout.Width(15));
                EditorGUILayout.LabelField("or custom packer");
                EditorGUILayout.EndHorizontal();

                if (m_unityPacker == false)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Force Square", GUILayout.Width(100));
                    m_forceSquare = EditorGUILayout.Toggle(m_forceSquare, GUILayout.Width(15));
                    EditorGUILayout.LabelField("if on,forces a square atlas texture");
                    EditorGUILayout.EndHorizontal();
                }

                GUILayout.Space(5);
                GUILayout.Box("", "PopupCurveSwatchBackground", GUILayout.Width(this.position.width),
                    GUILayout.Height(5));


                GUILayout.Space(5);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("When ready,", GUILayout.Width(100));
                if (GUILayout.Button(m_atlas != null ? "Add/Update Atlas" : "Create Atlas", GUILayout.Width(155)))
                {
                    UpdateAtlas();
                }

                EditorGUILayout.EndHorizontal();
            }
            EditorGUI.EndDisabledGroup();


            GUILayout.Space(5);
            GUILayout.Toggle(true, "<b><size=11>Sprites</size></b>", (GUIStyle) "dragtab");

            GUILayout.Space(5);
            m_scrollPosition = EditorGUILayout.BeginScrollView(m_scrollPosition);

            if (m_texturePacker)
            {
                int _index = 0;
                for (int i = 0; i < m_loseDatas.Count; i++)
                {
                    _index++;
                    EditorGUILayout.BeginHorizontal((GUIStyle) "box", GUILayout.Height(30));
                    EditorGUILayout.LabelField(" " + _index.ToString(), GUILayout.Width(30));
                    EditorGUILayout.ObjectField(m_loseDatas[i].m_sprite, typeof(Sprite), false);
                    EditorGUILayout.EndHorizontal();
                }
            }
            else
            {
                int _index = 0;
                for (int i = 0; i < m_addTextures.Count; i++)
                {
                    _index++;
                    EditorGUILayout.BeginHorizontal((GUIStyle) "box", GUILayout.Height(30));
                    EditorGUILayout.LabelField(" " + _index.ToString(), GUILayout.Width(30));
                    EditorGUILayout.ObjectField(m_addTextures[i], typeof(Texture), false);
                    GUI.color = Color.green;
                    EditorGUILayout.LabelField("Add", GUILayout.Width(50), GUILayout.Height(15));
                    GUI.color = Color.white;
                    GUILayout.Space(27);
                    EditorGUILayout.EndHorizontal();
                }

                for (int i = 0; i < m_updataDatas.Count; i++)
                {
                    _index++;
                    EditorGUILayout.BeginHorizontal((GUIStyle) "box", GUILayout.Height(30));
                    EditorGUILayout.LabelField(" " + _index.ToString(), GUILayout.Width(30));
                    EditorGUILayout.ObjectField(m_updataTextures[i], typeof(Texture2D), false);
                    EditorGUILayout.ObjectField(m_updataDatas[i].m_sprite, typeof(Sprite), false);
                    GUI.color = Color.green;
                    EditorGUILayout.LabelField("Update", GUILayout.Width(50), GUILayout.Height(15));
                    GUI.color = Color.white;
                    GUILayout.Space(28);
                    EditorGUILayout.EndHorizontal();
                }

                for (int i = 0; i < m_loseDatas.Count; i++)
                {
                    _index++;
                    EditorGUILayout.BeginHorizontal((GUIStyle) "box", GUILayout.Height(30));
                    EditorGUILayout.LabelField(" " + _index.ToString(), GUILayout.Width(30));
                    EditorGUILayout.ObjectField(m_loseTextures[i], typeof(Texture2D), false);
                    EditorGUILayout.ObjectField(m_loseDatas[i].m_sprite, typeof(Sprite), false);
                    GUI.color = Color.yellow;
                    EditorGUILayout.LabelField("Lose", GUILayout.Width(50), GUILayout.Height(15));
                    GUI.color = Color.white;
                    if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(15)))
                    {
                        m_removeDatas.Add(m_loseDatas[i]);
                        m_removeTextures.Add(m_loseTextures[i]);

                        m_loseTextures.RemoveAt(i);
                        m_loseDatas.RemoveAt(i);

                        UpdateAtlas();
                        return;
                    }

                    EditorGUILayout.EndHorizontal();
                }

                for (int i = 0; i < m_unUpdateDatas.Count; i++)
                {
                    _index++;
                    EditorGUILayout.BeginHorizontal((GUIStyle) "box", GUILayout.Height(30));
                    EditorGUILayout.LabelField(" " + _index.ToString(), GUILayout.Width(30));
                    EditorGUILayout.ObjectField(m_unUpdateTextures[i], typeof(Texture2D), false);
                    EditorGUILayout.ObjectField(m_unUpdateDatas[i].m_sprite, typeof(Sprite), false);
                    EditorGUILayout.LabelField("", GUILayout.Width(50), GUILayout.Height(15));
                    if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(15)))
                    {
                        m_removeDatas.Add(m_unUpdateDatas[i]);
                        m_removeTextures.Add(m_unUpdateTextures[i]);

                        m_unUpdateTextures.RemoveAt(i);
                        m_unUpdateDatas.RemoveAt(i);

                        UpdateAtlas();
                        return;
                    }

                    EditorGUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.EndScrollView();
            GUILayout.Space(5);
            if (string.IsNullOrEmpty(m_helpInfo) == false)
            {
                EditorGUILayout.HelpBox(m_helpInfo, m_helpMessageType);
                GUILayout.Space(5);
            }

            this.Repaint();
            SceneView.RepaintAll();
        }

        private void UpdateAtlas()
        {
            string _savePath = string.Empty;
            if (m_atlas != null)
            {
                if (m_removeDatas.Count > 0)
                {
                    if (!EditorUtility.DisplayDialog("Delect Sprite", "delect the sprite!!!", "yes"))
                    {
                        return;
                    }
                }

                _savePath = Application.dataPath.Substring(0, Application.dataPath.Length - 6) +
                            AssetDatabase.GetAssetPath(m_atlas);
                _savePath = System.IO.Path.ChangeExtension(_savePath, "asset");
            }
            else
            {
                _savePath = EditorUtility.SaveFilePanel("save atlas", "", "newAtlas", "asset");
            }

            if (string.IsNullOrEmpty(_savePath) == false)
            {
                if (m_atlas != null)
                {
                    for (int i = 0; i < m_loseDatas.Count; i++)
                    {
                        m_loseTextures[i] = m_atlas.GetSpriteTextureByUISpriteData(m_loseDatas[i]);
                    }
                }

                m_textures.Clear();
                m_textures.AddRange(m_addTextures);
                m_textures.AddRange(m_updataTextures);
                m_textures.AddRange(m_loseTextures);
                m_textures.AddRange(m_unUpdateTextures);

                //refresh spriteTexture to readorwrite
                for (int i = 0; i < m_textures.Count; i++)
                {
                    string _assetPath = AssetDatabase.GetAssetPath(m_textures[i]);
                    TextureImporter _textureImporter = TextureImporter.GetAtPath(_assetPath) as TextureImporter;
                    if (_textureImporter != null)
                    {
                        _textureImporter.isReadable = true;
                        _textureImporter.alphaIsTransparency = true;
                        _textureImporter.SaveAndReimport();
                    }
                }

                string _materialPath = _savePath.Replace(Application.dataPath, "Assets");
                _materialPath = Path.Combine(Path.GetDirectoryName(_materialPath),
                    Path.GetFileNameWithoutExtension(_materialPath) + "Material" + ".mat");
                _materialPath = _materialPath.Replace(@"\", "/");

                CreateMaterial(_materialPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                //texture packer methods
                //rect methodName(texture2D target,texture2D[] sprites) 
                string _texturePath = Path.Combine(Path.GetDirectoryName(_savePath),
                    Path.GetFileNameWithoutExtension(_savePath) + "Texture" + ".png");
                _texturePath = _texturePath.Replace(@"\", "/");
                Texture2D _texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                Rect[] _rects = PackTextures(ref _texture, m_textures.ToArray());

                byte[] bytes = _texture.EncodeToPNG();
                System.IO.File.WriteAllBytes(_texturePath, bytes);
                bytes = null;

                // Load the texture we just saved as a Texture2D
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                string _textureAssetPath = _texturePath.Replace(Application.dataPath, "Assets");
                TextureImporter _importer = TextureImporter.GetAtPath(_textureAssetPath) as TextureImporter;
                _importer.textureType = TextureImporterType.Sprite;
                _importer.spriteImportMode = SpriteImportMode.Multiple;
                _importer.isReadable = true;
                _importer.alphaIsTransparency = true;

                SpriteMetaData[] old = _importer.spritesheet;
                Dictionary<string, SpriteMetaData> dicMetas = new Dictionary<string, SpriteMetaData>();
                for (int i = 0; i < old.Length; i++)
                {
                    dicMetas[old[i].name] = old[i];
                }

                SpriteMetaData[] _metaDatas = new SpriteMetaData[_rects.Length];

                
                for (int i = 0; i < _metaDatas.Length; i++)
                {
                    _metaDatas[i].alignment = 0;
                    _metaDatas[i].name = m_textures[i].name;
                    _metaDatas[i].rect = new Rect(
                        Mathf.RoundToInt(_rects[i].x * _texture.width),
                        Mathf.RoundToInt(_rects[i].y * _texture.height),
                        Mathf.RoundToInt(_rects[i].width * _texture.width),
                        Mathf.RoundToInt(_rects[i].height * _texture.height));
                    
                    if (dicMetas.TryGetValue(m_textures[i].name, out var data))
                    {
                        _metaDatas[i].pivot = data.pivot;
                        _metaDatas[i].border = data.border;
                    }
                }

                _importer.spritesheet = _metaDatas;
                _importer.secondarySpriteTextures= new SecondarySpriteTexture[0];
                _importer.SaveAndReimport();


                string atlasPath = Path.Combine(Path.GetDirectoryName(_savePath),
                    Path.GetFileNameWithoutExtension(_savePath) + ".asset");
                atlasPath = atlasPath.Replace(@"\", "/");
                atlasPath = atlasPath.Replace(Application.dataPath, "Assets");
                m_atlas = CreateUAtalsData(atlasPath, _textureAssetPath, _materialPath, m_textures.ToArray());

                if (m_atlas != null)
                {
                    EditorPrefs.SetString("UIAtlasMakerWindow.m_atlas",
                        AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(m_atlas)));
                }

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        #endregion

        #region Other Methods

        private static UGUI.USpriteData[] GetUISpriteDataByTextures(Texture2D[] textures)
        {
            UGUI.USpriteData[] _datas = new UGUI.USpriteData[textures.Length];
            for (int i = 0; i < textures.Length; i++)
            {
                _datas[i] = new UGUI.USpriteData();
                _datas[i].m_name = textures[i].name;
                _datas[i].m_sourceTextureGuid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(textures[i]));
            }

            return _datas;
        }

        #endregion

        #region mathf Textures datas

        private void RefreshData()
        {
#if UNITY_2017
            m_allSelectTextures = Selection.GetFiltered<Texture2D>(SelectionMode.DeepAssets);
#else
            Object[] _objects = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
            m_allSelectTextures = new Texture2D[_objects.Length];
            for (int i = 0; i < _objects.Length; i++)
            {
                m_allSelectTextures[i] = _objects[i] as Texture2D;
            }
#endif
            m_addTextures = m_allSelectTextures.ToList();
            if (m_atlas != null)
            {
                if (m_addTextures.Contains(m_atlas.m_texture))
                {
                    m_addTextures.Remove(m_atlas.m_texture);
                }
            }

            m_updataTextures.Clear();
            m_loseTextures.Clear();
            m_unUpdateTextures.Clear();
            m_removeTextures.Clear();

            m_updataDatas.Clear();
            m_loseDatas.Clear();
            m_unUpdateDatas.Clear();
            m_removeDatas.Clear();

            if (m_atlas != null && m_atlas.m_sprites != null)
            {
                for (int i = 0; i < m_atlas.m_sprites.Length; i++)
                {
                    Texture2D _spriteSourceTexture =
                        AssetDatabase.LoadAssetAtPath<Texture2D>(
                            AssetDatabase.GUIDToAssetPath(m_atlas.m_sprites[i].m_sourceTextureGuid));
                    if (_spriteSourceTexture != null)
                    {
                        bool _isUpdate = false;
                        for (int t = 0; t < m_allSelectTextures.Length; t++)
                        {
                            if (_spriteSourceTexture == m_allSelectTextures[t])
                            {
                                _isUpdate = true;
                                break;
                            }
                        }

                        if (_isUpdate == true)
                        {
                            m_updataTextures.Add(_spriteSourceTexture);
                            m_updataDatas.Add(m_atlas.m_sprites[i]);
                            m_addTextures.Remove(_spriteSourceTexture);
                        }
                        else
                        {
                            m_unUpdateDatas.Add(m_atlas.m_sprites[i]);
                            m_unUpdateTextures.Add(_spriteSourceTexture);
                        }
                    }
                    else
                    {
                        m_loseDatas.Add(m_atlas.m_sprites[i]);
                        m_loseTextures.Add(_spriteSourceTexture);
                    }
                }
            }

            if (m_loseDatas.Count > 0 && m_texturePacker == false)
            {
                m_helpInfo = "No source resources found through guid";
                m_helpMessageType = MessageType.Warning;
            }
        }

        #endregion

        #region Texture Packer

        /// <summary>
        /// pack textures 
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="sprites"></param>
        /// <returns></returns>
        private Rect[] PackTextures(ref Texture2D texture, Texture2D[] sprites)
        {
            Rect[] rects;

#if UNITY_3_5 || UNITY_4_0
		int maxSize = 4096;
#else
            int maxSize = SystemInfo.maxTextureSize;
#endif

#if UNITY_ANDROID || UNITY_IPHONE
		maxSize = Mathf.Min(maxSize, 4096);
#endif
            if (m_unityPacker)
            {
                rects = texture.PackTextures(sprites, m_padding, maxSize);
            }
            else
            {
                rects = UGUI.UTexturePacker.PackTextures(texture, sprites, 4, 4, m_padding, maxSize, m_forceSquare);
            }

            return rects;
        }

        #endregion

        public static Material CreateMaterial(string textureAssetPath)
        {
            Material _material = AssetDatabase.LoadAssetAtPath<Material>(textureAssetPath);
            bool _contains = true;
            if (_material == null)
            {
                _contains = false;
                _material = new Material(Shader.Find("UI/Default"));
            }

            if (_contains)
            {
                AssetDatabase.ImportAsset(textureAssetPath, ImportAssetOptions.ForceUpdate);
            }
            else
            {
                AssetDatabase.CreateAsset(_material, textureAssetPath);
            }

            return _material;
        }

        public static UGUI.UAtlasData CreateUAtalsData(string atlasPath, string textureAssetPath, string materialPath,
            Texture2D[] sourceTextures, bool texturePacker = false)
        {
            string _dataAssetPath = System.IO.Path.ChangeExtension(atlasPath, "asset");
            UGUI.UAtlasData _atlas = AssetDatabase.LoadAssetAtPath<UGUI.UAtlasData>(_dataAssetPath);
            bool _contains = true;
            if (_atlas == null)
            {
                _contains = false;
                _atlas = ScriptableObject.CreateInstance<UGUI.UAtlasData>();
            }

            _atlas.m_padding = m_padding;
            _atlas.m_unityPacker = m_unityPacker;
            _atlas.m_forceSquare = m_forceSquare;
            m_texturePacker = texturePacker;
            _atlas.m_texturePacker = texturePacker;
            _atlas.m_texture = AssetDatabase.LoadAssetAtPath<Texture2D>(textureAssetPath);
            _atlas.m_material = AssetDatabase.LoadAssetAtPath<Material>(materialPath);
            _atlas.m_material.mainTexture = _atlas.m_texture;
            Object[] _objects = UnityEditor.AssetDatabase.LoadAllAssetsAtPath(textureAssetPath);
            List<UGUI.USpriteData> _spriteDatas = new List<USpriteData>();
            for (int i = 0; i < _objects.Length; i++)
            {
                Sprite _sprite = _objects[i] as Sprite;
                if (_sprite != null)
                {
                    UGUI.USpriteData _data = new USpriteData();
                    _data.m_name = _sprite.name;
                    _data.m_sprite = _sprite;
                    _data.m_rect = _sprite.rect;
                    _data.m_pivot = _sprite.pivot;
                    _data.m_uv = _sprite.uv;
                    if (sourceTextures != null)
                    {
                        for (int s = 0; s < sourceTextures.Length; s++)
                        {
                            if (sourceTextures[s].name == _sprite.name)
                            {
                                _data.m_sourceTextureGuid =
                                    AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(sourceTextures[s]));
                                break;
                            }
                        }
                    }

                    _spriteDatas.Add(_data);
                }
            }

            _atlas.m_sprites = _spriteDatas.ToArray();

            if (_contains)
            {
                AssetDatabase.ImportAsset(_dataAssetPath, ImportAssetOptions.ForceUpdate);
            }
            else
            {
                AssetDatabase.CreateAsset(_atlas, _dataAssetPath);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return _atlas;
        }
    }
}