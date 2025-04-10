using System.Diagnostics;
using UnityEngine;

public class Enemy_ChaseState : Enemy_BaseState
{
    public float updateThreshold = 1.0f;
    private Vector3 lastTargetPosition;

    public override void EnterState(Enemy_StateMachine parent)
    {
       //Debug.Log("Chasing player");

       Vector3 playerPos = Player.Instance.transform.position;
       lastTargetPosition = playerPos;
       parent.agent.SetDestination(playerPos);
    }

    public override void ExitState(Enemy_StateMachine parent)
    {

    }

    public override void UpdateState(Enemy_StateMachine parent)
    {
        if(Player.Instance.isPoweredUp)
        {
            parent.SwitchState(parent.fleeState);
        }


        Vector3 playerPos = Player.Instance.transform.position;
        float distanceToPlayer = Vector3.Distance(parent.transform.position, playerPos);

        if(distanceToPlayer > parent.chaseRadius)
        {
            parent.SwitchState(parent.wanderState);
        }

        //If player has moved, set destination to new position
        float playerDistanceFromLastPosition = Vector3.Distance(playerPos, lastTargetPosition);
        if (playerDistanceFromLastPosition > updateThreshold)
        {
            lastTargetPosition = playerPos;
            parent.agent.SetDestination(playerPos);
        }

        

    }


}
