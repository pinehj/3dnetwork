using UnityEngine;

public class BearIdleState : BearBaseState
{
    private float _idleTimer;
    public override void Enter()
    {
        _stateMachine.Owner.Agent.ResetPath();
        _stateMachine.Owner.Target = null;
        _idleTimer = _stateMachine.Owner.Stat.IdleTimeDuration;
    }

    public override void Execute()
    {
        base.Execute();
        if(_idleTimer <= 0)
        {
            _stateMachine.ChangeState(EState.Patrol);
            return;
        }
        _idleTimer -= Time.deltaTime;
    }

    public override void Exit()
    {
    }
}
