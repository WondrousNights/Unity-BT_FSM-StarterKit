using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Flee", story: "[Agent] flees from [Player]", category: "Action", id: "992a2432cc11371173dca8f939748c34")]
public partial class FleeAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Player;

    public float fleeDistance = 5f;
    Vector3 fleeTarget;
    UnityEngine.AI.NavMeshAgent navMeshAgent;

    protected override Status OnStart()
    {

        navMeshAgent = Agent.Value.GetComponent<UnityEngine.AI.NavMeshAgent>();
        Vector3 fleeDir = (Agent.Value.transform.position - Player.Value.transform.position).normalized;
        fleeTarget = Agent.Value.transform.position + fleeDir * fleeDistance;

        if (UnityEngine.AI.NavMesh.SamplePosition(fleeTarget, out UnityEngine.AI.NavMeshHit hit, 2f, UnityEngine.AI.NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position);
        }
        else
        {
            return Status.Failure;
        }

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if(Vector3.Distance(Agent.Value.transform.position, fleeTarget) < 0.5f) return Status.Success;
        else {return Status.Failure; }
    }

    protected override void OnEnd()
    {
    }
}

