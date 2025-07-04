using Photon.Pun;
using UnityEngine;


[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(PhotonTransformView))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class ItemObject : MonoBehaviour
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

            Debug.Log("xxxx");
            ScoreManager.Instance.Score += 10;

            ItemObjectFactory.Instance.RequestDelete(_photonView.ViewID);

        }
    }
}
