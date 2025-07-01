using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Player : PlayerAbility
{
    public TextMeshProUGUI NickNameTextUI;
    public Slider HealthSlider;
    private void Start()
    {
        Init();
    }
    private void Init()
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

        _owner.GetAbility<PlayerHealth>().OnDataChanged += UpdateHealth;
    }

    private void UpdateHealth(float value)
    {
        HealthSlider.value = value;
    }
}
