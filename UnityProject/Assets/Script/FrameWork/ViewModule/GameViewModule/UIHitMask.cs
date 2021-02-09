//-----------------------------------------------------------------
//
//              Maggic @  2021-02-09 12:41:52
//
//----------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class UIHitMask : MonoBehaviour
{
    public Image m_mask;

    public float m_max = 0.6f;

    [Header("State")] [Label] public bool m_isPlaying = false;

    [Header("Time Setting")] public float m_duration = 1;
    public float m_time = 0;

    #region View

    public void OnOpen(object data)
    {
        GameApp.Event.RegisterEvent(LocalMessageName.CC_GAME_Start, OnEventGameStart);
        GameApp.Event.RegisterEvent(LocalMessageName.CC_GAME_PlayerHit, OnEventPlayerHit);
        if (m_mask != null) m_mask.gameObject.SetActive(false);
    }

    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
        if (m_isPlaying == false) return;
        m_time += deltaTime;
        if (m_time >= m_duration)
        {
            //结束
            m_time = m_duration;
            m_isPlaying = false;
            if (m_mask != null) m_mask.gameObject.SetActive(false);
        }

        var value = m_time / m_duration * m_max;
        SetMask(value);
    }

    public void OnClose()
    {
        if (m_mask != null) m_mask.gameObject.SetActive(false);
        GameApp.Event.UnRegisterEvent(LocalMessageName.CC_GAME_Start, OnEventGameStart);
        GameApp.Event.UnRegisterEvent(LocalMessageName.CC_GAME_PlayerHit, OnEventPlayerHit);
    }

    #endregion


    #region Event

    private void OnEventGameStart(int type, object obj)
    {
        if (m_mask != null) m_mask.gameObject.SetActive(false);
    }

    private void OnEventPlayerHit(int type, object obj)
    {
        Play();
    }

    #endregion

    private void Play()
    {
        m_isPlaying = true;
        m_time = 0;
        if (m_mask != null) m_mask.gameObject.SetActive(true);
    }

    private void SetMask(float value)
    {
        if (m_mask != null) m_mask.color = new Color(1, 1, 1, value);
    }
}