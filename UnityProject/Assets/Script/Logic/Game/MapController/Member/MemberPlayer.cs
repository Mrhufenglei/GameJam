//-----------------------------------------------------------------
//
//              Maggic @  2021-02-07 15:43:01
//
//----------------------------------------------------------------

using UnityEngine;

/// <summary>
/// 
/// </summary>
public class MemberPlayer : BaseMember
{
    public override void OnControllerColliderHit(ControllerColliderHit collider)
    {
        base.OnControllerColliderHit(collider);

        if (collider.gameObject.layer == 10)
        {
            Debug.LogFormat("碰到人怪物了 {0}", collider.gameObject.name);
            // CharacterController rigidbody = collider.gameObject.GetComponent<CharacterController>();
            // if (rigidbody != null)
            // {
            //     Vector3 dir = transform.position - collider.transform.position;
            //     float force = 10;
            //     dir = dir.normalized * force;
            //     rigidbody.v
            //     rigidbody.AddForce(dir, ForceMode.Force);
            // }
            return;
        }

        if (collider.gameObject.layer == 11)
        {
            Debug.LogFormat("碰到炸弹了 {0}", collider.gameObject.name);
            Rigidbody rigidbody = collider.gameObject.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                Vector3 dir = collider.transform.position - transform.position;
                float force = 2f;
                dir = dir.normalized * force;
                rigidbody.AddForce(dir, ForceMode.Impulse);
            }

            return;
        }
    }
}