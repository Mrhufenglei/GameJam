using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RimRower : MonoBehaviour
{
    public float m_durtion = 0.2f;

    public AnimationCurve m_curve = AnimationCurve.Linear(0, 0, 1, 1);

    public float m_maxIntensity = 1;
    public string m_shaderName = "_RimPower";

    private Renderer[] m_renderers;


    [Label] public bool m_isPlaying = false;
    [Label] public float m_currentTime = 0;
    [Label] public float m_currentProgress = 0;

    public void OnInit(Renderer[] renderers)
    {
        m_currentTime = 0;
        m_renderers = renderers;
    }

    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
        if (m_isPlaying)
        {
            m_currentTime += deltaTime;
            if (m_currentTime >= m_durtion)
            {
                m_currentTime = m_durtion;
                m_isPlaying = false;
            }

            m_currentProgress = m_currentTime / m_durtion;
            float timeValue = m_curve.Evaluate(m_currentProgress);
            float value = timeValue * m_maxIntensity;
            if (m_renderers != null)
            {
                SetRimPower(value);
            }
        }
    }

    public void OnDeInit()
    {
    }

    /// <summary>
    /// 播放
    /// </summary>
    /// <param name="progress">进度</param>
    public void Play(float progress = 0)
    {
        m_currentTime = progress * m_durtion;
        m_isPlaying = true;
        SetRimPower(0);
    }

    /// <summary>
    /// 停止
    /// </summary>
    public void Stop()
    {
        m_isPlaying = false;
    }

    /// <summary>
    /// 赋值
    /// </summary>
    /// <param name="value"></param>
    public void SetRimPower(float value)
    {
        if (m_renderers == null) return;
        MaterialPropertyBlock m_rimPower = new MaterialPropertyBlock();
        m_rimPower.SetFloat(m_shaderName, value);
        for (int i = 0; i < m_renderers.Length; i++)
        {
            m_renderers[i].SetPropertyBlock(m_rimPower);
        }
    }
}