using Photon.Pun;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (!player.PhotonView.IsMine || player.State.IsDead)
            {
                return;
            }
            player.GetAbility<PlayerHealth>().CurrentHealth += 50;

            ItemObjectFactory.Instance.RequestDelete(_photonView.ViewID);
        }
    }
}
