//----------------------------------------------------------------------
//
//              Maggic @  2020/8/6 14:20:22
//
//---------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

/// <summary>
/// 
/// </summary>
[System.Serializable]
public class AssetReferenceScriptableObject : AssetReferenceT<ScriptableObject>
{
    public AssetReferenceScriptableObject(string guid) : base(guid)
    {

    }
}
