using UnityEngine;
using UnityEditor;
using UnityEditor.AddressableAssets;
using System.Collections.Generic;
using System.Linq;
using UnityAddressableImporter.Helper;
using UnityEditor.AddressableAssets.Settings;

[CreateAssetMenu(fileName = "AddressableImportSettings", menuName = "Addressable Assets/Import Settings", order = 50)]
public class AddressableImportSettings : ScriptableObject
{
    public const string kDefaultConfigObjectName = "addressableimportsettings";
    public const string kDefaultPath = "Assets/AddressableAssetsData/AddressableImportSettings.asset";

    [Tooltip("Creates a group if the specified group doesn't exist.")]
    public bool allowGroupCreation = false;

    [Tooltip("Rules for managing imported assets.")]
#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ListDrawerSettings(HideAddButton = false,Expanded = false,DraggableItems = true,HideRemoveButton = false)]
#endif
    public List<AddressableImportRule> rules;

    [ButtonMethod]
    public void Save()
    {
        AssetDatabase.SaveAssets();
    }

    [ButtonMethod]
    public void Documentation()
    {
        Application.OpenURL("https://github.com/favoyang/unity-addressable-importer/blob/master/Documentation~/AddressableImporter.md");
    }

    [ButtonMethod]
    public void CleanEmptyGroup()
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        if (settings == null)
        {
            return;
        }
        var dirty = false;
        var emptyGroups = settings.groups.Where(x => x.entries.Count == 0 && !x.IsDefaultGroup()).ToArray();
        for (var i = 0; i < emptyGroups.Length; i++)
        {
            dirty = true;
            settings.RemoveGroup(emptyGroups[i]);
        }
        if (dirty)
        {
            AssetDatabase.SaveAssets();
        }
    }

    [ButtonMethod]
    private void Refresh()
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        List<AddressableAssetGroup> groups = new List<AddressableAssetGroup>();
        if (settings.groups.Count > 2)
        {
            for (int i = 2; i < settings.groups.Count; i++)
            {
                groups.Add(settings.groups[i]);
            }
        }

        for (int i = 0; i < groups.Count; i++)
        {
            Debug.Log(groups[i].Name);
            settings.RemoveGroup(groups[i]);
        }

        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();

        AddressableImporter.FolderImporter.ReimportFolders(new string[] { "Assets" });

        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();
    }

    public static AddressableImportSettings Instance
    {
        get
        {
            AddressableImportSettings so;
            // Try to locate settings via EditorBuildSettings.
            if (EditorBuildSettings.TryGetConfigObject(kDefaultConfigObjectName, out so))
                return so;
            // Try to locate settings via path.
            so = AssetDatabase.LoadAssetAtPath<AddressableImportSettings>(kDefaultPath);
            if (so != null)
                EditorBuildSettings.AddConfigObject(kDefaultConfigObjectName, so, true);
            return so;
        }
    }
}