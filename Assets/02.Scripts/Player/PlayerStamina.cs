using System;
using UnityEngine;

public class PlayerStamina : PlayerAbility
{
    public event Action<float, float> OnDataChanged;
    [SerializeField] private float _currentStamina;
    public float CurrentStamina
    {
        get
        {
            return _currentStamina;
        }
        set
        {
            _currentStamina = Mathf.Clamp(value, 0, _owner.Stat.MaxStamina);
            OnDataChanged?.Invoke(_currentStamina, _owner.Stat.MaxStamina);
        }
    }

    private void Start()
    {
        CurrentStamina = _owner.Stat.MaxStamina;
    }
    private void Update()
    {
        if (!_owner.State.IsRunning && !_owner.State.IsJumping)
        {
            CurrentStamina += _owner.Stat.StaminaRegen * Time.deltaTime;
        }
    }

    public bool TryConsumeStamina(float value)
    {
        if(CurrentStamina < value)
        {
            return false;
        }

        CurrentStamina -= value;
        return true;
    }
}
