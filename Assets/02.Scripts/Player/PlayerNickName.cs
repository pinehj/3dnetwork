using TMPro;
using UnityEngine;

public class PlayerNickName : PlayerAbility
{
    public TextMeshProUGUI NickNameTextUI;

    private void Start()
    {
        NickNameTextUI.text = $"{_owner.PhotonView.Owner.NickName}_{_owner.PhotonView.OwnerActorNr}";

        if (!_owner.PhotonView.IsMine)
        {
            NickNameTextUI.color = Color.red;
        }
        else
        {
            NickNameTextUI.color = Color.green;
        }
    }
}
