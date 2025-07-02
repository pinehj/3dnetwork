using Photon.Pun;
using UnityEngine;
using UnityEngine.UIElements;

public enum EItemType
{
    Score,
}

[RequireComponent(typeof(PhotonView))]
public class ItemObjectFactory : MonoBehaviourPun
{
    private static ItemObjectFactory _instance;
    public static ItemObjectFactory Instance => _instance;

    private PhotonView _photonView;

    private void Awake()
    {
        _instance = this;
        _photonView = GetComponent<PhotonView>();
    }
    public void RequestCreate(EItemType itemType, Vector3 position)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Create(itemType, position);
            return;
        }
        _photonView.RPC(nameof(Create), RpcTarget.MasterClient, itemType, position);
    }

    [PunRPC]
    private void Create(EItemType itemType, Vector3 position)
    {
        PhotonNetwork.InstantiateRoomObject($"{itemType}Item", position + Vector3.up * 2, Quaternion.identity);
    }

    public void RequestDelete(int viewID)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Delete(viewID);
            return;
        }
        _photonView.RPC(nameof(Delete), RpcTarget.MasterClient, viewID);

    }

    [PunRPC]
    private void Delete(int viewID)
    {
        PhotonNetwork.Destroy(PhotonView.Find(viewID).gameObject);
    }
}
