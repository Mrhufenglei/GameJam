using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesAgent : MonoBehaviour,IAgent<ResourcesTask>
{
    public enum State
    {
        Wait,
        Begin,
        Loading,
        Complete
    }
    private ResourcesTask m_task;
    private ResourceRequest m_resourceRequest;

    [Label]
    public State m_state = State.Wait;

    public ResourcesTask CurrentTask()
    {
        return m_task;
    }

    public void AddTask(ResourcesTask task)
    {
        m_task = task;
        m_resourceRequest = Resources.LoadAsync(m_task.path);
        m_state = State.Begin;
        if (m_task.onBegin != null)
        {
            m_task.onBegin(m_task.path);
        }
    }

    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
        if (m_task != null && m_resourceRequest != null)
        {
            m_state = State.Loading;
            if (m_task.onProgress != null)
            {
                m_task.onProgress(m_task.path, m_resourceRequest.progress);
            }
            if (m_resourceRequest.isDone == true)
            {
                if (m_task.onComplete != null)
                {
                    m_task.asset = m_resourceRequest.asset;
                    m_task.onComplete(m_task.path, m_task.asset);
                    m_state = State.Complete;
                }
            }
        }
    }

    public void RemoveTask()
    {
        m_task = null;
        m_resourceRequest = null;
        m_state = State.Wait;
    }

}


