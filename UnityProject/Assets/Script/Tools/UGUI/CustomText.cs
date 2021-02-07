//----------------------------------------------------------------------
//
//              Maggic @  2020/1/19 19:35:30
//
//---------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using LocalModels.Bean;
using LocalModels;

/// <summary>
/// 
/// </summary>
[AddComponentMenu("UI/CustomText", 21)]
public class CustomText : Text
{
    [SerializeField]
    private int m_languageId = 0;
    [SerializeField]
    private LanguageType m_language = LanguageType.English;

    protected override void OnEnable()
    {
        base.OnEnable();
        if (GameApp.Data != null)
        {
            LanguageDataModule data = GameApp.Data.GetDataModule<LanguageDataModule>(DataName.LanguageDataModule);
            m_language = data.GetCurrentLanguageType;
        }
        SetContent();
    }

    public void SetContent()
    {
        if (m_languageId > 0)
        {
            if (GameApp.Data != null)
            {
                text = LanguageManager.Instance.GetInfoByID(m_language, m_languageId);
            }
            else
            {
                try
                {
                    Language_languagetable table = Singleton<LocalModelManager>.Instance.GetLanguage_languagetableModelInstance().GetElementById(m_languageId);
                    text = LanguageManager.Instance.GetInfoByID(table, m_language);

                }
                catch (System.Exception e)
                {
                    Debug.LogError("language id is null----->" + m_languageId + "\n" + e.ToString(), this.gameObject);
                }
            }
        }
    }
}
