//-----------------------------------------------------------------
//
//              Maggic @  2021-02-07 15:43:38
//
//----------------------------------------------------------------

using UnityEngine;

/// <summary>
/// 
/// </summary>
public class MemberEnemy : BaseMember
{
    public override void OnControllerColliderHit(ControllerColliderHit collider)
    {
        base.OnControllerColliderHit(collider);
      
        if (collider.gameObject.layer == 9)
        {
            Debug.LogFormat("碰到玩家了 {0}", collider.gameObject.name);
            return;
        }

        if (collider.gameObject.layer == 10)
        {
            Debug.LogFormat("碰到人怪物了 {0}", collider.gameObject.name);
            return;
        }

        if (collider.gameObject.layer == 11)
        {
            Debug.LogFormat("碰到炸弹了 {0}", collider.gameObject.name);
            return;
        }
    }
}