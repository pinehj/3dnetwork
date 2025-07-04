using System.Collections.Generic;
using UnityEngine;

public class BearStateMachine : AStateMachine<Bear>
{
    protected override void InitDict()
    {
        _stateDict = new Dictionary<EState, AState<Bear>>
        {
            {EState.Idle, new BearIdleState()},
            {EState.Patrol, new BearPatrolState()},
            {EState.Chase, new BearChaseState()},
            {EState.Attack, new BearAttackState()},
            {EState.Die, new BearDieState() }
        };
    }

    public override void Execute()
    {
        base.Execute();
    }
}
