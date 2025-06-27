using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.GridLayoutGroup;

public class UI_Player : MonoBehaviour
{
    public TextMeshProUGUI NickNameTextUI;
    public Slider HealthSlider;
    private void Start()
    {
        GameManager.Instance.OnInit += Init;
    }
    private void Init()
    {
        Player player = GameManager.Instance.Player;
        NickNameTextUI.text = $"{player.PhotonView.Owner.NickName}_{player.PhotonView.OwnerActorNr}";

        if (!player.PhotonView.IsMine)
        {
            NickNameTextUI.color = Color.red;
        }
        else
        {
            NickNameTextUI.color = Color.green;
        }
    }
}
