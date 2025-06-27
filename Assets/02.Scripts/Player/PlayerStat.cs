using System;
using UnityEngine;

[Serializable]
public class PlayerStat : MonoBehaviour
{
    public float WalkSpeed;
    public float RunSpeed;
    public float JumpPower;
    public float AttackSpeed;
    public float RotationSpeed;

    public float MaxStamina;
    public float StaminaRegen;
    public float RunStaminaCost;
    public float JumpStaminaCost;
    public float AttackStaminaCost;
}
