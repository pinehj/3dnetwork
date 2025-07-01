using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerAttack : PlayerAbility
{
    [SerializeField] private Collider _weaponCollider;
    [SerializeField] GameObject HitEffect;
    private float _attackTimer = 0;

    private void Start()    
    {
        DeactiveCollider();
    }

    // - '위치/회전' 처럼 상시로 확인이 필요한 데이터 동기화: IPunObservable(OnPhotonSerializeView)
    // - '트리거/공격/피격' 처럼 간헐적으로 특정한 이벤트가 발생했을때의 변화된 데이터 동기화: RPC
    //    RPC: Remote Procedure Call
    //     ㄴ 물리적으로 떨어져 있는 다른 디바이스의 함수를 호출하는 기능
    //     ㄴ 
    void Update()
    {
        if (!_owner.PhotonView.IsMine || _owner.State.IsDead)
        {
            return;
        }

        if (_attackTimer <= 0 && Input.GetMouseButton(0) && _owner.GetAbility<PlayerStamina>().TryConsumeStamina(_owner.Stat.AttackStaminaCost))
        {
            _attackTimer = 1 / _owner.Stat.AttackSpeed;

            _owner.PhotonView.RPC(nameof(PlayAttackAnimation), RpcTarget.All, Random.Range(1,4));
        }
        else
        {
            _attackTimer -= Time.deltaTime;
        }
    }

    [PunRPC]
    private void PlayAttackAnimation(int randomNumber)
    {
        _owner.Animator.SetTrigger($"Attack{randomNumber}");
    }

    //For Anim
    public void ActiveCollider()
    {
        _weaponCollider.enabled = true;
    }
    public void DeactiveCollider()
    {
        _weaponCollider.enabled = false;
    }

    public void Hit(Collider other)
    {
        Instantiate(HitEffect, other.ClosestPoint(_owner.transform.position), Quaternion.identity);
        if (!_owner.PhotonView.IsMine)
        {
            return;
        }
        DeactiveCollider();

        PhotonView otherPhotonView = other.GetComponent<PhotonView>();
        otherPhotonView.RPC(nameof(PlayerHealth.TakeDamage), RpcTarget.All, _owner.Stat.AttackPower);
    }
}
