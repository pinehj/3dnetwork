using Photon.Pun;
using System;
using UnityEngine;

public class PlayerHealth : PlayerAbility, IDamageable, IPunObservable
{
    public event Action<float> OnDataChanged;
    [SerializeField] private float _currentHealth;
    public float CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = Mathf.Clamp(value, 0, _owner.Stat.MaxHealth);
            OnDataChanged?.Invoke(_currentHealth / _owner.Stat.MaxHealth);


            if (_currentHealth != 0 || _owner.State.IsDead)
            {
                return;
            }
            Die();
            RoomManager.Instance.OnPlayerDeath(_owner.PhotonView.Owner.ActorNumber, _lastAttackerNumber);
            _lastAttackerNumber = -1;

            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }
            for (int i = 0; i < 2; ++i)
            {
                ItemObjectFactory.Instance.RequestCreate(EItemType.Score, _owner.transform.position);
            }

        }
    }
    private int _lastAttackerNumber = -1;

    private void Start()
    {
        if (!_owner.PhotonView.IsMine)
        {
            return;
        }
        CurrentHealth = _owner.Stat.MaxHealth;
    }

    [PunRPC]
    public void TakeDamage(float damage, int actorNumber)
    {
        if (_owner.State.IsDead )
        {
            return;
        }
        _owner.GetAbility<PlayerShake>().StartCoroutine(nameof(PlayerShake.ShakeRoutine));
        _lastAttackerNumber = actorNumber;

        if (!_owner.PhotonView.IsMine)
        {
            return;
        }
        CurrentHealth -= damage;
        CameraManager.Instance.StartCoroutine("ShakeCamera");
    }

    public void Die()
    {
        _owner.State.IsDead = true;
        _owner.Animator.SetTrigger("Die");

        if (!_owner.PhotonView.IsMine)
        {
            return;
        }
        GameManager.Instance.StartCoroutine("RespawnPlayer", _owner);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_currentHealth);
        }
        else if (stream.IsReading)
        {
            Debug.Log("reading");
            CurrentHealth = (float)stream.ReceiveNext();
        }
    }
}
