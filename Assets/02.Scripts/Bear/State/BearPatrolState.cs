using UnityEngine;

public class BearPatrolState : BearBaseState
{
    private Transform _targetPatrolPoint;

    private float _distanceCheckDuration = 1;
    private float _distanceCheckTimer;
    public override void Enter()
    {
        _targetPatrolPoint = PatrolManager.Instance.GetRandomPatrolPoint();
        _stateMachine.Owner.Agent.SetDestination(_targetPatrolPoint.position);
        _stateMachine.Owner.Agent.speed = _stateMachine.Owner.Stat.MoveSpeed;

        _distanceCheckTimer = _distanceCheckDuration;
    }

    public override void Execute()
    {
        base.Execute();
        if(_distanceCheckTimer <= 0 && _stateMachine.Owner.Agent.remainingDistance < _stateMachine.Owner.Stat.PatorlDistance)
        {
            _stateMachine.ChangeState(EState.Idle);
            return;
        }
        _distanceCheckTimer -= Time.deltaTime;
    }

    public override void Exit()
    {
    }
}
