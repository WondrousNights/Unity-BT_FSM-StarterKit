using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Chase", story: "[Agent] chases [Target]", category: "Action", id: "abd3399616b9d86237cb050eee0b18d9")]
public partial class ChaseAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Target;


    float updateThreshold = 1.0f;
    private Vector3 lastTargetPosition;

    NavMeshAgent navMeshAgent;
    protected override Status OnStart()
    {
        navMeshAgent = Agent.Value.gameObject.GetComponent<NavMeshAgent>();
        lastTargetPosition = Target.Value.gameObject.transform.position;
        navMeshAgent.SetDestination(lastTargetPosition);

        return Status.Running;
    }

    protected override Status OnUpdate()
    {

        Vector3 playerPos = Target.Value.transform.position;
        float playerDistanceFromLastPosition = Vector3.Distance(playerPos, lastTargetPosition);

        if (playerDistanceFromLastPosition > updateThreshold)
        {
            lastTargetPosition = playerPos;
            navMeshAgent.SetDestination(playerPos);
        }

        float distanceFromPlayer = Vector3.Distance(playerPos, Agent.Value.transform.position);

        if(distanceFromPlayer <= 0.5f)
        {
            return Status.Success;
        }
        if(distanceFromPlayer >= 7f)
        {
            return Status.Failure;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

