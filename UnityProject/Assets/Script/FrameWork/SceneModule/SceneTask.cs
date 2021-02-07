using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTask : ITask
{
    public delegate void Complete(string name);
    public delegate void Progress(string name, float progress);
    public delegate void Begin(string name);

    public enum Style
    {
        Load,
        UnLoad,
    }

    public string name;
    public UnityEngine.SceneManagement.LoadSceneMode loadMode;
    public Style style;
    public Complete onComplete;
    public Begin onBegin;
    public Progress onProgress;

    /// <summary>
    /// 卸载场景
    /// </summary>
    /// <param name="name"></param>
    /// <param name="style"></param>
    /// <param name="complete"></param>
    /// <param name="progress"></param>
    /// <param name="begin"></param>
    public SceneTask(string name, Complete complete, Progress progress = null, Begin begin = null)
    {
        this.name = name;
        loadMode = UnityEngine.SceneManagement.LoadSceneMode.Single;
        style = Style.UnLoad;
        onComplete = complete;
        onBegin = begin;
        onProgress = progress;
    }
    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="name"></param>
    /// <param name="mode"></param>
    /// <param name="complete"></param>
    /// <param name="progress"></param>
    /// <param name="begin"></param>
    public SceneTask(string name, UnityEngine.SceneManagement.LoadSceneMode mode, Complete complete, Progress progress = null, Begin begin = null)
    {
        this.name = name;
        loadMode = mode;
        style = Style.Load;
        onComplete = complete;
        onBegin = begin;
        onProgress = progress;
    }
}
