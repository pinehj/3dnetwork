using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LobbyScene : MonoBehaviourPunCallbacks
{
    public TMP_InputField NicknameInputField;
    public TMP_InputField RoomNameInputField;
    public Button MaleButton;
    public Button FemaleButton;
    public GameObject MaleModel;
    public GameObject FemaleModel;


    private List<RoomInfo> _roomList;
    public List<RoomInfo> RoomList => _roomList;
    public event Action OnDataChanged;
    public static EPlayerType PlayerType = EPlayerType.Male;

    public void OnClickMaleButton() => OnClickPlayerTypeButton(EPlayerType.Male);
    public void OnClickFemaleButton() => OnClickPlayerTypeButton(EPlayerType.Female);

    public static LobbyScene Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        OnClickMaleButton();
    }
    public void OnClickPlayerTypeButton(EPlayerType playerType)
    {
        MaleModel.SetActive(playerType == EPlayerType.Male);
        FemaleModel.SetActive(playerType == EPlayerType.Female);

        PlayerType = playerType;
    }

    
    public void OnClickMakeRoomButton()
    {
        MakeRoom();
    }
    private void MakeRoom()
    {
        string nickName = NicknameInputField.text;
        string roomName = RoomNameInputField.text;

        if (string.IsNullOrEmpty(nickName) || string.IsNullOrEmpty(roomName))
        {
            return;
        }

        PhotonNetwork.NickName = nickName;

        RoomOptions roomOptions = new RoomOptions();
        
        roomOptions.MaxPlayers = 20;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;

        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }
    // 룸 목록을 수신하는 콜백 함수
    // 내가 입장한 로비(채널)에서 룸이 수정/삭제/추가되면 호출되는 콜백 함수
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        _roomList = roomList;
        OnDataChanged?.Invoke();

        foreach (RoomInfo room in roomList)
        {
            // UI에 필요한 내용: 방 이름, 방장명, 인원수
            Debug.Log($"{room.Name}(방장명): ({room.PlayerCount}/{room.MaxPlayers})");
        }
    }

    public void TryJoinRoom(string roomName)
    {
        string nickname = NicknameInputField.text;

        if (string.IsNullOrEmpty(nickname))
        {
            return;
        }

        PhotonNetwork.NickName = nickname;

        PhotonNetwork.JoinRoom(roomName);

        return;
    }

}
