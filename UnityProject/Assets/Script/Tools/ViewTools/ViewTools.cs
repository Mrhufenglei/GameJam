//*************************************************************
//
//          Maggic@2015.6.2
//
//*************************************************************
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewTools
{
    /// <summary>
    /// 获取rootGameObject下所有gameobject,重名将被最后识别到的覆盖
    /// </summary>
    /// <param name="rootGameObject"></param>
    /// <returns></returns>
    public static Dictionary<string, GameObject> CollectAllGameObjects(GameObject rootGameObject)
    {
        Dictionary<string, GameObject> result = new Dictionary<string, GameObject>();
        CollectAllGameObject(result, rootGameObject);
        return result;
    }

    static void CollectAllGameObject(Dictionary<string, GameObject> objectMap, GameObject gameObject)
    {
        if (objectMap.ContainsKey(gameObject.name))
        {
            objectMap[gameObject.name] = gameObject;
        }
        else
        {
            objectMap.Add(gameObject.name, gameObject);
        }

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            CollectAllGameObject(objectMap, gameObject.transform.GetChild(i).gameObject);
        }
    }
}
