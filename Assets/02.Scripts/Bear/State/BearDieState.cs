using Photon.Pun;
using UnityEngine;

public class BearDieState : AState<Bear>
{
    public override void Enter()
    {
        _stateMachine.Owner.PhotonView.RPC(nameof(_stateMachine.Owner.SetDieTrigger), Photon.Pun.RpcTarget.All);
        _stateMachine.Owner.Agent.ResetPath();
        _stateMachine.Owner.Target = null;
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}
