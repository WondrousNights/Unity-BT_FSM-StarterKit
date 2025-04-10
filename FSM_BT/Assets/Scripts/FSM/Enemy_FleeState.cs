using UnityEngine;
using UnityEngine.AI;

public class Enemy_FleeState : Enemy_BaseState
{
    public float fleeDistance = 5f;
    Vector3 fleeTarget;
    public override void EnterState(Enemy_StateMachine parent)
    {
        //Calculates direction away from player, finds a point on the navmesh from that direction, moves to it
        Vector3 fleeDir = (parent.transform.position - Player.Instance.transform.position).normalized;
        fleeTarget = parent.transform.position + fleeDir * fleeDistance;

        if (NavMesh.SamplePosition(fleeTarget, out NavMeshHit hit, 2f, NavMesh.AllAreas))
        {
            parent.agent.SetDestination(hit.position);
        }
    }

    public override void ExitState(Enemy_StateMachine parent)
    {
        //throw new System.NotImplementedException();
    }

    public override void UpdateState(Enemy_StateMachine parent)
    {
        if(Vector3.Distance(parent.transform.position, fleeTarget) < 1)
        {
            parent.SwitchState(parent.wanderState);
        }
    }

   
}
