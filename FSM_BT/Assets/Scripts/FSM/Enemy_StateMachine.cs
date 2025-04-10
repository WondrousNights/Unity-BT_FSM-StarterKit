using UnityEngine;
using UnityEngine.AI;

public class Enemy_StateMachine : MonoBehaviour
{

    //Components
    public NavMeshAgent agent;

    //States
    Enemy_BaseState currentState;
    public Enemy_WanderState wanderState = new Enemy_WanderState();
    public Enemy_ChaseState chaseState = new Enemy_ChaseState();
    public Enemy_FleeState fleeState = new Enemy_FleeState();
    
    //Behavior Settings
    public float wanderRadius;
    public float chaseRadius;
    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();


        currentState = wanderState;
        currentState.EnterState(this);
    }

    public void SwitchState(Enemy_BaseState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

   
}
