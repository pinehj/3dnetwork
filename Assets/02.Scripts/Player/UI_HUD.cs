using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_HUD : Singleton<UI_HUD>
{
    public Slider StaminaSlider;

    protected override void Start()
    {
        base.Start();
        GameManager.Instance.OnInit += Init;
    }
    public void Init()
    {
        GameManager.Instance.Player.GetAbility<PlayerStamina>().OnDataChanged += UpdateStaminaUI;
    }
    public void UpdateStaminaUI(float value, float maxValue)
    {
        StaminaSlider.maxValue = maxValue;
        StaminaSlider.value = value;
    }
}
