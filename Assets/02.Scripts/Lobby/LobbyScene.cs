using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LobbyScene : MonoBehaviour
{
    public TMP_InputField NicknameInputField;
    public TMP_InputField RoomNameInputField;
    public Button MaleButton;
    public Button FemaleButton;
    public GameObject MaleModel;
    public GameObject FemaleModel;

    public static EPlayerType PlayerType = EPlayerType.Male;

    public void OnClickMaleButton() => OnClickPlayerTypeButton(EPlayerType.Male);
    public void OnClickFemaleButton() => OnClickPlayerTypeButton(EPlayerType.Female);

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

        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }
}
