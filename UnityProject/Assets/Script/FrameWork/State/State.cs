//*************************************************************
//
//          Maggic@2015.6.2
//
//*************************************************************
//



public abstract class State
{
    /// <summary>
    /// 获取状态名称
    /// </summary>
    /// <returns></returns>
    public abstract int GetName();
    /// <summary>
    /// 进入状态
    /// </summary>
    public abstract void OnEnter();
    /// <summary>
    /// Update
    /// </summary>
    /// <param name="deltaTime"></param>
    /// <param name="unscaledDeltaTime"></param>
    public abstract void OnUpdate(float deltaTime, float unscaledDeltaTime);
    /// <summary>
    /// 离开状态
    /// </summary>
    public abstract void OnExit();
  
    public abstract void RegisterEvents(EventSystemManager manager);

    public abstract void UnRegisterEvents(EventSystemManager manager);
}
