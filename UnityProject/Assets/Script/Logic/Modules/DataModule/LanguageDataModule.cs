//----------------------------------------------------------------------
//
//              Maggic @  2019/12/13 16:24:45
//
//---------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class LanguageDataModule : IDataModule
{
    private const string CURRENT_LANGUAGETYPE_KEY = "CurrentLanguageType";
    private LanguageType m_currentLanguageType = LanguageType.English;
    public LanguageType GetCurrentLanguageType
    {
        get
        {
            return m_currentLanguageType;
        }
    }

    #region IDataModule
    public int GetName()
    {
        return (int)DataName.LanguageDataModule;
    }

    public void RegisterEvents(EventSystemManager manager)
    {
        manager.RegisterEvent(LocalMessageName.CC_REFRESH_LANGUAGE, RefreshLanguageHandle);
    }

    public void UnRegisterEvents(EventSystemManager manager)
    {
        manager.UnRegisterEvent(LocalMessageName.CC_REFRESH_LANGUAGE, RefreshLanguageHandle);
    }
    #endregion

    private void RefreshLanguageHandle(int type, object eventObject)
    {
        m_currentLanguageType = (LanguageType)eventObject;
        Debug.Log("RefreshLanguageHandle ---->" + m_currentLanguageType);
        PlayerPrefsUtils.SetInt(LocalDataName.CURRENTLANGUAGETYPEKEY, (int)m_currentLanguageType);
    }

    public void CheckLanguage()
    {
        if (PlayerPrefsUtils.HasKey(LocalDataName.CURRENTLANGUAGETYPEKEY) == true)
        {
            m_currentLanguageType = (LanguageType)PlayerPrefsUtils.GetInt(LocalDataName.CURRENTLANGUAGETYPEKEY);
            Debug.Log("CheckLanguage ---->_type=" + m_currentLanguageType);
            return;
        }

        LanguageType _type = LanguageType.English;

        switch (Application.systemLanguage)
        {
            case SystemLanguage.English:
                _type = LanguageType.English;
                break;
            case SystemLanguage.Spanish:
                _type = LanguageType.Spanish;
                break;
            case SystemLanguage.Japanese:
                _type = LanguageType.Japanese;
                break;
            case SystemLanguage.French:
                _type = LanguageType.French;
                break;
            case SystemLanguage.German:
                _type = LanguageType.German;
                break;
            case SystemLanguage.Italian:
                _type = LanguageType.Italian;
                break;
            case SystemLanguage.Dutch:
                _type = LanguageType.Dutch;
                break;
            case SystemLanguage.Russian:
                _type = LanguageType.Russian;
                break;
            case SystemLanguage.Arabic:
                _type = LanguageType.Arabic;
                break;
            case SystemLanguage.Korean:
                _type = LanguageType.Korean;
                break;
            case SystemLanguage.Chinese:
            case SystemLanguage.ChineseSimplified:
                _type = LanguageType.ChineseSimplified;
                break;
            case SystemLanguage.ChineseTraditional:
                _type = LanguageType.ChineseTraditional;
                break;
            default:
                _type = LanguageType.English;
                break;
        }
        GameApp.Event.DispatchNow((int)LocalMessageName.CC_REFRESH_LANGUAGE, _type);
        Debug.Log("CheckLanguage---->_type =" + _type + " ,system = " + Application.systemLanguage);
    }
}
