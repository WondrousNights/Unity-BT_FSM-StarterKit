using UnityEngine;

public abstract class Enemy_BaseState
{
    public abstract void EnterState(Enemy_StateMachine parent);
    public abstract void UpdateState(Enemy_StateMachine parent);
    public abstract void ExitState(Enemy_StateMachine parent);
}
