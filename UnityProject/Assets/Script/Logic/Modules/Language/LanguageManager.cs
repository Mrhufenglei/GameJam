//----------------------------------------------------------------------
//
//              Maggic @  2019/12/13 16:15:30
//
//---------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LocalModels.Bean;
using LocalModels.Model;
/// <summary>
/// 
/// </summary>
public class LanguageManager : Singleton<LanguageManager>
{
    private LanguageDataModule m_data;

    public SystemLanguage m_systemLanguage = SystemLanguage.English;
    /// <summary>
    /// 通过语言类型和列表ID获得语言内容
    /// </summary>
    /// <param name="languageType"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public string GetInfoByID(LanguageType languageType, int id)
    {
        string _info = string.Empty;
        try
        {
            Language_languagetable _table = GameApp.Table.GetLanguage_languagetableModelInstance().GetElementById(id);
            _info = GetInfoByID(_table, languageType);
        }
        catch (System.Exception e)
        {
            throw new System.Exception("LanguageManager.GetInfoByID------>\n(" + languageType + "," + id + ")\n" + e.Message);
        }
        return _info;
    }

    public string GetInfoByID(Language_languagetable table, LanguageType languageType)
    {
        string _info = string.Empty;
        try
        {
            switch (languageType)
            {
                case LanguageType.English:
                    _info = table.english;
                    break;
                case LanguageType.Spanish:
                    _info = table.spanish;
                    break;
                case LanguageType.ChineseSimplified:
                    _info = table.chinesesimplified;
                    break;
                case LanguageType.ChineseTraditional:
                    _info = table.chinesetraditional;
                    break;
                case LanguageType.Japanese:
                    _info = table.japanese;
                    break;
                case LanguageType.French:
                    _info = table.french;
                    break;
                case LanguageType.German:
                    _info = table.german;
                    break;
                case LanguageType.Italian:
                    _info = table.italian;
                    break;
                case LanguageType.Dutch:
                    _info = table.dutch;
                    break;
                case LanguageType.Russian:
                    _info = table.russian;
                    break;
                case LanguageType.Arabic:
                    _info = table.arabic;
                    break;
                case LanguageType.Korean:
                    _info = table.korean;
                    break;
                default:
                    _info = table.english;
                    break;
            }
        }
        catch (System.Exception e)
        {
            throw new System.Exception("LanguageManager.GetInfoByID------>\n(" + languageType + "," + table.id + ")\n" + e.Message);
        }
        return _info;
    }
    /// <summary>
    /// 通过当前的语言类型和列表ID获得语言内容
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public string GetInfoByID(int id)
    {
        return GetInfoByID(m_data.GetCurrentLanguageType, id);
    }
    public void CheckLanguage()
    {
        m_data = GameApp.Data.GetDataModule<LanguageDataModule>(DataName.LanguageDataModule);
        m_data.CheckLanguage();
    }
}
