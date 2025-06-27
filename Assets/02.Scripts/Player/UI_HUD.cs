using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_HUD : Singleton<UI_HUD>
{
    public Slider StaminaSlider;

    public void Init()
    {
        // GameManager로?
        List<Player> players = FindObjectsByType<Player>(FindObjectsSortMode.None).Where(player => player.PhotonView.IsMine).ToList();
        foreach(Player player in players)
        {
            player.GetAbility<PlayerStamina>().OnDataChanged += UpdateStaminaUI;
            break;
        }
    }
    public void UpdateStaminaUI(float value, float maxValue)
    {
        Debug.Log("s");
        StaminaSlider.maxValue = maxValue;
        StaminaSlider.value = value;
    }
}
