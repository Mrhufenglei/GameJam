using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameApp : MonoBehaviour
{
    [SerializeField]
    public EventSystemManager m_event;

    [SerializeField]
    private DataModuleManager m_data;

    [SerializeField]
    public CoroutineManager m_coroutine;

    [SerializeField]
    public ViewModuleManager m_ui;

    [SerializeField]
    public StateManager m_state;


    [SerializeField]
    public LocalModels.LocalModelManager m_table;

    [SerializeField]
    public ResourcesManager m_resources;

    [SerializeField]
    public SceneManager m_scene;

    [SerializeField]
    public SoundManager m_sound;

    [SerializeField]
    public HttpManager m_http;


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
    public static LocalModels.LocalModelManager Table
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
        Scene.OnUpdate(Time.deltaTime, Time.unscaledDeltaTime);
        Resources.OnUpdate(Time.deltaTime, Time.unscaledDeltaTime);
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
        views.RegisterViewModule(new ViewModuleData(new LoadingViewModule()));
        views.RegisterViewModule(new ViewModuleData(new MainViewModule()));
        views.RegisterViewModule(new ViewModuleData(new GameStartViewModule()));
        views.RegisterViewModule(new ViewModuleData(new GameViewModule()));
        views.RegisterViewModule(new ViewModuleData(new GameOverViewModule()));
    }
    /// <summary>
    /// 注册所有状态
    /// </summary>
    private static void RegisterAllStates(StateManager states)
    {
        states.RegisterState(new MainState());
        states.RegisterState(new GameState());

    }
}
