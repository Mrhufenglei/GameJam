using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLauncher : MonoBehaviour
{
    [SerializeField] private GameApp m_gameApp = null;
    [Header("Language Setting")] public bool m_isReadyLanguage = false;
    public LanguageType m_languageType = LanguageType.English;

	
    void Awake()
    {
        //Application.targetFrameRate = 30;
        GameObject.DontDestroyOnLoad(this.gameObject);

        m_gameApp.StarUp();
		
		LanguageManager.Instance.CheckLanguage();
        if (m_isReadyLanguage)
        {
            var languageEvent = Singleton<EventArgLanguageType>.Instance;
            languageEvent.SetData(m_languageType);
            GameApp.Event.DispatchNow(this, (int) LocalMessageName.CC_REFRESH_LANGUAGE, languageEvent);
        }

        GameApp.State.ActiveState(StateName.CheckAssetsState);

    }
    private void Update()
    {
        m_gameApp.OnUpdate();
    }
 


  
}
