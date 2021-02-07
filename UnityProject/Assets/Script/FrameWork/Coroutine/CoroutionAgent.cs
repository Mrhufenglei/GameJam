//======================================================================== 
//	
// 	 Maggic @ 2019/10/14 11:08:01 　　　　　　　　
// 	
//========================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class CoroutionAgent : MonoBehaviour
{
    public void AddTask(IEnumerator routine)
    {
        StartCoroutine(routine);
    }
    public void RemoveTask(IEnumerator routine)
    {
        StopCoroutine(routine);
    }
    public void RemoveAllTask()
    {
        StopAllCoroutines();
    }
}
