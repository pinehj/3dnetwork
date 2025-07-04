using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class BearChaseState : BearBaseState
{
    private float _distanceCheckDuration = .2f;
    private float _distanceCheckTimer;
    private Vector3 _velocity;
    public override void Enter()
    {
        if (_stateMachine.Owner.Target == null || !_stateMachine.Owner.Target.CanDamage())
        {
            _stateMachine.ChangeState(EState.Idle);
            return;
        }
        _stateMachine.Owner.Agent.speed = _stateMachine.Owner.Stat.ChaseSpeed;

        _distanceCheckTimer = _distanceCheckDuration;
    }

    public override void Execute()
    {
        base.Execute();

        if (_stateMachine.Owner.Target == null || !_stateMachine.Owner.Target.CanDamage())
        {
            _stateMachine.ChangeState(EState.Idle);
            return;
        }

        Quaternion targetRot = Quaternion.LookRotation((_stateMachine.Owner.Target.GetPos() - _stateMachine.Owner.transform.position));
        _stateMachine.Owner.transform.rotation = Quaternion.Slerp(_stateMachine.Owner.transform.rotation, targetRot, Time.deltaTime * 10f);

        if (_distanceCheckTimer <= 0 && _stateMachine.Owner.AttackTimer <= 0 &&(_stateMachine.Owner.Target.GetPos() - _stateMachine.Owner.transform.position).sqrMagnitude <= Mathf.Pow(_stateMachine.Owner.Stat.AttakDistance, 2))
        {
            _stateMachine.ChangeState(EState.Attack);
            return;
        }
        _stateMachine.Owner.Agent.SetDestination(_stateMachine.Owner.Target.GetPos());

        _distanceCheckTimer -= Time.deltaTime;
    }

    public override void Exit()
    {
    }
}
