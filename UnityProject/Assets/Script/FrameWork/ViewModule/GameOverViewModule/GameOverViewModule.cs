//-----------------------------------------------------------------
//
//              Maggic @  2021-02-07 13:33:39
//
//----------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class GameOverViewModule : IViewModule
{
    private GameObject m_gameObject;
    public Button m_button;

    #region IViewModule

    public int GetName()
    {
        return (int) ViewName.GameOverViewModule;
    }

    public GameObject OnCreate()
    {
        m_gameObject = GameApp.UI.Pool.m_gameOverUI;
        return m_gameObject;
    }

    public void OnOpen(object data)
    {
        var dic = ViewTools.CollectAllGameObjects(m_gameObject);
        m_button = dic["GameOverButton"].GetComponent<Button>();
        if (m_button != null) m_button.onClick.AddListener(OnClickButton);
    }

    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
    }

    public void OnClose()
    {
    }

    public void RegisterEvents(EventSystemManager manager)
    {
    }

    public void UnRegisterEvents(EventSystemManager manager)
    {
    }

    #endregion

    #region OnClick

    private void OnClickButton()
    {
       GameApp.State.ActiveState(StateName.MainState);
    }

    #endregion
}