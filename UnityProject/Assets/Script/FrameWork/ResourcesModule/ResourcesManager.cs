using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    private Dictionary<string, Object> m_datas = new Dictionary<string, Object>();
    public ResourcesAgent[] m_agents = new ResourcesAgent[3];
    public Queue<ResourcesTask> m_tasks = new Queue<ResourcesTask>(4096);

    #region Methods load
    /// <summary>
    ///  加载Base 资源
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="path">路径</param>
    /// <returns>内存</returns>
    public T Load<T>(string path) where T : UnityEngine.Object
    {
        Object obj = null;
        if (!m_datas.TryGetValue(path, out obj) || obj == null)
        {
            obj = Resources.Load(path);
            m_datas[path] = obj;
        }
        return obj as T;
    }
    /// <summary>
    /// 异步加载
    /// </summary>
    /// <param name="tasks"></param>
    public void LoadAsync(params ResourcesTask[] tasks)
    {
        for (int i = 0; i < tasks.Length; i++)
        {
            m_tasks.Enqueue(tasks[i]);
        }
    }

    #endregion

    #region Methods Unload
    /// <summary>
    /// 卸载资源
    /// </summary>
    /// <param name="paths"></param>
    public void Unload(params string[] paths)
    {
        for (int i = 0; i < paths.Length; i++)
        {
            m_datas.Remove(paths[i]);
        }
    }
    /// <summary>
    /// 卸载无用资源
    /// </summary>
    public void UnloadUnusedAssets()
    {
        Resources.UnloadUnusedAssets();
    }
    #endregion

    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
        if (m_agents.Length > 0)
        {
            for (int i = 0; i < m_agents.Length; i++)
            {
                if (m_agents[i].m_state == ResourcesAgent.State.Begin || m_agents[i].m_state == ResourcesAgent.State.Loading)
                {
                    m_agents[i].OnUpdate(deltaTime, unscaledDeltaTime);
                }
                if (m_agents[i].m_state == ResourcesAgent.State.Complete)
                {
                    //记录相关数据
                    ResourcesTask task = m_agents[i].CurrentTask();
                    if (task != null)
                    {
                        m_datas[task.path] = task.asset;
                    }
                    //重置状态
                    m_agents[i].RemoveTask();
                }
            }
        }


        if (m_tasks.Count > 0 && m_agents.Length > 0)
        {
            for (int i = 0; i < m_agents.Length; i++)
            {
                if (m_agents[i].m_state == ResourcesAgent.State.Wait)
                {
                    ResourcesTask task = GetNextTask();
                    if (task != null)
                    {
                        m_agents[i].AddTask(task);
                    }
                }
            }
        }

    }

    private ResourcesTask GetNextTask()
    {
        if (m_tasks.Count == 0) return null;
        ResourcesTask task = m_tasks.Dequeue();
        Object obj = null;
        //是否包含
        if (m_datas.TryGetValue(task.path, out obj) && obj != null)
        {
            task.asset = obj;
            if (task.onBegin != null) { task.onBegin(task.path); }
            if (task.onProgress != null) { task.onProgress(task.path, 1); }
            if (task.onComplete != null) { task.onComplete(task.path, obj); }
            //递归下一个
            task = GetNextTask();
        }
        return task;
    }
}
