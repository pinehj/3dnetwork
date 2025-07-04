using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;

public class BearAttackState : AState<Bear>
{
    private Vector3 _velocity;

    public override void Enter()
    {
        _stateMachine.Owner.Agent.ResetPath();


        //포톤
        _stateMachine.Owner.PhotonView.RPC(nameof(_stateMachine.Owner.SetAttackTrigger), Photon.Pun.RpcTarget.All);
        _stateMachine.Owner.AttackTimer = 1 / _stateMachine.Owner.Stat.AttackSpeed;
        _stateMachine.Owner.IsAttacking = true;
    }

    public override void Execute()
    {
        if (_stateMachine.Owner.CurrentHealth <= 0)
        {
            _stateMachine.ChangeState(EState.Die);
            return;
        }

        if (_stateMachine.Owner.Target == null || !_stateMachine.Owner.Target.CanDamage()
            || (_stateMachine.Owner.Target.GetPos() - _stateMachine.Owner.transform.position).sqrMagnitude > Mathf.Pow(_stateMachine.Owner.Stat.AttakDistance,2))
        {
            if (_stateMachine.Owner.IsAttacking)
            {
                return;
            }
            _stateMachine.ChangeState(EState.Idle);
            return;
        }

        Quaternion targetRot = Quaternion.LookRotation((_stateMachine.Owner.Target.GetPos() - _stateMachine.Owner.transform.position));
        _stateMachine.Owner.transform.rotation = Quaternion.Slerp(_stateMachine.Owner.transform.rotation, targetRot, Time.deltaTime * 10f);
        _stateMachine.Owner.transform.forward = Vector3.SmoothDamp(_stateMachine.Owner.transform.forward, (_stateMachine.Owner.Target.GetPos() - _stateMachine.Owner.transform.position).normalized, ref _velocity, 0.02f);
        if(_stateMachine.Owner.AttackTimer <= 0)
        {
            _stateMachine.Owner.PhotonView.RPC(nameof(_stateMachine.Owner.SetAttackTrigger), Photon.Pun.RpcTarget.All);
            _stateMachine.Owner.AttackTimer = 1 / _stateMachine.Owner.Stat.AttackSpeed;
            _stateMachine.Owner.IsAttacking = true;
        }
    }

    public override void Exit()
    {
    }
}
