using UnityEngine;

public class BearBaseState : AState<Bear>
{
    protected float _findTargetTimer;
    public override void Enter()
    {
        _findTargetTimer = 0;
    }

    public override void Execute()
    {
        if (_stateMachine.Owner.CurrentHealth <= 0)
        {
            _stateMachine.ChangeState(EState.Die);
            return;
        }

        if (_findTargetTimer <= 0)
        {
            _findTargetTimer = _stateMachine.Owner.Stat.FindTargetDuration;

            Collider minDistanceTarget = null;
            float minDistance;
            if (_stateMachine.Owner.Target != null && _stateMachine.Owner)
            {
                minDistance = (_stateMachine.Owner.Target.GetPos() - _stateMachine.Owner.transform.position).sqrMagnitude;
            }
            else
            {
                minDistance = 99999;
            }

            Collider[] targetCandidates = Physics.OverlapSphere(_stateMachine.Owner.transform.position, _stateMachine.Owner.Stat.FindTargetRad, _stateMachine.Owner.TargetLayer);
            foreach (Collider targetCandidate in targetCandidates)
            {
                float distance = (targetCandidate.transform.position - _stateMachine.Owner.transform.position).sqrMagnitude;
                if ((targetCandidate.transform.position - _stateMachine.Owner.transform.position).sqrMagnitude < minDistance)
                {
                    minDistance = distance;
                    minDistanceTarget = targetCandidate;
                }
            }
            if (minDistanceTarget != null)
            {
                _stateMachine.Owner.Target = minDistanceTarget.GetComponent<IDamageable>();
                _stateMachine.ChangeState(EState.Chase);
                return;
            }

        }
        else
        {
            _findTargetTimer -= Time.deltaTime;
        }
    }

    public override void Exit()
    {
    }
}
