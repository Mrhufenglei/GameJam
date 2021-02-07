using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAgent : MonoBehaviour, IAgent<SceneTask>
{
    public enum State
    {
        Wait,
        Begin,
        Run,
        Complete
    }

    private SceneTask m_task;
    private AsyncOperation m_async;

    [Label]
    public State m_state = State.Wait;

    public SceneTask CurrentTask()
    {
        return m_task;
    }

    public void AddTask(SceneTask task)
    {
        m_task = task;
        if (m_task.style == SceneTask.Style.Load)
        {
            m_async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(m_task.name, m_task.loadMode);
        }
        else
        {
            m_async = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(m_task.name);
        }

        m_state = State.Begin;
        if (m_task.onBegin != null)
        {
            m_task.onBegin(m_task.name);
        }
    }

    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
        if (m_task != null && m_async != null)
        {
            m_state = State.Run;
            if (m_task.onProgress != null)
            {
                m_task.onProgress(m_task.name, m_async.progress);
            }
            if (m_async.isDone == true)
            {
                if (m_task.onComplete != null)
                {
                    m_task.onComplete(m_task.name);
                    m_state = State.Complete;
                }
            }
        }
    }

    public void RemoveTask()
    {
        m_task = null;
        m_async = null;
        m_state = State.Wait;
    }
}




