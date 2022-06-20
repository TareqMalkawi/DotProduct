using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    private Quaternion turretCurrentRot;
    
    const float maxCooldownTimer = 2.0f;
    float cooldownTimer = maxCooldownTimer;
   
    Transform updatedRot;

    private void Start()
    {
        Quaternion turretCurrentRot = transform.rotation;
    }
    
    // void LookAtTarget()
    // {
    //     // AI agent forward vector
    //     Vector3 AIForward = transform.forward;
    //     Debug.DrawRay(transform.position,AIForward,Color.red);
    //     Vector3 direction = target.position - transform.position;
    //     Debug.DrawRay(transform.position,direction,Color.green);
    //     direction.Normalize();
    //   
    //     float scalarVec = Vector3.Dot(AIForward,direction);
    //
    //     float desiredAngle = Mathf.Acos(scalarVec / (AIForward.magnitude * direction.magnitude));
    //
    //     Vector3 c = Vector3.Cross(AIForward, direction);
    //     float y = c.y;
    //     float a = 0.0f;
    //     if (y > 0) // anti clockwise
    //     {
    //         a = desiredAngle;
    //     }
    //     else if (y < 0) // clockwise
    //     {
    //         a = 2 * Mathf.PI - desiredAngle;
    //     }
    //
    //     Vector3 up = new Vector3(0.0f, c.y, 0.0f);
    //     up.Normalize();
    //   
    //     if (Mathf.Abs(a * Mathf.Rad2Deg) < cutOffAngle)
    //     {
    //         if (target.GetComponent<ThirdPersonController>().GetPlayerSpeed > 0.5f)
    //         {
    //         
    //         }
    //     }
    // }
}
