using System;
using UnityEngine;

[Serializable]
public class BearStat
{
    public float MoveSpeed = 3.5f;
    public float ChaseSpeed = 8f;

    public float IdleTimeDuration = 5f;

    public float PatorlDistance = 2.5f;
    public float AttakDistance = 2.5f;
    public float AttackSpeed = .2f;
    public float AttackPower = 50f;
    public float FindTargetDuration = 0.2f;
    public float FindTargetRad = 15f;

    public float MaxHealth = 200f;
}
