using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLauncher : MonoBehaviour
{
    [SerializeField]
    private GameApp m_gameApp;
    [Header("Language Setting")]
    public bool m_isReadyLanguage = false;
    public LanguageType m_languageType = LanguageType.English;
    
    void Awake()
    {
        //Application.targetFrameRate = 30;
        GameObject.DontDestroyOnLoad(this.gameObject);

        m_gameApp.StarUp();
        
        LanguageManager.Instance.CheckLanguage();
        if (m_isReadyLanguage) GameApp.Event.DispatchNow((int)LocalMessageName.CC_REFRESH_LANGUAGE, m_languageType);
        //默认loading界面为关闭
        GameApp.UI.OpenView(ViewName.LoadingViewModule, LoadingViewModule.LoadingType.Opened, UILayers.Second);
      
        GameApp.State.ActiveState(StateName.MainState);

    }
    private void Update()
    {
        m_gameApp.OnUpdate();
    }
    //void OnBegin(string path)
    //{
    //    Debug.Log("OnBegin = " + path);
    //}
    //void OnPregress(string path, float progress)
    //{
    //    Debug.Log("OnPregress = " + path + " , " + progress);
    //}
    //void OnComplete(string path, Object obj)
    //{
    //    Debug.Log("OnComplete = " + path);
    //}

    //private void OnGUI()
    //{
    //    if (GUILayout.Button("Loading", GUILayout.Width(200), GUILayout.Height(200)))
    //    {
    //        ResourcesTask[] resourcesTasks = new ResourcesTask[8];
    //        for (int i = 0; i < 8; i++)
    //        {
    //            ResourcesTask task = new ResourcesTask();
    //            task.path = string.Format("Prefab/UI/GameObject ({0})", i);
    //            task.onBegin = OnBegin;
    //            task.onProgress = OnPregress;
    //            task.onComplete = OnComplete;
    //            resourcesTasks[i] = task;
    //        }
    //        GameApp.Resources.LoadAsync(resourcesTasks);
    //    }
    //}
}
