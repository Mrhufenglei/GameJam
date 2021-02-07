//-----------------------------------------------------------------
//
//              Maggic @  2021-02-07 14:41:07
//
//----------------------------------------------------------------

using System;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public partial class GameController : MonoBehaviour, IGameController
{
    public OpController m_opController;
    public MapController m_mapController;
    public CameraController m_cameraController;
    // public CameraController m_bombController;

    public static GameController Builder;

    [SerializeField] [Label] private State m_state = State.GameStart;

    #region Mono

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        OnUpdate(Time.deltaTime, Time.unscaledDeltaTime);
    }

    private void OnDestroy()
    {
        OnDeInit();
    }

    #endregion

    #region IGameController

    public void OnInit()
    {
        Builder = this;
        m_state = State.GameStart;
        if (m_opController != null) m_opController.OnInit();
        if (m_mapController != null) m_mapController.OnInit();
        if (m_cameraController != null) m_cameraController.OnInit();

        GameApp.Event.RegisterEvent(LocalMessageName.CC_GAME_Start, OnEventGameStart);
        GameApp.Event.RegisterEvent(LocalMessageName.CC_GAME_WIN, OnEventGameWin);
        GameApp.Event.RegisterEvent(LocalMessageName.CC_GAME_FAIL, OnEventGameFail);
        GameApp.Event.RegisterEvent(LocalMessageName.CC_GAME_CHECKISOVERFORMEMBERS, OnEventCheckIsOverForMembers);
    }

    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
        if (m_state != State.Game) return;
        if (m_opController != null) m_opController.OnUpdate(deltaTime, unscaledDeltaTime);
        if (m_mapController != null) m_mapController.OnUpdate(deltaTime, unscaledDeltaTime);
        if (m_cameraController != null) m_cameraController.OnUpdate(deltaTime, unscaledDeltaTime);
    }

    public void OnDeInit()
    {
        GameApp.Event.UnRegisterEvent(LocalMessageName.CC_GAME_Start, OnEventGameStart);
        GameApp.Event.UnRegisterEvent(LocalMessageName.CC_GAME_WIN, OnEventGameWin);
        GameApp.Event.UnRegisterEvent(LocalMessageName.CC_GAME_FAIL, OnEventGameFail);
        GameApp.Event.UnRegisterEvent(LocalMessageName.CC_GAME_CHECKISOVERFORMEMBERS, OnEventCheckIsOverForMembers);

        if (m_opController != null) m_opController.OnDeInit();
        if (m_mapController != null) m_mapController.OnDeInit();
        if (m_cameraController != null) m_cameraController.OnDeInit();
    }

    public void OnReset()
    {
        m_state = State.GameStart;
        if (m_opController != null) m_opController.OnReset();
        if (m_mapController != null) m_mapController.OnReset();
        if (m_cameraController != null) m_cameraController.OnReset();
    }

    public void OnGameStart()
    {
        if (m_opController != null) m_opController.OnGameStart();
        if (m_mapController != null) m_mapController.OnGameStart();
        if (m_cameraController != null) m_cameraController.OnGameStart();
        m_state = State.Game;
    }

    public void OnPause(bool pause)
    {
        if (m_opController != null) m_opController.OnPause(pause);
        if (m_mapController != null) m_mapController.OnPause(pause);
        if (m_cameraController != null) m_cameraController.OnPause(pause);
    }

    public void OnGameOver(GameOverType gameOverType)
    {
        if (m_opController != null) m_opController.OnGameOver(gameOverType);
        if (m_mapController != null) m_mapController.OnGameOver(gameOverType);
        if (m_cameraController != null) m_cameraController.OnGameOver(gameOverType);
        
        GameApp.UI.CloseView(ViewName.GameViewModule);
        GameApp.UI.OpenView(ViewName.GameOverViewModule,gameOverType);
        
        m_state = State.GameOver;
    }

    #endregion

    #region OnEvent

    private void OnEventGameStart(int type, object eventObject)
    {
        OnGameStart();
    }

    private void OnEventGameWin(int type, object eventObject)
    {
        OnGameOver(GameOverType.Win);
    }

    private void OnEventGameFail(int type, object eventObject)
    {
        OnGameOver(GameOverType.Failure);
    }

    private void OnEventCheckIsOverForMembers(int type, object eventObject)
    {
        if(m_mapController!=null)m_mapController.CheckIsOverForMembers();
    }

    #endregion

    #region Other Mothed

    public State GetState()
    {
        return m_state;
    }

    #endregion
}