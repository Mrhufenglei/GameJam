using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SceneManager : MonoBehaviour
{
    public SceneAgent[] m_agents = new SceneAgent[3];
    private Queue<SceneTask> m_tasks = new Queue<SceneTask>(8);

    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="name">场景名称</param>
    /// <param name="mode">加载模式</param>
    /// <param name="complete">完成</param>
    /// <param name="progress">进度</param>
    /// <param name="begin">开始</param>
    public void LoadAsync(string name, UnityEngine.SceneManagement.LoadSceneMode mode, SceneTask.Complete complete, SceneTask.Progress progress = null, SceneTask.Begin begin = null)
    {
        SceneTask task = new SceneTask(name, mode, complete, progress, begin);
        m_tasks.Enqueue(task);
    }
    /// <summary>
    /// 卸载场景
    /// </summary>
    /// <param name="name">场景名称</param>
    /// <param name="complete">完成</param>
    /// <param name="progress">进度</param>
    /// <param name="begin">开始</param>
    public void UnLoadAsync(string name, SceneTask.Complete complete, SceneTask.Progress progress = null, SceneTask.Begin begin = null)
    {
        SceneTask task = new SceneTask(name, complete, progress, begin);
        m_tasks.Enqueue(task);
    }

    /// <summary>
    /// 帧调用
    /// </summary>
    /// <param name="deltaTime"></param>
    /// <param name="unscaledDeltaTime"></param>
    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
        if (m_agents.Length > 0)
        {
            for (int i = 0; i < m_agents.Length; i++)
            {
                if (m_agents[i].m_state == SceneAgent.State.Begin || m_agents[i].m_state == SceneAgent.State.Run)
                {
                    m_agents[i].OnUpdate(deltaTime, unscaledDeltaTime);
                }
                if (m_agents[i].m_state == SceneAgent.State.Complete)
                {
                    //记录相关数据

                    //重置状态
                    m_agents[i].RemoveTask();
                }
            }
        }

        if (m_tasks.Count > 0 && m_agents.Length > 0)
        {
            for (int i = 0; i < m_agents.Length; i++)
            {
                if (m_agents[i].m_state == SceneAgent.State.Wait)
                {
                    SceneTask task = GetNextTask();
                    if (task != null)
                    {
                        m_agents[i].AddTask(task);
                    }
                }
            }
        }
    }

    private SceneTask GetNextTask()
    {
        if (m_tasks.Count == 0) return null;
        SceneTask task = m_tasks.Dequeue();
        return task;
    }
}
