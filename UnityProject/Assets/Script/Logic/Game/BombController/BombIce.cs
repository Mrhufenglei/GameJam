//-----------------------------------------------------------------
//
//              Maggic @  2021-02-08 15:11:13
//
//----------------------------------------------------------------

using UnityEngine;

/// <summary>
/// 
/// </summary>
public class BombIce : BombBase
{
    private GameObject m_bombObj;

    protected override void OnBombInit(GameObject obj)
    {
        base.OnBombInit(obj);
        m_bombObj = obj;
        ECdestroyMe des =  obj.GetComponent<ECdestroyMe>();
        if (des != null)
        {
            des.deathtimer = m_destroyDuration;
        }
    }

    protected override void OnBombUpdate(float deltaTime, float unscaledDeltaTime)
    {
        base.OnBombUpdate(deltaTime, unscaledDeltaTime);
    }
}