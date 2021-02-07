//----------------------------------------------------------------------
//
//              Maggic @  2020/8/14 19:18:56
//
//---------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LocalModels;
/// <summary>
/// 
/// </summary>
public class TableManager : MonoBehaviour
{
    public LocalModelManager Manager { get; private set; }
    /// <summary>
    /// 实例化
    /// </summary>
    public void OnInitialize()
    {
        Manager = new LocalModelManager();
        Manager.InitialiseLocalModels();
    }
}
