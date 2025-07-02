using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerInfo : MonoBehaviour
{
    public Slider StaminaSlider;
    public Slider HealthSlider;

    private void Start()
    {
        GameManager.Instance.OnInit += Init;
    }
    public void Init()
    {
        GameManager.Instance.Player.GetAbility<PlayerStamina>().OnDataChanged += UpdateStaminaUI;
        GameManager.Instance.Player.GetAbility<PlayerHealth>().OnDataChanged += UpdateHealthUI;
    }
    public void UpdateStaminaUI(float value)
    {
        StaminaSlider.value = value;
    }
    public void UpdateHealthUI(float value)
    {
        HealthSlider.value = value;
    }
}
