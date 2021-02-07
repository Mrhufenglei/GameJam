//======================================================================== 
//	
// 	 Maggic @ 2020/2/24 18:33:59　　　　　　　
// 	
//========================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RayColliderLisen : MonoBehaviour
{
    public class RayLisen : UnityEvent<GameObject> { }

    public RayLisen OnRay = new RayLisen();

    public static RayColliderLisen Get(GameObject obj)
    {
        if (obj == null) return null;
        RayColliderLisen listen = obj.GetComponent<RayColliderLisen>();
        if (listen == null) listen = obj.AddComponent<RayColliderLisen>();
        return listen;
    }
    public static RayColliderLisen GetRayColliderLisen(GameObject obj)
    {
        if (obj == null) return null;
        RayColliderLisen listen = obj.GetComponent<RayColliderLisen>();
        return listen;
    }
    public void RayInvoke()
    {
        if (OnRay != null)
        {
            OnRay.Invoke(this.gameObject);
        }
    }
}
