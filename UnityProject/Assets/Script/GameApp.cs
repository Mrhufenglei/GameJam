using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameApp : MonoBehaviour
{
    [SerializeField]
    public EventSystemManager m_event = null;

    [SerializeField]
    private DataModuleManager m_data = null;

    [SerializeField]
    public CoroutineManager m_coroutine = null;

    [SerializeField]
    public ViewModuleManager m_ui = null;

    [SerializeField]
    public StateManager m_state = null;

    [SerializeField]
    public TableManager m_table = null;

    [SerializeField]
    public ResourcesManager m_resources = null;

    [SerializeField]
    public SceneManager m_scene = null;

    [SerializeField]
    public SoundManager m_sound = null;

    [SerializeField]
    public HttpManager m_http = null;


    public static EventSystemManager Event
    {
        get; private set;
    }
    public static DataModuleManager Data
    {
        get; private set;
    }
    public static CoroutineManager CoroutineSystem
    {
        get; private set;
    }
    public static ViewModuleManager UI
    {
        get; private set;
    }
    public static StateManager State
    {
        get; private set;
    }
    public static TableManager Table
    {
        get; private set;
    }
    public static ResourcesManager Resources
    {
        get; private set;
    }

    public static SceneManager Scene
    {
        get; private set;
    }
    public static SoundManager Sound
    {
        get; set;
    }

    public static HttpManager Http
    {
        get; set;
    }

    /// <summary>
    /// 框架启动
    /// </summary>
    public void StarUp()
    {
        Event = m_event;

        Data = m_data;
        CoroutineSystem = m_coroutine;

        UI = m_ui;
        State = m_state;

        m_table.OnInitialize();
        Table = m_table;

        Resources = m_resources;
        Scene = m_scene;
        Sound = m_sound;

        Http = m_http;

        RegisterAllMessage(Event);
        RegisterAllDataModules(Data);
        RegisterAllViewModules(UI);
        RegisterAllStates(State);
    }

    public void OnUpdate()
    {
        Event.OnUpdate(Time.deltaTime, Time.unscaledDeltaTime);
        UI.OnUpdate(Time.deltaTime, Time.unscaledDeltaTime);
        State.OnUpdate(Time.deltaTime, Time.unscaledDeltaTime);
    }
    /// <summary>
    /// 注册所有网络消息
    /// </summary>
    private static void RegisterAllMessage(EventSystemManager events)
    {
        //MessageRegister.registerAllMessage(Singleton<TcpClient>.getSingleton());
    }
    /// <summary>
    ///  注册所有数据层
    /// </summary>
    private static void RegisterAllDataModules(DataModuleManager datas)
    {
        //多语言
        datas.RegisterDataModule(new LanguageDataModule());

    }
    /// <summary>
    /// 注册所有视图 
    /// </summary>
    private static void RegisterAllViewModules(ViewModuleManager views)
    {

        views.RegisterViewModule(new ViewModuleData((int)ViewName.StarupViewModule, string.Empty, GameApp.UI.Pool.m_starupUI, false));
        views.RegisterViewModule(new ViewModuleData((int)ViewName.LoginViewModule, "Assets/_Resources/Prefab/UI/LoginUI.prefab", null));
        views.RegisterViewModule(new ViewModuleData((int)ViewName.LoadingViewModule, "Assets/_Resources/Prefab/UI/LoadingUI.prefab", null));
        views.RegisterViewModule(new ViewModuleData((int)ViewName.SelectLevelViewModule, "Assets/_Resources/Prefab/UI/SelectLevelUI.prefab", null));
    }
    /// <summary>
    /// 注册所有状态
    /// </summary>
    private static void RegisterAllStates(StateManager states)
    {
        states.RegisterState(new CheckAssetsState());
        states.RegisterState(new LoginState());
        states.RegisterState(new LoadingToSelectLevelState());
        states.RegisterState(new SelectLevelState());
    }
}
