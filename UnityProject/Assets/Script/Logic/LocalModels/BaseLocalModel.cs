//----------------------------------------------------------------------
//
//              Maggic @  2020/8/14 14:21:08
//
//---------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace LocalModels
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseLocalModel
    {
        public abstract void Initialise(string name,byte[] bytes);
    }
}
