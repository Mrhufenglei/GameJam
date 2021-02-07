using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesTask : ITask
{
    public delegate void Complete(string path, Object obj);
    public delegate void Progress(string path, float task);
    public delegate void Begin(string path);

    public string path;
    public Object asset;
    public Complete onComplete;
    public Begin onBegin;
    public Progress onProgress;

    public ResourcesTask()
    {

    }

    public ResourcesTask(string path, Complete complete, Progress progress = null, Begin begin = null)
    {
        this.path = path;
        onComplete = complete;
        onBegin = begin;
        onProgress = progress;
    }
}

