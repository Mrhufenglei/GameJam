//----------------------------------------------------------------------
//
//              Maggic @  2020/8/6 14:04:32
//
//---------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// 
/// </summary>

public class PathManager
{
    private const string m_addressablePath = "Assets/_Resources/";

    public static string GetAddressablePath(string path)
    {
        return Path.Combine(m_addressablePath, path);
    }
    public static string GetAddressablePathForPrefab(string path)
    {
        return Path.ChangeExtension(Path.Combine(m_addressablePath, path), "prefab");
    }
    public static string GetAddressablePathForMaterial(string path)
    {
        return Path.ChangeExtension(Path.Combine(m_addressablePath, path), "material");
    }
    public static string GetAddressablePathForPNG(string path)
    {
        return Path.ChangeExtension(Path.Combine(m_addressablePath, path), "png");
    }
    public static string GetAddressablePathForAsset(string path)
    {
        return Path.ChangeExtension(Path.Combine(m_addressablePath, path), "asset");
    }
    public static string GetAddressablePathForScene(string path)
    {
        return Path.ChangeExtension(Path.Combine(m_addressablePath, path), "unity");
    }
    public static string GetAddressablePathForMP3(string path)
    {
        return Path.ChangeExtension(Path.Combine(m_addressablePath, path), "mp3");
    }
}
