using NUnit.Framework;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PhotonView))]

public class Bear : AStateMachineOwner<Bear>, IDamageable, IPunObservable
{
    private Animator _animator;
    public Animator Animator => _animator;
    private NavMeshAgent _agent;
    public NavMeshAgent Agent => _agent;
    private PhotonView _photonView;
    public PhotonView PhotonView => _photonView;

    [SerializeField] private BearStat _stat;
    public BearStat Stat => _stat;

    public IDamageable Target;
    public LayerMask TargetLayer;
    public float AttackTimer { get; set; }
    public bool IsAttacking { get; set; }
    [SerializeField] private Collider _attackCollider;

    private float _currentHealth;
    public float CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = Mathf.Clamp(value, 0, Stat.MaxHealth);
            UpdateHealthSlider(CurrentHealth / Stat.MaxHealth);
        }
    }
    public Slider HealthSlider;

    private Vector3 _targetVelocity;
    protected override void Awake()
    {
        _stat = new BearStat();
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _photonView = GetComponent<PhotonView>();
        base.Awake();
    }

    private void Start()
    {
        CurrentHealth = Stat.MaxHealth;
    }
    protected override void Update()
    {
        if (!PhotonView.IsMine)
        {
            return;
        }
        base.Update();
        Animator.SetFloat("Speed", Agent.speed);
        _targetVelocity = Vector3.Lerp(_targetVelocity, Agent.velocity.normalized, Time.deltaTime * 10f);
        Animator.SetFloat("Move", _targetVelocity.magnitude);
        AttackTimer -= Time.deltaTime;
    }
    public override void InitStateMachine()
    {
        _stateMachine = new BearStateMachine();
    }
    public void ActiveCollider()
    {
        _attackCollider.enabled = true;
    }
    public void DeactiveCollider()
    {
        _attackCollider.enabled = false;
    }

    public void EndAttack()
    {
        IsAttacking = false;
    }
    public void Hit(Collider other)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        DeactiveCollider();

        PhotonView otherPhotonView = other.GetComponent<PhotonView>();
        otherPhotonView.RPC(nameof(IDamageable.TakeDamage), RpcTarget.All, Stat.AttackPower, -999);
    }

    [PunRPC]
    public void TakeDamage(float damage, int actorNumber)
    {
        if (!PhotonView.IsMine || CurrentHealth <= 0)
        {
            return;
        }
        CurrentHealth -= damage;
    }
    private void UpdateHealthSlider(float value)
    {
        HealthSlider.value = value;
    }
    public bool CanDamage()
    {
        return true;
    }

    public Vector3 GetPos()
    {
        return transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _stat.AttakDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _stat.FindTargetRad);
    }

    [PunRPC]
    public void SetAttackTrigger()
    {
        Animator.SetTrigger("Attack");
    }
    [PunRPC]
    public void SetDieTrigger()
    {
        Animator.SetTrigger("Die");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(CurrentHealth);
        }
        else if (stream.IsReading)
        {
            CurrentHealth = (float)stream.ReceiveNext();
        }
    }

    public void Die()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(_stateMachine.Owner.PhotonView);
        }
    }
}
