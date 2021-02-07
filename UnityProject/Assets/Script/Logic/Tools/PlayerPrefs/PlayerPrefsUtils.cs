//======================================================================== 
//	
// 	 Maggic @ 2020/2/12 9:56:08　　　　　　　
// 	
//========================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsUtils
{
    static bool m_enable = true;

    public static void DeleteAll()
    {
        if (m_enable)
        {
            PlayerPrefs.DeleteAll();
        }
    }
    public static void DeleteKey(string key)
    {
        if (m_enable)
        {
            PlayerPrefs.DeleteKey(key);
        }
    }

    public static float GetFloat(string key)
    {
        if (m_enable)
        {
            return PlayerPrefs.GetFloat(key);
        }
        else
        {
            return -1;
        }
    }
    public static float GetFloat(string key, float defaultValue)
    {
        if (m_enable)
        {
            return PlayerPrefs.GetFloat(key, defaultValue);
        }
        else
        {
            return -1;
        }
    }

    public static int GetInt(string key)
    {
        if (m_enable)
        {
            return PlayerPrefs.GetInt(key);
        }
        else
        {
            return -1;
        }
    }
    public static int GetInt(string key, int defaultValue)
    {
        if (m_enable)
        {
            return PlayerPrefs.GetInt(key, defaultValue);

        }
        else
        {
            return -1;
        }
    }
    public static string GetString(string key)
    {
        if (m_enable)
        {
            return PlayerPrefs.GetString(key);
        }
        else
        {
            return "";
        }
    }
    public static string GetString(string key, string defaultValue)
    {
        if (m_enable)
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }
        else
        {
            return "";
        }
    }
    public static bool HasKey(string key)
    {
        if (m_enable)
        {
            return PlayerPrefs.HasKey(key);
        }
        else
        {
            return false;
        }
    }
    public static void Save()
    {
        if (m_enable)
        {
            PlayerPrefs.Save();
        }
    }
    public static void SetFloat(string key, float value)
    {
        if (m_enable)
        {
            PlayerPrefs.SetFloat(key, value);
        }
    }
    public static void SetInt(string key, int value)
    {
        if (m_enable)
        {
            PlayerPrefs.SetInt(key, value);
        }
    }
    public static void SetString(string key, string value)
    {
        if (m_enable)
        {
            PlayerPrefs.SetString(key, value);
        }
    }
}
