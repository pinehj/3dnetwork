using System;
using Unity.VisualScripting;
using UnityEngine;

public interface IDamageable
{
    public abstract void TakeDamage(float damage, int actorNumber);
    public abstract bool CanDamage();
    public abstract Vector3 GetPos();
}
