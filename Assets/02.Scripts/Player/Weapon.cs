using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private PlayerAttack _attack;
    private PhotonView _photonView;
    private void Awake()
    {
        _attack = GetComponentInParent<PlayerAttack>();
        _photonView = GetComponentInParent<PhotonView>();
    }

    private void Start()
    {
        ScoreManager.Instance.OnDataChanged += AdjustSize;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == _attack.transform)
        {
            return;
        }

        if (other.GetComponent<IDamageable>() != null)
        {
            _attack.Hit(other);
        }
    }

    private void AdjustSize()
    {
        if (!_photonView.IsMine)
        {
            return;
        }
        transform.localScale = Vector3.one * (1 + ScoreManager.Instance.Score * 0.1f);

    }
}
