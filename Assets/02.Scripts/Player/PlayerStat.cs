using System;
using UnityEngine;

[Serializable]
public class PlayerStat
{
    [Header("이동")]
    public float WalkSpeed = 6f;
    public float RunSpeed = 10f;
    public float JumpPower = 4f;
    public float RotationSpeed = 300f;
    
    [Header("체력")]
    public float MaxHealth = 100f;
    
    [Header("공격")]
    public float AttackSpeed = 1.2f;
    public float AttackPower = 20f;

    [Header("스태미너")]
    public float MaxStamina = 100f;
    public float StaminaRegen = 20f;
    public float RunStaminaCost = 20f;
    public float JumpStaminaCost = 20f;
    public float AttackStaminaCost = 20f;
}
