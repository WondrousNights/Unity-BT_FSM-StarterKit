using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Wander", story: "[Agent] wanders within [Radius]", category: "Action", id: "3f40564d8c4933d710dd012f6c67346e")]
public partial class WanderAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<float> Radius;
    Vector3 patrolPoint;
    UnityEngine.AI.NavMeshAgent agent;
    protected override Status OnStart()
    {
        agent = Agent.Value.GetComponent<UnityEngine.AI.NavMeshAgent>();

        patrolPoint = Utilities.FindRandomPointInsideUnitSphere(Agent.Value.transform.position, Radius.Value);
        agent.SetDestination(patrolPoint);

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if(Vector3.Distance(Agent.Value.transform.position, patrolPoint) < 1f)
        {
            return Status.Success;
        }
        else
        {
            return Status.Running;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

