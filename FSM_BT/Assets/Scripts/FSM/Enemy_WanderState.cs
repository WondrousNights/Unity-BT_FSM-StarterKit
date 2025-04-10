using UnityEngine;

public class Enemy_WanderState : Enemy_BaseState
{

    Vector3 patrolPoint;
    public override void EnterState(Enemy_StateMachine parent)
    {
        patrolPoint = Utilities.FindRandomPointInsideUnitSphere(parent.transform.position, parent.wanderRadius);

        parent.agent.SetDestination(patrolPoint);

        //Debug.Log("Entered Wander State");
    }

    public override void ExitState(Enemy_StateMachine parent)
    {
        patrolPoint = Vector3.zero;
    }

    public override void UpdateState(Enemy_StateMachine parent)
    {
        //Finds a random point on navmesh within a radius and sets its position to it
        float distanceToWander = Vector3.Distance(parent.transform.position, patrolPoint);
        
        if(distanceToWander < 1)
        {
            patrolPoint = Utilities.FindRandomPointInsideUnitSphere(parent.transform.position, parent.wanderRadius);
            parent.agent.SetDestination(patrolPoint);
        }

        float distanceToPlayer = Vector3.Distance(parent.transform.position, Player.Instance.transform.position);

        if(distanceToPlayer < parent.chaseRadius)
        {
            parent.SwitchState(parent.chaseState);
        }
    }

    
}
