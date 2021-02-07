using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public SourceAgent[] m_agents;
    public void PlaySound(int index, AudioClip clip)
    {
        if (clip == null) return;
        SourceAgent _node = GetSourceAgent(index);
        _node.m_audioSource.clip = clip;
        _node.m_audioSource.Play();
    }

    public void StopSound(int index)
    {
        SourceAgent _node = GetSourceAgent(index);
        _node.m_audioSource.Stop();
    }

    public void PauseSound(int index)
    {
        SourceAgent _node = GetSourceAgent(index);
        _node.m_audioSource.Pause();
    }
    public void UnPauseSound(int index)
    {
        SourceAgent _node = GetSourceAgent(index);
        _node.m_audioSource.UnPause();
    }

    public SourceAgent GetSourceAgent(int index)
    {
        return m_agents[index];
    }

}
