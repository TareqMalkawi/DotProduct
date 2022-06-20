using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AILineOfSight : MonoBehaviour
{
   // refernces 
   [SerializeField] private Transform target;
   private Animator animator;
   private NavMeshAgent navMeshAgent;

   // how far the NBC has to stay from target when he suddenly stops
   // could be anything I simply initilaize to the agent stopping distance
   [SerializeField] private float stoppingDist;

   // how wide the NBC LineOfSight
   // remember that any value greter than 90.0 degrees means that the NBC can detect what behinds him
   [SerializeField] private float cutOffAngle = 45.0f;
   
   // AI agent forward vector
   private Vector3 AIForward;
   
   private Vector3 direction;
   
   [SerializeField] private Text resultText;
   private void Start()
   {
      animator = gameObject.GetComponent<Animator>();
      navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
      stoppingDist = navMeshAgent.stoppingDistance;
   }

   private void FixedUpdate()
   {
      SimpleNBCBehaviour();
   }

   private void LateUpdate()
   {
      UpdateUIBasedOnDotProductResult();
   }

   /* if target is in the range of the angle that was defined by the dot product (in this case infront of the NBC)
    * then the NBC can detect the target and do something
    * in this case simply , run towrads target then attack him 
    */
   private void SimpleNBCBehaviour()
   {
      if (IsTargetInFrontOfMe())
      {
         animator.SetBool("Run",true);
         navMeshAgent.SetDestination(target.position);
         
         if (navMeshAgent.remainingDistance < stoppingDist)
         {
            animator.SetBool("Run",false);
         
            // Debug.Log("Attack Target !");
            
            animator.SetTrigger("Attack");
         }
      }
      else
      {
         animator.SetBool("Run",false);
      }
   }


    // calculate the dot product between AI agent forward vector and the direction ,
    // where the direction is the distance from the target to the NBC
   private float GetDotProductResult()
   {
      // AI agent forward vector
      AIForward = transform.forward;
      
      Debug.DrawRay(transform.position,AIForward,Color.yellow);
      
      direction = target.position - transform.position;
      direction.Normalize();
      
      Debug.DrawRay(transform.position,direction,Color.green);
      
      return Vector3.Dot(AIForward,direction);
   }
   
   
    /* find the desired angle from the NBC forward vector to the direction from the target to the NBC ,
     * since the value returned from using (Mathf.Acos) is in radian we have to multiply it by 180/PI ,
     * or we can simply use (Mathf.Rad2Deg)
     */
   bool IsTargetInFrontOfMe()
   {
      float desiredAngle = Mathf.Acos(GetDotProductResult() / (AIForward.magnitude * direction.magnitude));
      
      desiredAngle *= Mathf.Rad2Deg;
      
      // Debug.Log("dot product result  = " + GetDotProductResult() + "  angle  = " + desiredAngle);

      if (Mathf.Abs(desiredAngle) < cutOffAngle)
         return true;

      return false;
   }
   
   private void UpdateUIBasedOnDotProductResult() => resultText.text = "Dot Product result: " + Mathf.RoundToInt(GetDotProductResult());
}