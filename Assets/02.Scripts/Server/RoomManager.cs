using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using Photon.Realtime;
using System;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    private Room _room;
    public Room Room => _room;

    private static RoomManager _instance;
    public static RoomManager Instance => _instance;

    public event Action OnRoomDataChanged;
    public event Action<string> OnPlayerEntered;
    public event Action<string> OnPlayerExited;
    public event Action<string, string> OnPlayerDead;

    private bool _isInitialized;
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        if (!_isInitialized)
        {
            Init();
        }
    }

    public override void OnJoinedRoom()
    {
        Init();
    }
    private void Init()
    {
        _isInitialized = true;
        GameManager.Instance.Init();

        SetRoom();

        OnRoomDataChanged?.Invoke();
        OnPlayerEntered?.Invoke(PhotonNetwork.LocalPlayer.NickName + "_" + PhotonNetwork.LocalPlayer.ActorNumber);

    }

    

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        OnRoomDataChanged?.Invoke();
        OnPlayerEntered?.Invoke(newPlayer.NickName + "_" + newPlayer.ActorNumber);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        OnRoomDataChanged?.Invoke();
        OnPlayerExited?.Invoke(otherPlayer.NickName + "_" + otherPlayer.ActorNumber);
    }

    public void OnPlayerDeath(int actorNumber, int otherActorNumber)
    {
        string deadNickname = _room.Players[actorNumber].NickName + "_" + _room.Players[actorNumber].ActorNumber;
    
        string attackerNickname = (otherActorNumber>0)?_room.Players[otherActorNumber].NickName + "_" + _room.Players[otherActorNumber].ActorNumber : deadNickname;
        if(otherActorNumber == -999)
        {
            attackerNickname = "Bear";
        }
        OnPlayerDead?.Invoke(deadNickname, attackerNickname);
    }
    private void SetRoom()
    {
        _room = PhotonNetwork.CurrentRoom;
        Debug.Log(_room.Name);
        Debug.Log(_room.PlayerCount);
        Debug.Log(_room.MaxPlayers);
    }
}
