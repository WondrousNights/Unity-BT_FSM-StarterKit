using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "If Player Is Powered Up", story: "[Player] is PoweredUp", category: "Conditions", id: "adf0bc6ce03e611ec0bd77069953939c")]
public partial class IfPlayerIsPoweredUpCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Player;

    public override bool IsTrue()
    {
        if(Player.Value.GetComponent<Player>().isPoweredUp)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
