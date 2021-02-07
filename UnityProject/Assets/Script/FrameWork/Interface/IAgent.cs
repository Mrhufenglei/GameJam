//======================================================================== 
//	
// 	 Maggic @ 2019/10/14 16:05:40 　　　　　　　　
// 	
//========================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public interface IAgent<T> where T : ITask
{
    T CurrentTask();
    void AddTask(T task);
    void OnUpdate(float deltaTime, float unscaledDeltaTime);
    void RemoveTask();
}
